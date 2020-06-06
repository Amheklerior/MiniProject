using UnityEngine;
using Amheklerior.Solitaire.Util;

namespace Amheklerior.Solitaire {

    public class GameStateManager : MonoBehaviour {

        #region Moves Counter 

        [SerializeField] private IntVariable _moves;

        [ContextMenu("Increment Moves Counter")]
        public void IncrementMovesCounter() => _moves.CurrentValue++;

        [ContextMenu("Reset Moves")]
        public void ResetMovesCounter() => _moves.CurrentValue = 0;

        #endregion

        #region Busy indicator

        private int _busyIndicator = 0;

        public bool IsGameBusy => _busyIndicator > 0;

        [ContextMenu("Increment Game Busy Indicator")]
        public void IncrementGameBusyIndicator() => _busyIndicator++;

        [ContextMenu("Decrement Game Busy Indicator")]
        public void DecrementGameBusyIndicator() => _busyIndicator--;

        #endregion

    }

}
