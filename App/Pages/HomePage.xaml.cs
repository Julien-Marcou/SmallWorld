using Microsoft.Win32;
using SmallWorld.Models;
using SmallWorld.Models.Utils;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace SmallWorld.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            App.Current.NavigateTo(new CreatePage());
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Fichiers SmallWorld (*.sw)|*.sw|Tous les fichiers (*.*)|*.*";
            if (dialog.ShowDialog(App.Current.MainWindow) == true && !string.IsNullOrWhiteSpace(dialog.FileName))
            {
                try
                {
                    var file = new FileStream(dialog.FileName, FileMode.Open);
                    var serializer = GameSerializer.Get();
                    var game = (Game)serializer.ReadObject(file);
                    file.Close();

                    App.Current.NavigateTo(new GamePage(game));
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Erreur d'ouverture");
                }
            }
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            App.Current.NavigateTo(new HelpPage());
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
