using System.Linq;
using System.Windows.Controls;

namespace MediaManager.GUI.Controls.Edit
{
    /// <summary>
    /// Interaction logic for MediaViewer.xaml
    /// </summary>
    public partial class MediaViewer : UserControl
    {
        private MediumWithTags medium;

        public MediaViewer()
        {
            InitializeComponent();
        }

        public MediumWithTags Medium
        {
            get => medium;
            set
            {
                medium = value;
                list.SetPartList(medium.Title, medium.Parts.Select(p => new List.PartListElement
                {
                    Id = p.Id,
                    Title = p.Title
                }).ToList());
                OpenMediumTab();
            }
        }

        public void OpenMediumTab()
        {
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
    }
}
