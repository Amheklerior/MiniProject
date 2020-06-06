using UnityEngine;

namespace Amheklerior.Solitaire {

    public static class Game {

        public static bool IsBusy => StateManager && StateManager.IsGameBusy;

        public static void StartAction() => StateManager?.IncrementGameBusyIndicator();
        public static void EndAction() => StateManager?.DecrementGameBusyIndicator();

        #region Internals

        private static GameStateManager _stateManager;

        public static GameStateManager StateManager => _stateManager ??
            (_stateManager = Object.FindObjectOfType<GameStateManager>());
        
        #endregion

    }
}
