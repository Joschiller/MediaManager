using MediaManager.Globals.LanguageProvider;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace MediaManager.GUI.Controls.Analyze
{
    /// <summary>
    /// Interaction logic for AnalyzeModeSelector.xaml
    /// </summary>
    public partial class AnalyzeModeSelector : UserControl, UpdatedLanguageUser
    {
        public delegate void ModeHandler(AnalyzeMode mode);
        public event ModeHandler ModeChanged;

        public AnalyzeMode Mode { get; set; } = AnalyzeMode.MediumEmpty;

        public ObservableCollection<string> Attributes { get; set; } = new ObservableCollection<string>();
        private int ComboBoxMode = 0;

        public AnalyzeModeSelector()
        {
            InitializeComponent();
            DataContext = this;
            RegisterAtLanguageProvider();

            rbEmpty.IsChecked = true;
        }

        public void RegisterAtLanguageProvider() => LanguageProvider.RegisterUnique(this);
        public void LoadTexts(string language)
        {
            rbEmpty.Content = LanguageProvider.getString("Controls.Analyze.Empty.Radio");
            rbDuplicate.Content = LanguageProvider.getString("Controls.Analyze.Duplicate.Radio");
            rbMediaAttribute.Content = LanguageProvider.getString("Controls.Analyze.MediaAttribute.Radio");
            rbPartAttribute.Content = LanguageProvider.getString("Controls.Analyze.PartAttribute.Radio");
            SetupComboBox();
            LoadDescription();
        }
        private void SetupComboBox()
        {
            Attributes.Clear();
            switch (Mode)
            {
                case AnalyzeMode.MediumEmpty:
                case AnalyzeMode.MediumDoubled:
                    attributeSelection.Visibility = Visibility.Collapsed;
                    break;
                case AnalyzeMode.MediumDescription:
                case AnalyzeMode.MediumTags:
                case AnalyzeMode.MediumLocation:
                    attributeSelection.Visibility = Visibility.Visible;
                    Attributes.Add(LanguageProvider.getString("Controls.Analyze.MediaAttribute.Description.Select"));
                    Attributes.Add(LanguageProvider.getString("Controls.Analyze.MediaAttribute.Tags.Select"));
                    Attributes.Add(LanguageProvider.getString("Controls.Analyze.MediaAttribute.Location.Select"));
                    attributeSelection.SelectedIndex = 0;
                    ComboBoxMode = 0;
                    afterComboBoxChanged();
                    break;
                case AnalyzeMode.PartDescription:
                case AnalyzeMode.PartTags:
                case AnalyzeMode.PartLength:
                case AnalyzeMode.PartPublication:
                case AnalyzeMode.PartImage:
                    attributeSelection.Visibility = Visibility.Visible;
                    Attributes.Add(LanguageProvider.getString("Controls.Analyze.PartAttribute.Description.Select"));
                    Attributes.Add(LanguageProvider.getString("Controls.Analyze.PartAttribute.Tags.Select"));
                    Attributes.Add(LanguageProvider.getString("Controls.Analyze.PartAttribute.Length.Select"));
                    Attributes.Add(LanguageProvider.getString("Controls.Analyze.PartAttribute.Publication.Select"));
                    Attributes.Add(LanguageProvider.getString("Controls.Analyze.PartAttribute.Image.Select"));
                    attributeSelection.SelectedIndex = 0;
                    ComboBoxMode = 1;
                    afterComboBoxChanged();
                    break;
            }
        }
        private void LoadDescription()
        {
            switch (Mode)
            {
                case AnalyzeMode.MediumEmpty: description.Text = LanguageProvider.getString("Controls.Analyze.Empty.Description"); break;
                case AnalyzeMode.MediumDoubled: description.Text = LanguageProvider.getString("Controls.Analyze.Duplicate.Description"); break;
                case AnalyzeMode.MediumDescription: description.Text = LanguageProvider.getString("Controls.Analyze.MediaAttribute.Description.Description"); break;
                case AnalyzeMode.MediumTags: description.Text = LanguageProvider.getString("Controls.Analyze.MediaAttribute.Tags.Description"); break;
                case AnalyzeMode.MediumLocation: description.Text = LanguageProvider.getString("Controls.Analyze.MediaAttribute.Location.Description"); break;
                case AnalyzeMode.PartDescription: description.Text = LanguageProvider.getString("Controls.Analyze.PartAttribute.Description.Description"); break;
                case AnalyzeMode.PartTags: description.Text = LanguageProvider.getString("Controls.Analyze.PartAttribute.Tags.Description"); break;
                case AnalyzeMode.PartLength: description.Text = LanguageProvider.getString("Controls.Analyze.PartAttribute.Length.Description"); break;
                case AnalyzeMode.PartPublication: description.Text = LanguageProvider.getString("Controls.Analyze.PartAttribute.Publication.Description"); break;
                case AnalyzeMode.PartImage: description.Text = LanguageProvider.getString("Controls.Analyze.PartAttribute.Image.Description"); break;
            }
        }

        private void rbEmpty_Click(object sender, RoutedEventArgs e)
        {
            Mode = AnalyzeMode.MediumEmpty;
            afterRadioChanged();
        }
        private void rbDuplicate_Click(object sender, RoutedEventArgs e)
        {
            Mode = AnalyzeMode.MediumDoubled;
            afterRadioChanged();
        }
        private void rbMediaAttribute_Click(object sender, RoutedEventArgs e)
        {
            Mode = AnalyzeMode.MediumDescription;
            afterRadioChanged();
        }
        private void rbPartAttribute_Click(object sender, RoutedEventArgs e)
        {
            Mode = AnalyzeMode.PartDescription;
            afterRadioChanged();
        }
        private void afterRadioChanged()
        {
            SetupComboBox();
            LoadDescription();
            ModeChanged?.Invoke(Mode);
        }

        private void attributeSelection_SelectionChanged(object sender, SelectionChangedEventArgs e) => afterComboBoxChanged();
        private void afterComboBoxChanged()
        {
            switch (ComboBoxMode)
            {
                case 0:
                    switch (attributeSelection.SelectedIndex)
                    {
                        case 0: Mode = AnalyzeMode.MediumDescription; break;
                        case 1: Mode = AnalyzeMode.MediumTags; break;
                        case 2: Mode = AnalyzeMode.MediumLocation; break;
                    }
                    break;
                case 1:
                    switch (attributeSelection.SelectedIndex)
                    {
                        case 0: Mode = AnalyzeMode.PartDescription; break;
                        case 1: Mode = AnalyzeMode.PartTags; break;
                        case 2: Mode = AnalyzeMode.PartLength; break;
                        case 3: Mode = AnalyzeMode.PartPublication; break;
                        case 4: Mode = AnalyzeMode.PartImage; break;
                    }
                    break;
            }
            LoadDescription();
            ModeChanged?.Invoke(Mode);
        }
    }
}
