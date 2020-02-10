using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TicTacToe.View
{
    public interface IMainWindowView
    {
        Frame FrameHost { get; }

        IMainMenuView MainMenuView { get; }

        IGameView GameView { get; }

        IOptionsView OptionsView { get; }
}
}
