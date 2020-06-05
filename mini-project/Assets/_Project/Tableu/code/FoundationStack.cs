﻿using UnityEngine;

namespace Amheklerior.Solitaire {

    public class FoundationStack : CardStackComponent, ICardDragArea, ICardDropArea {

        [SerializeField] private Seed _seed;

        public void Drop(Card card) {
            Put(card);
            card.Pile.DetachPrevious();
        }

        public void UndoDrop(Card card) {
            if (!HasCards) return;
            Take(); // THE ERROR #2 THROWS HERE FOR EMPTY STACK.. ADDED ABOVE GARD CLAUSE FOR TEST
        }

        public bool ValidDropPositionFor(Card card) => 
            card.Seed == _seed && 
            !card.IsAPile() && (
                (!HasCards && card.Number == Number.A) || 
                TopCard.IsNextLowNumber(card)
            );
        
    }
}
