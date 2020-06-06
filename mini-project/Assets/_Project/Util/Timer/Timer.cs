using System;

namespace Amheklerior.Solitaire.Util {
    
    public class Timer {

        public float Current { get; private set; }

        public bool IsRunning { get; private set; }

        public Timer(float? expiryTime = null, Action OnTimeExpired = null) {
            if (expiryTime != null && expiryTime <= 0)
                throw new ArgumentException($"The expiry time must be greater than zero. Was {expiryTime} instead.");

            _expiryTime = expiryTime;
            _onTimeExpired = OnTimeExpired;
        }

        public void Start() => IsRunning = true;

        public void Pause() => IsRunning = false;

        public void Resume() => IsRunning = true;

        public void Stop() {
            Current = 0;
            IsRunning = false;
        }

        public void Restart() {
            Current = 0;
            IsRunning = true;
        }

        public void Tick(float elapsed) {
            if (!IsRunning || Expired) return;
            Current += elapsed;
            if (Expired) {
                _onTimeExpired?.Invoke();
                Restart();
            }
        }

        #region Internals 

        private readonly Action _onTimeExpired;
        private readonly float? _expiryTime;
        private bool Expired => _expiryTime != null && Current >= _expiryTime;

        #endregion

    }
}
