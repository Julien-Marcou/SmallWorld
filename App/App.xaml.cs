using SmallWorld.Pages;
using SmallWorld.Windows;
using System.Windows;
using System.Windows.Controls;

namespace SmallWorld
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static new App Current
        {
            get { return (App)Application.Current; }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow = new MainWindow();

            NavigateTo(new HomePage());

            MainWindow.Show();
        }

        internal void NavigateTo(Page page)
        {
            MainWindow.Content = page;
        }
    }
}
