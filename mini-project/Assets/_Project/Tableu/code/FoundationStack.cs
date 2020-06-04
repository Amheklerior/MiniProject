using UnityEngine;

namespace Amheklerior.Solitaire {

    public class FoundationStack : CardStackComponent, ICardDropArea {

        [SerializeField] private Seed _seed;
        
        public void Drop(Card card) => Put(card);

        public bool ValidDropPositionFor(Card card) => card.Seed == _seed &&
            ((!HasCards && card.Number == Number.A) || TopCard.IsNextLowNumber(card));
        
    }

}
