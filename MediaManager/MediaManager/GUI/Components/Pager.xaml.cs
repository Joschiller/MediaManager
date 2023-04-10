using MediaManager.Globals.LanguageProvider;
using System.Windows;
using System.Windows.Controls;

namespace MediaManager.GUI.Components
{
    /// <summary>
    /// Interaction logic for Pager.xaml
    /// </summary>
    public partial class Pager : UserControl, UpdatedLanguageUser
    {
        public delegate void PageChangeHandler(int newPage);
        public event PageChangeHandler PageChanged;

        private int _CurrentPage = 1;
        private int _TotalPages = 1;
        public int CurrentPage
        {
            get => _CurrentPage; set
            {
                _CurrentPage = value > TotalPages ? TotalPages : (value < 1 ? 1 : value);
                UpdateGUI();
                PageChanged?.Invoke(_CurrentPage);
            }
        }
        public int TotalPages
        {
            get => _TotalPages; set
            {
                _TotalPages = value < 1 ? 1 : value;
                if (CurrentPage > value) CurrentPage = value;
                UpdateGUI();
            }
        }

        public Pager()
        {
            InitializeComponent();
            UpdateGUI();
            RegisterAtLanguageProvider();
        }

        private void prev_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage--;
            UpdateGUI();
        }
        private void next_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage++;
            UpdateGUI();
        }
        private void UpdateGUI()
        {
            page.Text = CurrentPage.ToString() + "/" + TotalPages.ToString();
            prev.Enabled = CurrentPage > 1;
            next.Enabled = CurrentPage < TotalPages;
        }

        public void RegisterAtLanguageProvider() => LanguageProvider.Register(this);
        public void LoadTexts(string language)
        {
            prev.ToolTip = LanguageProvider.getString("Component.Pager.Previous");
            next.ToolTip = LanguageProvider.getString("Component.Pager.Next");
        }
        ~Pager() => LanguageProvider.Unregister(this);
    }
}
