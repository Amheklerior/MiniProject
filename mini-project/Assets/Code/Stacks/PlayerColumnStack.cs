using UnityEngine;

namespace Amheklerior.Solitaire {

    public class PlayerColumnStack : CardStackComponent {

        [Header("Dependencies:")]
        [SerializeField] private Transform _collider;

        [Space, Header("Animations:")]
        [SerializeField] protected float _verticalOffset = -0.2f;
        [SerializeField] protected float _depthOffset = -0.1f;


        public Card PlayebleCardPile { get; set; } // TODO Change type to CardPile

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
