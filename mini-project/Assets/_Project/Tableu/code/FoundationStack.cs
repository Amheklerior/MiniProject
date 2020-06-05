using UnityEngine;

namespace Amheklerior.Solitaire {

    public class FoundationStack : CardStackComponent, ICardDropArea {

        [SerializeField] private Seed _seed;

        public void Drop(Card card) {
            Put(card);
            card.Pile.DetachPrevious();
        }

        public bool ValidDropPositionFor(Card card) => 
            card.Seed == _seed && 
            !card.IsAPile() && (
                (!HasCards && card.Number == Number.A) || 
                TopCard.IsNextLowNumber(card)
            );
        
    }
}
