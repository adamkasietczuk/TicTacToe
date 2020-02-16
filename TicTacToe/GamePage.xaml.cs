using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TicTacToe.View;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page, IGameView
    {
        private const int CELL_SIZE = 40;
        private const string PLAYER_ONE_MARKER = "O";
        private const string PLAYER_TWO_MARKER = "X";
        private bool isAi = false;
        private bool isGameFinished = false;
        private string currentMarker = PLAYER_ONE_MARKER;
        private int leastMarkerIndex = -1;
        private IMainWindowView mainWindowView;
        private WrapPanel wrapPanelArea = new WrapPanel();
        private List<Button> buttons = new List<Button>();
        private string[,] area = null;

        public GamePage(IMainWindowView mainWindowView)
        {
            this.mainWindowView = mainWindowView;

            InitializeComponent();
        }

        public void SetAi(bool isAi)
        {
            this.isAi = isAi;
        }

        public bool CheckWinner()
        {
            if(isWinner(PLAYER_ONE_MARKER, Settings.MarksToWin, Settings.GameplayGridSize, leastMarkerIndex))
            {
                labelPlayerOneWin.Visibility = Visibility.Visible;

                return true;
            }

            if (isWinner(PLAYER_TWO_MARKER, Settings.MarksToWin, Settings.GameplayGridSize, leastMarkerIndex))
            {
                labelPlayerTwoWin.Visibility = Visibility.Visible;

                return true;
            }

            for(int i = 0; i < Settings.GameplayGridSize; i++)
            {
                for (int j = 0; j < Settings.GameplayGridSize; j++)
                {
                    if (!string.IsNullOrEmpty(area[i, j]))
                    {
                        if (i + j == Settings.GameplayGridSize + Settings.GameplayGridSize - 2)
                        {
                            EndGame();
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        public void EndGame()
        {
            isGameFinished = true;
            buttonReset.Visibility = Visibility.Visible;
        }

        public void GenerateGrid(Grid grid, int gameplayGridSize)
        {
            grid.Children.Remove(wrapPanelArea);
            wrapPanelArea.Orientation = Orientation.Horizontal;
            wrapPanelArea.HorizontalAlignment = HorizontalAlignment.Center;
            wrapPanelArea.VerticalAlignment = VerticalAlignment.Center;
            wrapPanelArea.Width = CELL_SIZE * gameplayGridSize;
            wrapPanelArea.Height = CELL_SIZE * gameplayGridSize;
            wrapPanelArea.Children.Clear();
            buttons.Clear();

            for (int i = 0; i < gameplayGridSize * gameplayGridSize; i++)
            {
                Button button = new Button();
                button.Width = CELL_SIZE;
                button.Height = CELL_SIZE;
                button.FontSize = 24;
                button.Click += Button_Click;
                buttons.Add(button);

                wrapPanelArea.Children.Add(button);
            }

            grid.Children.Add(wrapPanelArea);

            area = new string[gameplayGridSize, gameplayGridSize];
            isGameFinished = false;
        }

        public bool PutMark(object element, int gameplayGridSize)
        {
            int index = buttons.IndexOf((Button)element);
            int i = index / gameplayGridSize;
            int j = index % gameplayGridSize;


            if (area.Length > index 
                && area[i, j] != PLAYER_ONE_MARKER 
                && area[i, j] != PLAYER_TWO_MARKER)
            {
                ((Button)element).Content = currentMarker;
                area[i, j] = currentMarker;
                leastMarkerIndex = index;

                return true;
            }
            else
            {
                return false;
            }
        }

        public void SwitchTurn()
        {
            if(currentMarker == PLAYER_ONE_MARKER)
            {
                currentMarker = PLAYER_TWO_MARKER;
                labelTurn.Content = "Turn: Player Two " + PLAYER_TWO_MARKER;
            }
            else
            {
                currentMarker = PLAYER_ONE_MARKER;
                labelTurn.Content = "Turn: Player One " + PLAYER_ONE_MARKER;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(!isGameFinished && PutMark(e.Source, Settings.GameplayGridSize))
            {
                

                if (!CheckWinner())
                {
                    if (isAi)
                    {
                        SwitchTurn();
                        aIMove();
                        if (CheckWinner())
                        {
                            EndGame();
                            return;
                        }
                    }
                    SwitchTurn();
                }
                else
                {
                    EndGame();
                }
            }
        }

        private bool isWinner(string marker, int markerToWin, int gameplayGridSize, int markerIndex)
        {
            bool win = false;
            int win_index = 0;
            
            int counter = 0;
            int x_offset = 0;
            int y_offset = 0;

            int x = markerIndex % gameplayGridSize;
            int y = markerIndex / gameplayGridSize;

            //Horizontal
            for (int i = 0; i < gameplayGridSize; i++)
            {
                if (area[y, i] == marker)
                {
                    counter++;

                    if (counter == markerToWin)
                    {
                        return true;
                    }
                }
                else
                {
                    counter = 0;
                }
            }

            counter = 0;

            //Vertical
            for (int i = 0; i < gameplayGridSize; i++)
            {
                if (area[i, x] == marker)
                {
                    counter++;

                    if (counter == markerToWin)
                    {
                        return true;
                    }
                }
                else
                {
                    counter = 0;
                }
            }

            counter = 0;

            //Cross Left-Right
            if (x > y)
            {
                x_offset = x - y;
                y_offset = 0;
            }
            else
            {
                x_offset = 0;
                y_offset = y-x;
            }
            
            for (int i = 0; i < gameplayGridSize; i++)
            {
                if(x_offset +  i >= gameplayGridSize || y_offset + i >= gameplayGridSize)
                {
                    break;
                }

                if (area[y_offset + i, x_offset + i] == marker)
                {
                    counter++;

                    if (counter == markerToWin)
                    {
                        return true;
                    }
                }
                else
                {
                    counter = 0;
                }
            }

            counter = 0;

            // Cross Right-Left
            if (x + y <= gameplayGridSize - 1)
            {
                x_offset = x + y;
                y_offset = 0;
                
            }
            else
            {
                x_offset = gameplayGridSize - 1;
                y_offset = y - ( gameplayGridSize - 1 - x);
            }

            for (int i = 0; i < gameplayGridSize; i++)
            {
                if (x_offset - i < 0 || y_offset + i >= gameplayGridSize)
                {
                    break;
                }

                if (area[y_offset + i, x_offset - i] == marker)
                {
                    counter++;

                    if (counter == markerToWin)
                    {
                        return true;
                    }
                }
                else
                {
                    counter = 0;
                }
            }

            return win;
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(e.NewValue != null && (bool)e.NewValue == true)
            {
                if(isAi)
                {
                    labelGameType.Content = "Player vs Computer";
                }
                else
                {
                    labelGameType.Content = "Player vs Player";
                }
                
                labelTurn.Content = "Turn: Player One " + PLAYER_ONE_MARKER;
                labelPlayerOneWin.Visibility = Visibility.Collapsed;
                labelPlayerTwoWin.Visibility = Visibility.Collapsed;
                buttonReset.Visibility = Visibility.Collapsed;

                GenerateGrid(gridMain, Settings.GameplayGridSize);
            }
        }

        private void buttonReset_Click(object sender, RoutedEventArgs e)
        {
            labelTurn.Content = "Turn: Player One " + PLAYER_ONE_MARKER;
            labelPlayerOneWin.Visibility = Visibility.Collapsed;
            labelPlayerTwoWin.Visibility = Visibility.Collapsed;
            buttonReset.Visibility = Visibility.Collapsed;
            currentMarker = PLAYER_ONE_MARKER;
            GenerateGrid(gridMain, Settings.GameplayGridSize);
        }

        private void buttonMainMenu_Click(object sender, RoutedEventArgs e)
        {
            mainWindowView.FrameHost.Navigate(mainWindowView.MainMenuView);
        }

        private void aIMove()
        {
            int bestMoveIndex = 0;
            int bestEnemyMoveIndex = 0;
            int bestScore = -10;
            int tempIndex = 0;

            for(int i = 0; i < Settings.GameplayGridSize; i++)
            {
                for (int j = 0; j < Settings.GameplayGridSize; j++)
                {
                    if (area[i,j] == null)
                    {
                        tempIndex = i * Settings.GameplayGridSize + j;

                        area[i, j] = PLAYER_TWO_MARKER;
                        if (isWinner(PLAYER_TWO_MARKER, Settings.MarksToWin, Settings.GameplayGridSize, tempIndex))
                        {
                            area[i, j] = null;
                            if(bestScore < 10)
                            {
                                bestMoveIndex = tempIndex;
                                bestScore = 10;
                            }
                        }

                        area[i, j] = PLAYER_ONE_MARKER;
                        if (isWinner(PLAYER_ONE_MARKER, Settings.MarksToWin, Settings.GameplayGridSize, tempIndex))
                        {
                            area[i, j] = null;
                            if (bestScore < 10)
                            {
                                bestMoveIndex = tempIndex;
                                bestScore = 10;
                            }
                        }
                        area[i, j] = null;

                        if (bestScore < 0)
                        {
                            bestMoveIndex = tempIndex;
                            bestScore = 0;
                        }
                    }
                }
            }

            if(bestScore <= 0)
            {
                if(area[1, 1] == null)
                {
                    bestMoveIndex = 4;
                }
            }

            PutMark(buttons[bestMoveIndex], Settings.GameplayGridSize);
        }
    }
}
