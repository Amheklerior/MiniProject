using System.Collections.Generic;
using UnityEngine;

namespace Amheklerior.Solitaire {
    
    [CreateAssetMenu(menuName = "Solitaire/CardImageProvider")]
    public class CardImageProvider : ScriptableObject {

        #region Inspector Interface
        [SerializeField] private Sprite[] _heartsCards = new Sprite[Deck.CARDS_PER_SEED];
        [SerializeField] private Sprite[] _clubsCards = new Sprite[Deck.CARDS_PER_SEED];
        [SerializeField] private Sprite[] _squaresCards = new Sprite[Deck.CARDS_PER_SEED];
        [SerializeField] private Sprite[] _spadesCards = new Sprite[Deck.CARDS_PER_SEED];
        [SerializeField] private Sprite _back;
        #endregion

        private Dictionary<Seed, Sprite[]> _sprites; 

        private void OnEnable() => _sprites = new Dictionary<Seed, Sprite[]>() {
            [Seed.HEARTS] = _heartsCards,
            [Seed.CLUBS] = _clubsCards,
            [Seed.SQUARES] = _squaresCards,
            [Seed.SPADES] = _spadesCards
        };

        public Sprite GetFrontImageFor(Seed seed, Number number) => _sprites[seed][(int) number];

        public Sprite BackImage => _back;

    }
}
