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
    /// Interaction logic for OptionsPage.xaml
    /// </summary>
    public partial class OptionsPage : Page, IOptionsView
    {
        private IMainWindowView mainWindowView;

        public OptionsPage(IMainWindowView mainWindowView)
        {
            this.mainWindowView = mainWindowView;

            InitializeComponent();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            int gameplayGridSize = 0;
            int marksToWin = 0;

            if(!int.TryParse(textBoxGameplayGridSize.Text, out gameplayGridSize))
            {
                MessageBox.Show("Can't save new option, please check you input");
                return;
            }

            if (!int.TryParse(textBoxMarksToWin.Text, out marksToWin))
            {
                MessageBox.Show("Can't save new option, please check you input");
                return;
            }

            if (gameplayGridSize < 3 || gameplayGridSize > 10 
                || marksToWin < 3 || marksToWin > 10)
            {
                MessageBox.Show("Can't save new option, please check you input");
                return;
            }

            Settings.GameplayGridSize = gameplayGridSize;
            Settings.MarksToWin = marksToWin;

            mainWindowView.FrameHost.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            mainWindowView.FrameHost.Navigate(mainWindowView.MainMenuView);
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            mainWindowView.FrameHost.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            mainWindowView.FrameHost.Navigate(mainWindowView.MainMenuView);
        }
    }
}
