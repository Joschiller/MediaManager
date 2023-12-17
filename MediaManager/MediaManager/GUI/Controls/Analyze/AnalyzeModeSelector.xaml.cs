using LanguageProvider;
using static LanguageProvider.LanguageProvider;
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
        #region Events
        public delegate void ModeHandler(AnalyzeMode mode);
        public event ModeHandler ModeChanged;
        #endregion

        #region Properties
        public AnalyzeMode Mode { get; set; } = AnalyzeMode.MediumEmpty;
        #endregion

        #region Bindings
        public ObservableCollection<string> Attributes { get; set; } = new ObservableCollection<string>();
        private int ComboBoxMode = 0;
        #endregion

        #region Setup
        public AnalyzeModeSelector()
        {
            InitializeComponent();
            DataContext = this;
            RegisterAtLanguageProvider();

            rbMediumEmpty.IsChecked = true;
            afterRadioChanged();
        }
        public void RegisterAtLanguageProvider() => Register(this);
        public void LoadTexts(string language)
        {
            rbMediumEmpty.Content = getString("Controls.Analyze.MediumEmpty.Radio");
            rbMediumDuplicate.Content = getString("Controls.Analyze.MediumDuplicate.Radio");
            rbMediumCommonTags.Content = getString("Controls.Analyze.MediumCommonTags.Radio");
            rbMediaAttribute.Content = getString("Controls.Analyze.MediaAttribute.Radio");
            rbPartAttribute.Content = getString("Controls.Analyze.PartAttribute.Radio");
            SetupComboBox();
            LoadDescription();
        }
        ~AnalyzeModeSelector() => Unregister(this);
        private void SetupComboBox()
        {
            Attributes.Clear();
            switch (Mode)
            {
                case AnalyzeMode.MediumEmpty:
                case AnalyzeMode.MediumDoubled:
                case AnalyzeMode.MediumCommonTags:
                    attributeSelection.Visibility = Visibility.Collapsed;
                    break;
                case AnalyzeMode.MediumDescription:
                case AnalyzeMode.MediumTags:
                case AnalyzeMode.MediumLocation:
                    attributeSelection.Visibility = Visibility.Visible;
                    Attributes.Add(getString("Controls.Analyze.MediaAttribute.Description.Select"));
                    Attributes.Add(getString("Controls.Analyze.MediaAttribute.Tags.Select"));
                    Attributes.Add(getString("Controls.Analyze.MediaAttribute.Location.Select"));
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
                    Attributes.Add(getString("Controls.Analyze.PartAttribute.Description.Select"));
                    Attributes.Add(getString("Controls.Analyze.PartAttribute.Tags.Select"));
                    Attributes.Add(getString("Controls.Analyze.PartAttribute.Length.Select"));
                    Attributes.Add(getString("Controls.Analyze.PartAttribute.Publication.Select"));
                    Attributes.Add(getString("Controls.Analyze.PartAttribute.Image.Select"));
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
                case AnalyzeMode.MediumEmpty: description.Text = getString("Controls.Analyze.MediumEmpty.Description"); break;
                case AnalyzeMode.MediumDoubled: description.Text = getString("Controls.Analyze.MediumDuplicate.Description"); break;
                case AnalyzeMode.MediumCommonTags: description.Text = getString("Controls.Analyze.MediumCommonTags.Description"); break;
                case AnalyzeMode.MediumDescription: description.Text = getString("Controls.Analyze.MediaAttribute.Description.Description"); break;
                case AnalyzeMode.MediumTags: description.Text = getString("Controls.Analyze.MediaAttribute.Tags.Description"); break;
                case AnalyzeMode.MediumLocation: description.Text = getString("Controls.Analyze.MediaAttribute.Location.Description"); break;
                case AnalyzeMode.PartDescription: description.Text = getString("Controls.Analyze.PartAttribute.Description.Description"); break;
                case AnalyzeMode.PartTags: description.Text = getString("Controls.Analyze.PartAttribute.Tags.Description"); break;
                case AnalyzeMode.PartLength: description.Text = getString("Controls.Analyze.PartAttribute.Length.Description"); break;
                case AnalyzeMode.PartPublication: description.Text = getString("Controls.Analyze.PartAttribute.Publication.Description"); break;
                case AnalyzeMode.PartImage: description.Text = getString("Controls.Analyze.PartAttribute.Image.Description"); break;
            }
        }
        #endregion

        #region Handler
        private void rbMediumEmpty_Click(object sender, RoutedEventArgs e)
        {
            Mode = AnalyzeMode.MediumEmpty;
            afterRadioChanged();
        }
        private void rbMediumDuplicate_Click(object sender, RoutedEventArgs e)
        {
            Mode = AnalyzeMode.MediumDoubled;
            afterRadioChanged();
        }
        private void rbMediumCommonTags_Click(object sender, RoutedEventArgs e)
        {
            Mode = AnalyzeMode.MediumCommonTags;
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
        #endregion

        public void ModeUp()
        {
            switch(Mode)
            {
                case AnalyzeMode.MediumEmpty:
                    rbPartAttribute.IsChecked = true;
                    rbPartAttribute_Click(null, null);
                    break;
                case AnalyzeMode.MediumDoubled:
                    rbMediumEmpty.IsChecked = true;
                    rbMediumEmpty_Click(null, null);
                    break;
                case AnalyzeMode.MediumCommonTags:
                    rbMediumDuplicate.IsChecked = true;
                    rbMediumDuplicate_Click(null, null);
                    break;
                case AnalyzeMode.MediumDescription:
                    rbMediumCommonTags.IsChecked = true;
                    rbMediumCommonTags_Click(null, null);
                    break;
                case AnalyzeMode.PartDescription:
                    rbMediaAttribute.IsChecked = true;
                    rbMediaAttribute_Click(null, null);
                    break;
            }
        }
        public void ModeDown()
        {
            switch (Mode)
            {
                case AnalyzeMode.MediumEmpty:
                    rbMediumDuplicate.IsChecked = true;
                    rbMediumDuplicate_Click(null, null);
                    break;
                case AnalyzeMode.MediumDoubled:
                    rbMediumCommonTags.IsChecked = true;
                    rbMediumCommonTags_Click(null, null);
                    break;
                case AnalyzeMode.MediumCommonTags:
                    rbMediaAttribute.IsChecked = true;
                    rbMediaAttribute_Click(null, null);
                    break;
                case AnalyzeMode.MediumDescription:
                    rbPartAttribute.IsChecked = true;
                    rbPartAttribute_Click(null, null);
                    break;
                case AnalyzeMode.PartDescription:
                    rbMediumEmpty.IsChecked = true;
                    rbMediumEmpty_Click(null, null);
                    break;
            }
        }
    }
}
