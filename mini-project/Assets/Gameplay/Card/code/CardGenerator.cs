using System.Collections.Generic;
using UnityEngine;

namespace Amheklerior.Solitaire {

    [CreateAssetMenu(menuName = "Solitaire/Card/Generator")]
    public class CardGenerator : ScriptableObject {

        public static readonly int CARDS_COUNT = 52;
        public static readonly int CARDS_PER_SEED = 13;
        
        [SerializeField] private GameObject _cardPrototype;
        
        public ICollection<Card> GenerateCards() {
            Seed seed;
            Number number;
            var cards = new Card[CARDS_COUNT];

            for (int i = 0; i < CARDS_COUNT; i++) {
                GetSeedAndNumberByIndex(i, out seed, out number);
                cards[i] = CreateCard(seed, number);
            }

            return cards;
        }


        #region Internals

        private void OnEnable() {
            if (!_cardPrototype) {
                Debug.LogError("The card prototype reference is not set.", _cardPrototype);
                throw new MissingReferenceException();
            }
        }

        private Card CreateCard(Seed seed, Number number) {
            var card = Instantiate(_cardPrototype).GetComponent<Card>();
            card.name = $"{number} of {seed}";
            card.Deactivate();
            card.Init(seed, number);
            return card;
        }

        private static void GetSeedAndNumberByIndex(int index, out Seed seed, out Number number) {
            seed = (Seed) (index / CARDS_PER_SEED);
            number = (Number) (index % CARDS_PER_SEED);
        }

        #endregion

    }
}
