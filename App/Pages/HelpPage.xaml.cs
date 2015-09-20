using System.Windows;
using System.Windows.Controls;

namespace SmallWorld.Pages
{
    /// <summary>
    /// Interaction logic for HelpPage.xaml
    /// </summary>
    public partial class HelpPage : Page
    {
        public HelpPage()
        {
            InitializeComponent();
        }

        private void ExitHelp_Click(object sender, RoutedEventArgs e)
        {
            App.Current.NavigateTo(new HomePage());
        }
    }
}
