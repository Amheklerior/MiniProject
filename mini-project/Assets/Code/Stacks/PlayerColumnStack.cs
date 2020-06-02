using UnityEngine;

namespace Amheklerior.Solitaire {

    public class PlayerColumnStack : CardStackComponent {

        #region Inspector interface 

        [Header("Dependencies:")]
        [SerializeField] private Transform _collider;

        [Space, Header("Animation Settings:")]
        [SerializeField] protected float _verticalOffset = -0.2f;
        [SerializeField] protected float _depthOffset = -0.1f;

        #endregion
        
        private Card _playableCard;
        public Card PlayableCard {
            get => _playableCard;
            set {
                OnPut(value);
                _playableCard = value;
            }
        }

        protected override Vector3 Direction => new Vector3(0f, _verticalOffset, _depthOffset);

        protected override void Init() {
            base.Init();
            _collider.Translate(Vector3.down * _verticalOffset);
        }

        protected override void OnPut(Card card) {
            base.OnPut(card);
            _collider.Translate(Vector3.up * _verticalOffset);
        }
        
        protected override void OnTake(Card card) {
            base.OnPut(card);
            _collider.Translate(Vector3.down * _verticalOffset);
        }
        
    }
}
