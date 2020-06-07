using UnityEngine;

namespace Amheklerior.Solitaire {

    public enum GameScore {
        NO_SCORE = 0,
        MOVE_CARD_TO_FOUNDATION_STACK = 10,
        MOVE_CARD_FROM_TALON_TO_TABLEU_PILE = 5,
        MOVE_CARD_BETWEEN_TABLEU_PILES = 3,
        TIME_ELAPSED = -2,
        MOVE_CARD_FROM_FOUNDATION_STACK_TO_TABLEU_PILE = -15,
        RESET_DECK = -100
    }

    public static class Game {

        public static bool IsBusy => StateManager.IsGameBusy;

        public static void StartAction() => StateManager.IncrementGameBusyIndicator();

        public static void EndAction() => StateManager.DecrementGameBusyIndicator();
        
        public static void IncrementMovesCounter() => StateManager.IncrementMovesCounter();
        
        public static void UpdateScoreBy(int value) => StateManager.UpdateScoreBy(value);

        public static void UpdateScoreBy(GameScore scoringMove) => StateManager.UpdateScoreBy((int) scoringMove);
        
        #region Internals

        private static GameStateManager _stateManager;

        public static GameStateManager StateManager => _stateManager ??
            (_stateManager = Object.FindObjectOfType<GameStateManager>());
        
        #endregion

    }
}
