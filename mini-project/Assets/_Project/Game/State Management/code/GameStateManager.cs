using UnityEngine;
using Amheklerior.Solitaire.Util;

namespace Amheklerior.Solitaire {

    public class GameStateManager : MonoBehaviour {

        #region Score

        [SerializeField] private IntVariable _score;

        private Timer _timer;

        private void Awake() => _timer = new Timer(10f, () => UpdateScoreBy((int) GameScore.TIME_ELAPSED));

        private void Start() => _timer.Start();

        private void LateUpdate() => _timer.Tick(Time.deltaTime);
        
        public void UpdateScoreBy(int value) => 
            _score.CurrentValue = (int) Mathf.Clamp(_score.CurrentValue += value, 0, Mathf.Infinity);
        
        #endregion

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
