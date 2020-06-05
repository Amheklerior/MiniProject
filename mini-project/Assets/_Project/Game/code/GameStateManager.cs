using UnityEngine;

namespace Amheklerior.Solitaire {

    public class GameStateManager : MonoBehaviour {

        public bool IsGameBusy => _busyIndicator > 0;

        [ContextMenu("Increment Game Busy Indicator")]
        public void IncrementGameBusyIndicator() => _busyIndicator++;

        [ContextMenu("Decrement Game Busy Indicator")]
        public void DecrementGameBusyIndicator() => _busyIndicator--;

        #region Internals

        [SerializeField] private int _busyIndicator = 0;

        #endregion

    }
}
