using System.Windows.Media;

namespace MediaManager.GUI.Controls.MultiUseTabs
{
    public interface MultiUseTabsControl
    {
        ImageSource GetHeader();
        bool GetIsVisible();
    }
}
