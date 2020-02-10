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
using TicTacToe.Presenter;
using TicTacToe.View;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindowView
    {
        private readonly MainWindowPresenter windowPresenter;
        private readonly MainMenuPage mainMenuPage;
        private readonly GamePage gamePage;
        private readonly OptionsPage optionsPage;

        public MainWindow()
        {
            InitializeComponent();

            mainMenuPage = new MainMenuPage(this);
            gamePage = new GamePage(this);
            optionsPage = new OptionsPage(this);

            windowPresenter = new MainWindowPresenter(this);
        }

        public Frame FrameHost => frameHost;

        public IMainMenuView MainMenuView => mainMenuPage;

        public IGameView GameView => gamePage;

        public IOptionsView OptionsView => optionsPage;
    }
}
