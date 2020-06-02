using System.Collections.Generic;

namespace Amheklerior.Solitaire {

    public class DealingDeckStack : CardStackComponent {

        protected override void OnPut(Card card) {
            base.OnPut(card);
            card.Flip();
        }

        protected override void OnTake(Card card) {
            base.OnTake(card);
            TopCard.gameObject.SetActive(true);
        }

        public void SetCards(ICollection<Card> cards) => _stack.PutAll(cards);

        protected override void OnPutAll(ICollection<Card> cards) {
            foreach (var card in cards)
                card.transform.position = _stackPosition;
            _stack.TopCard.gameObject.SetActive(true);
        }

    }
}
