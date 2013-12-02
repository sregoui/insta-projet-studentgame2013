using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TooLate
{
    public static class ScreenState
    {
        #region Fields

        /// <summary>
        /// Enumerates all game's screens.
        /// </summary>
        public enum GameState
        {
            SPLASH_SCREEN,
            MENU,
            INGAME_1
        }

        private static GameState _currentGameState = GameState.SPLASH_SCREEN;

        #endregion

        #region Properties

        /// <summary>
        /// Get, set the current game state.
        /// </summary>
        public static GameState CurrentGameState
        {
            get { return _currentGameState; }
            set { _currentGameState = value; }
        }

        #endregion
    }
}
