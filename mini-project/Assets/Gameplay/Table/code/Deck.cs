using System.Collections.Generic;
using UnityEngine;

namespace Amheklerior.Solitaire {

    public class Deck : CardStackComponent {

        private Vector3 NextCardPosition => (Vector3) _stackPosition + _stackDirection * _stack.CardCount;

        protected override void OnPut(Card card) {
            base.OnPut(card);
            card.MoveTo(NextCardPosition);
            card.Flip();
        }

        protected override void OnTake(Card card) {
            base.OnTake(card);
            card.transform.Translate(Vector3.back * 3f);
            if (_stack.HasCards) TopCard.Activate();
        }

        protected override void OnPutAll(ICollection<Card> cards) {
            base.OnPutAll(cards);
            foreach (var card in cards) {
                card.transform.position = _stackPosition;
                card.Hide();
            }
            _stack.TopCard.Activate();
        }

    }
}
