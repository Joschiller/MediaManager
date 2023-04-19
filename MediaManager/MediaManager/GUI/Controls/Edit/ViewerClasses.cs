using System.Collections.Generic;

namespace MediaManager.GUI.Controls.Edit
{
    public class MediumWithTags
    {
        public int Id { get; set; }
        public int CatalogueId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public List<ValuedTag> Tags { get; set; }
    }
    public class PartWithTags
    {
        public int Id { get; set; }
        public int MediumId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Favourite { get; set; }
        public int Length { get; set; }
        public int Publication_Year { get; set; }
        public byte[] Image { get; set; }
        public List<ValuedTag> Tags { get; set; }
        public List<int> TagsBlockedByMedium { get; set; }
    }
}
