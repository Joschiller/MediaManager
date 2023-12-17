using LanguageProvider;
using static LanguageProvider.LanguageProvider;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static MediaManager.Globals.DataConnector;
using static MediaManager.Globals.KeyboardShortcutHelper;
using static MediaManager.Globals.Navigation;

namespace MediaManager.GUI.Controls.Edit
{
    /// <summary>
    /// Interaction logic for MediaEditor.xaml
    /// </summary>
    public partial class MediaEditor : UserControl, UpdatedLanguageUser
    {
        #region Events
        public delegate void MediumHandler(MediumWithTags medium);
        public event MediumHandler MediumEdited;
        #endregion

        #region Properties
        private MediumWithTags medium;
        public MediumWithTags Medium
        {
            get => medium;
            set
            {
                medium = value;
                reloadList();
                OpenMediumTab();
            }
        }
        #endregion

        #region Setup
        private bool needsListReload;
        public MediaEditor()
        {
            InitializeComponent();
            RegisterAtLanguageProvider();
        }
        public void RegisterAtLanguageProvider() => Register(this);
        public void LoadTexts(string language)
        {
            addPart.Tooltip = getString("Controls.Edit.Button.AddPart");
            deletePart.Tooltip = getString("Controls.Edit.Button.DeletePart");
        }
        ~MediaEditor() => Unregister(this);
        private void reloadList()
        {
            list.SetPartList(medium.Title, medium.Parts.Select(p => new List.PartListElement
            {
                Id = p.Id,
                Title = p.Title
            }).ToList());
            needsListReload = false;
        }
        #endregion

        #region Getter/Setter
        public void OpenMediumTab()
        {
            deletePart.Visibility = Visibility.Collapsed;
            editor.Medium = new EditableMedium
            {
                Id = medium.Id,
                CatalogId = medium.CatalogId,
                Title = medium.Title,
                Description = medium.Description,
                Location = medium.Location,
                Tags = medium.Tags
            };
        }
        public void OpenPartTab(int id, bool selectInList = true)
        {
            deletePart.Visibility = Visibility.Visible;
            if (selectInList) list.SelectItem(id);
            var part = medium.Parts.FirstOrDefault(p => p.Id == id);
            if (part != null)
            {
                editor.Part = new EditablePart
                {
                    Id = part.Id,
                    MediumId = medium.Id,
                    Title = part.Title,
                    Description = part.Description,
                    Favourite = part.Favourite,
                    Length = part.Length,
                    Publication_Year = part.Publication_Year,
                    Image = part.Image,
                    Tags = part.Tags,
                    TagsBlockedByMedium = medium.Tags.Where(t => t.Value.HasValue).Select(t => t.Tag.Id).ToList()
                };
            }
        }
        public void HandleKeycode(Key key, bool shifted)
        {
            switch(key)
            {
                case Key.N:
                    AddPart();
                    break;
                case Key.F:
                    editor.ToggleFavourite();
                    break;
                case Key.I:
                    if (shifted) editor.RemoveImage();
                    else editor.SelectImage();
                    break;
            }
        }
        #endregion

        #region Handler
        private void onChange() => MediumEdited?.Invoke(medium);

        private void list_SelectionChanged(int? id)
        {
            if (needsListReload) reloadList();
            if (id.HasValue) OpenPartTab(id.Value, false);
            else OpenMediumTab();
        }
        private void addPart_Click(object sender, RoutedEventArgs e) => AddPart();
        private void list_PreviewKeyDown(object sender, KeyEventArgs e) => runKeyboardShortcut(e, new System.Collections.Generic.Dictionary<(ModifierKeys Modifiers, Key Key), Action>
        {
            [(ModifierKeys.Control, Key.D)] = DeletePart,
            [(ModifierKeys.None, Key.Delete)] = DeletePart,
        });
        private void deletePart_Click(object sender, RoutedEventArgs e) => DeletePart();

        private void editor_MediumEdited(EditableMedium medium)
        {
            needsListReload = this.medium.Title != medium.Title;
            this.medium.Title = medium.Title;
            this.medium.Description = medium.Description;
            this.medium.Location = medium.Location;
            this.medium.Tags = medium.Tags;
            foreach (var t in this.medium.Tags.Where(t => t.Value.HasValue).ToList())
            {
                foreach (var p in this.medium.Parts)
                {
                    var newTags = p.Tags.Where(pt => pt.Tag.Id != t.Tag.Id).ToList();
                    newTags.Add(new ValuedTag
                    {
                        Tag = t.Tag,
                        Value = t.Value
                    });
                    p.Tags = newTags;
                }
            }
            onChange();
        }
        private void editor_PartEdited(EditablePart part)
        {
            var internalPart = medium.Parts.FirstOrDefault(p => p.Id == part.Id);
            needsListReload = internalPart.Title != part.Title;
            internalPart.Title = part.Title;
            internalPart.Description = part.Description;
            internalPart.Favourite = part.Favourite;
            internalPart.Length = part.Length;
            internalPart.Publication_Year = part.Publication_Year;
            internalPart.Image = part.Image;
            internalPart.Tags = part.Tags;
            onChange();
        }
        #endregion

        #region Functions
        private void AddPart()
        {
            var newId = -medium.Parts.Count - 1;
            var newPart = new PartWithTags
            {
                Id = newId,
                Title = "",
                Description = "",
                Favourite = false,
                Length = 0,
                Publication_Year = 0,
                Image = null,
                Tags = medium.Tags.Select(t => new ValuedTag { Tag = t.Tag, Value = t.Value }).ToList()
            };
            medium.Parts.Add(newPart);
            reloadList();
            OpenPartTab(newId);
            onChange();
        }
        private void DeletePart()
        {
            var part = list.GetSelectedItem();
            if (part == null) return;
            var performDeletion = !GlobalContext.Reader.GetCatalog(CatalogContext.CurrentCatalogId.Value).DeletionConfirmationPart;
            if (!performDeletion)
            {
                var confirmation = ShowDeletionConfirmationDialog(getString("Controls.Edit.PartDeletion"));
                performDeletion = confirmation.HasValue && confirmation.Value;
            }
            if (performDeletion)
            {
                medium.Parts.Remove(medium.Parts.Find(p => p.Id == part));
                reloadList();
                list.SelectItem(null);
                onChange();
            }
        }
        #endregion
    }
}
