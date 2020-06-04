using UnityEngine;

namespace Amheklerior.Solitaire {

    public class TalonStack : CardStackComponent {

        private Vector3 NextCardPosition => (Vector3) _stackPosition + _stackDirection * _stack.CardCount;

        protected override void OnPut(Card card) {
            base.OnPut(card);
            card.MoveTo(NextCardPosition);
            card.Flip();
        }
        
    }
}
