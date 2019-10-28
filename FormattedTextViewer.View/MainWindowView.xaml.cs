using System.Windows;

namespace FormattedTextViewer.View
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
            DataContext = App.ServiceProvider.GetService(typeof(IMainWindowViewModel));

            InitializeComponent();
        }
    }
}
