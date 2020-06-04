using System;
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
                if (value) base.OnPut(value);
                _playableCard = value;
            }
        }

        public override Card TopCard {
            get {
                if (PlayableCard) return TopPlayebleCard;
                else return base.TopCard;
            }
        }

        protected override Vector3 Direction => new Vector3(0f, _verticalOffset, _depthOffset);

        private Card TopPlayebleCard {
            get {
                var card = PlayableCard;
                //while (card.Next != null) card = card.Next;
                return card;
            }
        }

        protected override void OnPut(Card card) {
            base.OnPut(card);
            UpdateColliderPosition(card);
            
        }

        protected override void OnTake(Card card) {
            base.OnPut(card);
            UpdateColliderPosition(card);
        }

        private void UpdateColliderPosition(Card card) =>
            _collider.position = Vector3.zero; //_stackPosition + _stackDirection * _stack.CardCount + _stackDirection * 0.5f;

    }
}
