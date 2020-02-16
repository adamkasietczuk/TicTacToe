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
    /// Interaction logic for MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page, IMainMenuView
    {
        private IMainWindowView mainWindowView;

        public MainMenuPage(IMainWindowView mainWindowView)
        {
            this.mainWindowView = mainWindowView;

            InitializeComponent();
        }

        private void buttonPlayerVsPlayer_Click(object sender, RoutedEventArgs e)
        {
            mainWindowView.GameView.SetAi(false);
            mainWindowView.FrameHost.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            mainWindowView.FrameHost.Navigate(mainWindowView.GameView);
        }

        private void buttonPlayerVsComputer_Click(object sender, RoutedEventArgs e)
        {
            mainWindowView.GameView.SetAi(true);
            mainWindowView.FrameHost.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            mainWindowView.FrameHost.Navigate(mainWindowView.GameView);
        }

        private void buttonOptions_Click(object sender, RoutedEventArgs e)
        {
            mainWindowView.FrameHost.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            mainWindowView.FrameHost.Navigate(mainWindowView.OptionsView);
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
