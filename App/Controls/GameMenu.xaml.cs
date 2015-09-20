using Microsoft.Win32;
using SmallWorld.Models;
using SmallWorld.Models.AI;
using SmallWorld.Models.Factions;
using SmallWorld.Models.Maps;
using SmallWorld.Models.Utils;
using SmallWorld.Pages;
using SmallWorld.ViewModels;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace SmallWorld.Controls
{
    /// <summary>
    /// Interaction logic for GameMenu.xaml
    /// </summary>
    public partial class GameMenu : UserControl
    {
        public GameMenu()
        {
            InitializeComponent();
        }

        private void RestartGame_Click(object sender, RoutedEventArgs e)
        {
            var game = ((GameContext)DataContext).Game;

            var newGame = new Game(MapFactory.GetType(game.Map), game.Map.Seed);
            foreach (var intPlayerPair in game.Players)
            {
                Player player = intPlayerPair.Value;
                newGame.AddPlayer(player.Name, player.Color, FactionFactory.GetType(player.Faction), AIFactory.GetType(player.AI));
            }

            App.Current.NavigateTo(new GamePage(newGame));
        }

        private void SaveGame_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.FileName = "save";
            dialog.DefaultExt = ".sw";
            dialog.Filter = "Fichiers SmallWorld (*.sw)|*.sw|Tous les fichiers (*.*)|*.*";

            if (dialog.ShowDialog(App.Current.MainWindow) == true && !string.IsNullOrWhiteSpace(dialog.FileName))
            {
                try
                {
                    var game = ((GameContext)DataContext).Game;
                    var file = new FileStream(dialog.FileName, FileMode.Create);
                    var serializer = GameSerializer.Get();

                    serializer.WriteObject(file, game);
                    file.Close();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Erreur de sauvegarde");
                }
            }
        }

        private void ExitGame_Click(object sender, RoutedEventArgs e)
        {
            App.Current.NavigateTo(new HomePage());
        }
    }
}
