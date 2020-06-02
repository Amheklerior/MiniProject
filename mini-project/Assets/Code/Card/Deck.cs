using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Amheklerior.Solitaire {

    public class Deck {

        public static readonly int CARDS_COUNT = 52;
        public static readonly int CARDS_PER_SEED = 13;

        public ICollection<Card> Cards => _cards;

        public Deck(GameObject cardPrototype, Action<Card> onCardCreated = null) {
            _cardPrototype = cardPrototype;
            _onCardCreated = onCardCreated;
        }

        public void GenerateCards() {
            Seed seed;
            Number number;
            for (int i = 0; i < CARDS_COUNT; i++) {
                GetSeedAndNumberByIndex(i, out seed, out number);
                _cards[i] = CreateCard(seed, number);
            }
        }

        public void Shuffle() =>
            _cards = _cards.OrderBy(card => {
                card.IsFacingUp = false;
                return UnityEngine.Random.value;
            }).ToArray();



        #region Internals

        private readonly Action<Card> _onCardCreated;
        private readonly GameObject _cardPrototype;

        private Card[] _cards = new Card[CARDS_COUNT];

        private Card CreateCard(Seed seed, Number number) {
            var cardObj = UnityEngine.Object.Instantiate(_cardPrototype);
            cardObj.name = $"{number} of {seed}";
            cardObj.SetActive(false);

            var card = cardObj.GetComponent<Card>();
            card.SetCardData(new CardData(seed, number));

            _onCardCreated?.Invoke(card);
            return card;
        }

        private static void GetSeedAndNumberByIndex(int index, out Seed seed, out Number number) {
            seed = (Seed) (index / CARDS_PER_SEED);
            number = (Number) (index % CARDS_PER_SEED);
        }

        #endregion

    }
}
