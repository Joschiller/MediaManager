namespace MediaManager
{
    public class ValuedTag
    {
        public Tag Tag { get; set; }
        private TagValue _Value;
        public bool? Value
        {
            get => _Value == TagValue.Negative ? false : (_Value == TagValue.Positive ? true : (bool?)null);
            set => _Value = !value.HasValue ? TagValue.Neutral : (value.Value ? TagValue.Positive : TagValue.Negative);
        }
    }
}
