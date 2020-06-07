using System.Collections.Generic;
using UnityEngine;

namespace Amheklerior.Solitaire {

    [CreateAssetMenu(menuName = "Solitaire/Card/Generator")]
    public class CardGenerator : ScriptableObject {

        [SerializeField] private GameObject _cardPrototype;
        
        public ICollection<CardController> GenerateCards() {
            Seed seed;
            Number number;
            var cards = new CardController[Card.CARDS_COUNT];
            _container = new GameObject("Deck");

            for (int i = 0; i < Card.CARDS_COUNT; i++) {
                GetSeedAndNumberByIndex(i, out seed, out number);
                cards[i] = CreateCard(seed, number);
            }

            return cards;
        }


        #region Internals

        private GameObject _container;

        private void OnEnable() {
            if (!_cardPrototype) {
                Debug.LogError("The card prototype reference is not set.", _cardPrototype);
                throw new MissingReferenceException();
            }
        }

        private CardController CreateCard(Seed seed, Number number) {
            var card = Instantiate(_cardPrototype).GetComponent<CardController>();
            card.name = $"{number} of {seed}";
            card.transform.parent = _container.transform;
            card.Deactivate();
            card.Init(seed, number);
            return card;
        }

        private static void GetSeedAndNumberByIndex(int index, out Seed seed, out Number number) {
            seed = (Seed) (index / Card.CARDS_PER_SEED);
            number = (Number) (index % Card.CARDS_PER_SEED);
        }

        #endregion

    }
}
