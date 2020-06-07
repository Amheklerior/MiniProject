using UnityEngine;
using Amheklerior.Core.Time;

namespace Amheklerior.Solitaire {

    public class CardAutostack : MonoBehaviour {

        [Header("Settings:")]
        [SerializeField] private float _doubleClickTimeInterval;
        [SerializeField] private CardEvent _autostackEvent;

        private void OnMouseUpAsButton() {
            _clicksCount++;
            if (IsFirstClick) _timer.Start();
            else if (_timer.Current < _doubleClickTimeInterval) {
                _autostackEvent.Raise(_card);
                ResetClickCount();
                _timer.Stop();
            }
        }

        #region Internals 

        private CardController _card;
        private Timer _timer;
        private FoundationStack _myStack;
        private int _clicksCount;

        private bool IsFirstClick => _clicksCount == 1;
        
        private void Awake() {
            _card = GetComponent<CardController>();
            InitTimer();
        }

        private void LateUpdate() => _timer.Tick(Time.deltaTime);

        private void ResetClickCount() => _clicksCount = 0;

        private void InitTimer() => _timer = new Timer(_doubleClickTimeInterval, () => {
            ResetClickCount();
            _timer.Stop();
        });
        
        #endregion
        
    }
}
