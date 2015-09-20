using SmallWorld.Models;
using SmallWorld.Models.AI;
using SmallWorld.Models.Factions;
using SmallWorld.Models.Maps;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SmallWorld.Pages
{
    /// <summary>
    /// Interaction logic for CreatePage.xaml
    /// </summary>
    public partial class CreatePage : Page
    {
        public CreatePage()
        {
            InitializeComponent();
        }

        private void ExitCreate_Click(object sender, RoutedEventArgs e)
        {
            App.Current.NavigateTo(new HomePage());
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            var gameMap = (MapType)((ComboBoxItem)((ComboBox)GameMap).SelectedItem).Tag;
            var player1Pseudo = ((TextBox)Player1Pseudo).Text;
            var player2Pseudo = ((TextBox)Player2Pseudo).Text;

            var player1Color = (SolidColorBrush)((ComboBox)Player1Color).SelectedItem;
            var player2Color = (SolidColorBrush)((ComboBox)Player2Color).SelectedItem;

            var player1Faction = (FactionType)((ComboBoxItem)((ComboBox)Player1Faction).SelectedItem).Tag;
            var player2Faction = (FactionType)((ComboBoxItem)((ComboBox)Player2Faction).SelectedItem).Tag;

            var player1AI= (AIType)((ComboBoxItem)((ComboBox)Player1AI).SelectedItem).Tag;
            var player2AI= (AIType)((ComboBoxItem)((ComboBox)Player2AI).SelectedItem).Tag;

            if (string.IsNullOrWhiteSpace(player1Pseudo) || string.IsNullOrWhiteSpace(player2Pseudo))
            {
                MessageBox.Show("Vous devez renseigner le pseudo des joueurs.", "Impossible de créer la partie");
            }
            else
            {
                var game = new Game(gameMap);
                game.AddPlayer(player1Pseudo, new Models.Utils.Color(player1Color.Color.R, player1Color.Color.G, player1Color.Color.B), player1Faction, player1AI);
                game.AddPlayer(player2Pseudo, new Models.Utils.Color(player2Color.Color.R, player2Color.Color.G, player2Color.Color.B), player2Faction, player2AI);

                App.Current.NavigateTo(new GamePage(game));
            }
        }
    }
}
