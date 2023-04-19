using MediaManager.Globals.LanguageProvider;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static MediaManager.Globals.DataConnector;
using static MediaManager.Globals.Navigation;

namespace MediaManager.GUI.Controls.Edit
{
    /// <summary>
    /// Interaction logic for MediaEditor.xaml
    /// </summary>
    public partial class MediaEditor : UserControl, UpdatedLanguageUser
    {
        public delegate void MediumHandler(MediumWithTags medium);
        public event MediumHandler MediumEdited;

        private MediumWithTags medium;

        public MediaEditor()
        {
            InitializeComponent();
            RegisterAtLanguageProvider();
        }

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

        private void reloadList() => list.SetPartList(medium.Title, medium.Parts.Select(p => new List.PartListElement
        {
            Id = p.Id,
            Title = p.Title
        }).ToList());

        private void onChange() => MediumEdited?.Invoke(medium);

        public void OpenMediumTab()
        {
            deletePart.Visibility = Visibility.Collapsed;
            viewer.Medium = new EditableMedium
            {
                Id = medium.Id,
                CatalogueId = medium.CatalogueId,
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
                viewer.Part = new EditablePart
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

        private void list_SelectionChanged(int? id)
        {
            if (id.HasValue) OpenPartTab(id.Value, false);
            else OpenMediumTab();
        }

        private void addPart_Click(object sender, RoutedEventArgs e)
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
            reloadList();
            list.SelectItem(newId);
            onChange();
        }
        private void deletePart_Click(object sender, RoutedEventArgs e)
        {
            var performDeletion = !CURRENT_CATALOGUE.DeletionConfirmationPart;
            if (!performDeletion)
            {
                var confirmation = ShowDeletionConfirmationDialog(LanguageProvider.getString("Controls.Edit.PartDeletion"));
                performDeletion = confirmation.HasValue && confirmation.Value;
            }
            if (performDeletion)
            {
                medium.Parts.Remove(medium.Parts.Find(p => p.Id == list.GetSelectedItem()));
                reloadList();
                list.SelectItem(null);
                onChange();
            }
        }

        private void viewer_MediumEdited(EditableMedium medium)
        {
            this.medium.Title = medium.Title;
            this.medium.Description = medium.Description;
            this.medium.Location = medium.Location;
            this.medium.Tags = medium.Tags;
            onChange();
        }
        private void viewer_PartEdited(EditablePart part)
        {
            var internalPart = medium.Parts.FirstOrDefault(p => p.Id == part.Id);
            internalPart.Title = part.Title;
            internalPart.Description = part.Description;
            internalPart.Favourite = part.Favourite;
            internalPart.Length = part.Length;
            internalPart.Publication_Year = part.Publication_Year;
            internalPart.Image = part.Image;
            internalPart.Tags = part.Tags;
            onChange();
        }

        public void RegisterAtLanguageProvider() => LanguageProvider.Register(this);
        public void LoadTexts(string language)
        {
            addPart.Tooltip = LanguageProvider.getString("Controls.Edit.Button.AddPart");
            deletePart.Tooltip = LanguageProvider.getString("Controls.Edit.Button.DeletePart");
            throw new NotImplementedException();
        }
        ~MediaEditor() => LanguageProvider.Unregister(this);
    }
}
