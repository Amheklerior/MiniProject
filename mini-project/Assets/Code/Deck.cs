using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Amheklerior.Solitaire {
    
    [CreateAssetMenu(menuName = "Solitaire/Deck")]
    public class Deck : ScriptableObject {

        [SerializeField] private CardImageProvider _cardImageProvider;

        public Action<Card> OnCardCreated;

        public static readonly int CARDS_IN_DECK = 52;
        public static readonly int CARDS_PER_SEED = 13;

        public ICollection<Card> Cards => _cards;

        public void GenerateCards() {
            Seed seed;
            Number number;
            for (int i = 0; i < CARDS_IN_DECK; i++) {
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

        private Card[] _cards = new Card[CARDS_IN_DECK];

        private Card CreateCard(Seed seed, Number number) {
            var cardObj = new GameObject($"{number} of {seed}");
            cardObj.SetActive(false);
            var card = cardObj.AddComponent<Card>();
            card.Init(seed, number, _cardImageProvider);
            OnCardCreated?.Invoke(card);
            return card;
        }

        private static void GetSeedAndNumberByIndex(int index, out Seed seed, out Number number) {
            seed = (Seed) (index / CARDS_PER_SEED);
            number = (Number) (index % CARDS_PER_SEED);
        }

        #endregion

    }
}
