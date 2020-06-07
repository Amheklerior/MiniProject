using UnityEngine;
using Amheklerior.Core.EventSystem;

namespace Amheklerior.Solitaire {
    
    public class FoundationStack : CardStackComponent, IDragDropOrigin, IDragDropDestination {

        [Header("Settings:")]
        [SerializeField] private Seed _seed;
        [SerializeField] private GameEvent _stackCompletedEvent;

        public void Drop(CardController card) {
            Put(card);
            card.Pile.DetachPrevious();
            if (CardCount == Card.CARDS_PER_SEED)
                _stackCompletedEvent.Raise();
        }

        public void UndoDrop(CardController card) => Take();

        public bool ValidDropPositionFor(CardController card) => 
            card.Seed == _seed && 
            !card.IsAPile() && (
                (!HasCards && card.Number == Number.A) || 
                (HasCards && TopCard.IsNextLowNumber(card))
            );
        
    }
}
