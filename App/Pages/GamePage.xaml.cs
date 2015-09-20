using SmallWorld.Models;
using SmallWorld.Models.Factions;
using SmallWorld.ViewModels;
using SmallWorld.ViewModels.Utils;
using System.Media;
using System.Windows.Controls;
using System.Windows.Input;
using System;
using System.Windows.Media;
using System.Runtime.InteropServices;

namespace SmallWorld.Pages
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        private int volumePercent = 30;
        private SoundPlayer attackUnitSound = new SoundPlayer(Properties.Resources.AttackUnit);
        private SoundPlayer selectDwarfSound = new SoundPlayer(Properties.Resources.SelectDwarf);
        private SoundPlayer selectElfSound = new SoundPlayer(Properties.Resources.SelectElf);
        private SoundPlayer selectKnightSound = new SoundPlayer(Properties.Resources.SelectKnight);
        private SoundPlayer selectOrcSound = new SoundPlayer(Properties.Resources.SelectOrc);
        private SoundPlayer selectSlimeSound = new SoundPlayer(Properties.Resources.SelectSlime);

        [DllImport("winmm.dll")]
        public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

        public GamePage(Game game)
        {
            attackUnitSound.Load();
            selectDwarfSound.Load();
            selectElfSound.Load();
            selectKnightSound.Load();
            selectOrcSound.Load();
            selectSlimeSound.Load();
            InitializeComponent();

            var gameContext = new GameContext(game);
            SetSounds(gameContext.Map);
            DataContext = gameContext;

            App.Current.MainWindow.PreviewKeyDown += Game_PreviewKeyDown;
            App.Current.MainWindow.PreviewMouseWheel += Game_PreviewMouseWheel;
            App.Current.MainWindow.PreviewMouseDown += Game_PreviewMouseDown;
        }

        private void SetSounds(BaseViewModel viewModel)
        {
            var volume = ((ushort.MaxValue / 100) * volumePercent);
            waveOutSetVolume(IntPtr.Zero, (((uint)volume & 0x0000ffff) | ((uint)volume << 16)));

            viewModel.PropertyChanged += (context, property) =>
            {
                var mapContext = context as MapContext;
                if (property.PropertyName == "SelectUnitAction")
                {
                    switch (FactionFactory.GetType(mapContext.SelectedUnit))
                    {
                        case FactionType.Dwarf:
                            selectDwarfSound.Play();
                            break;
                        case FactionType.Elf:
                            selectElfSound.Play();
                            break;
                        case FactionType.Knight:
                            selectKnightSound.Play();
                            break;
                        case FactionType.Orc:
                            selectOrcSound.Play();
                            break;
                        case FactionType.Slime:
                            selectSlimeSound.Play();
                            break;
                    }
                }
                else if (property.PropertyName == "AttackUnitAction")
                {
                    attackUnitSound.Play();
                }
            };
        }

        private void Game_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var gameContext = (GameContext)DataContext;
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                if (e.Key == Key.OemMinus || e.Key == Key.D6)
                {
                    ZoomOut();
                }
                else if (e.Key == Key.OemPlus || e.Key == Key.Add)
                {
                    ZoomIn();
                }
                else if (e.Key == Key.NumPad0 || e.Key == Key.D0)
                {
                    ZoomReset();
                }
            }
            else
            {
                var refTile = gameContext.Map.OveredTile;
                var refUnitsCount = (gameContext.Map.SelectedUnits == null) ? 0 : gameContext.Map.SelectedUnits.Units.Count;
                if (refTile == null)
                {
                    refTile = gameContext.Game.Map.GetTopLeftTile();
                }

                if (e.Key == Key.Down)
                {
                    var overedTile = gameContext.Game.Map.GetTileUnder(refTile);
                    gameContext.Map.OverTileCommand.Execute(overedTile);
                }
                else if (e.Key == Key.Up)
                {
                    var overedTile = gameContext.Game.Map.GetTileOver(refTile);
                    gameContext.Map.OverTileCommand.Execute(overedTile);
                }
                else if (e.Key == Key.Left)
                {
                    var overedTile = gameContext.Game.Map.GetLeftTile(refTile);
                    gameContext.Map.OverTileCommand.Execute(overedTile);
                }
                else if (e.Key == Key.Right)
                {
                    var overedTile = gameContext.Game.Map.GetRightTile(refTile);
                    gameContext.Map.OverTileCommand.Execute(overedTile);
                }
                else if (e.Key == Key.Space)
                {
                    gameContext.Map.SelectTileCommand.Execute(refTile);
                }
                else if (e.Key == Key.Enter)
                {
                    gameContext.EndOfTurnCommand.Execute(null);
                }
                else if (e.Key == Key.NumPad1 || e.Key == Key.D1)
                {
                    if(refUnitsCount > 0)
                    {
                        gameContext.Map.SelectUnitCommand.Execute(gameContext.Map.SelectedUnits.Units[0]);
                    }
                }
                else if (e.Key == Key.NumPad2 || e.Key == Key.D2)
                {
                    if (refUnitsCount > 1)
                    {
                        gameContext.Map.SelectUnitCommand.Execute(gameContext.Map.SelectedUnits.Units[1]);
                    }
                }
                else if (e.Key == Key.NumPad3 || e.Key == Key.D3)
                {
                    if (refUnitsCount > 2)
                    {
                        gameContext.Map.SelectUnitCommand.Execute(gameContext.Map.SelectedUnits.Units[2]);
                    }
                }
                else if (e.Key == Key.NumPad4 || e.Key == Key.D4)
                {
                    if (refUnitsCount > 3)
                    {
                        gameContext.Map.SelectUnitCommand.Execute(gameContext.Map.SelectedUnits.Units[3]);
                    }
                }
                else if (e.Key == Key.NumPad5 || e.Key == Key.D5)
                {
                    if (refUnitsCount > 4)
                    {
                        gameContext.Map.SelectUnitCommand.Execute(gameContext.Map.SelectedUnits.Units[4]);
                    }
                }
                else if (e.Key == Key.NumPad6 || e.Key == Key.D6)
                {
                    if (refUnitsCount > 5)
                    {
                        gameContext.Map.SelectUnitCommand.Execute(gameContext.Map.SelectedUnits.Units[5]);
                    }
                }
                else if (e.Key == Key.NumPad7 || e.Key == Key.D7)
                {
                    if (refUnitsCount > 6)
                    {
                        gameContext.Map.SelectUnitCommand.Execute(gameContext.Map.SelectedUnits.Units[6]);
                    }
                }
                else if (e.Key == Key.NumPad8 || e.Key == Key.D8)
                {
                    if (refUnitsCount > 7)
                    {
                        gameContext.Map.SelectUnitCommand.Execute(gameContext.Map.SelectedUnits.Units[7]);
                    }
                }
                else if (e.Key == Key.NumPad9 || e.Key == Key.D9)
                {
                    if (refUnitsCount > 8)
                    {
                        gameContext.Map.SelectUnitCommand.Execute(gameContext.Map.SelectedUnits.Units[8]);
                    }
                }
            }
        }

        private void Game_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                if (e.Delta < 0)
                {
                    ZoomOut();
                }
                else if (e.Delta > 0)
                {
                    ZoomIn();
                }

                // Handles to prevent scrolling inside the ScrollViewer
                e.Handled = true;
            }
        }

        private void Game_PreviewMouseDown(object sender, MouseEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                if(e.MiddleButton == MouseButtonState.Pressed)
                {
                    ZoomReset();

                    // Handles to prevent scrolling inside the ScrollViewer
                    e.Handled = true;
                }
            }
        }

        private void Zoom(double delta)
        {
            ScaleTransform zoom = GameMap.MapZoom;
            zoom.ScaleX = Math.Max(0.5, Math.Min(2.5, zoom.ScaleX * delta));
            zoom.ScaleY = Math.Max(0.5, Math.Min(2.5, zoom.ScaleY * delta));
        }

        private void ZoomReset()
        {
            ScaleTransform zoom = GameMap.MapZoom;
            zoom.ScaleX = 1;
            zoom.ScaleY = 1;
        }

        private void ZoomOut()
        {
            Zoom(0.8);
        }

        private void ZoomIn()
        {
            Zoom(1.2);
        }
    }
}
