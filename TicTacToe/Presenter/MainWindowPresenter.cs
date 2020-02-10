using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using TicTacToe.View;

namespace TicTacToe.Presenter
{
    public class MainWindowPresenter
    {
        private readonly IMainWindowView mainWindow;

        public MainWindowPresenter(IMainWindowView mainWindow)
        {
            this.mainWindow = mainWindow;

            mainWindow.FrameHost.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            mainWindow.FrameHost.Navigate(mainWindow.MainMenuView);
        }
    }
}
