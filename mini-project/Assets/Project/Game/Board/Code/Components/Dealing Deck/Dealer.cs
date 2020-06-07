using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Amheklerior.Solitaire {

    [RequireComponent(typeof(Deck))]
    public class Dealer : MonoBehaviour {
        
        [Header("Settings:")]
        [SerializeField] private float _dealingAnimationTime = 0.05f;
        [SerializeField] private CardGenerator _cardGenerator;
        [SerializeField] private TableuPile[] _tableuPiles;
        

        void Awake() {
            CheckDependencies();
            InitDeck();
        }

        void Start() {
            ClearTable();
            ShuffleDeck();
            PlaceDeck();
            DealCards();
        }


        #region Internals

        private Deck _deck;
        private ICollection<CardController> _cards;

        public void ShuffleDeck() => _cards = _cards.OrderBy(card => UnityEngine.Random.value).ToArray();

        private void CheckDependencies() {
            if (!_cardGenerator) {
                Debug.LogError("No card generator has been found.", this);
                throw new MissingReferenceException();
            }
            if (_tableuPiles == null || _tableuPiles.Length == 0) {
                Debug.LogError("Tableu piles not set.", this);
                throw new MissingReferenceException();
            }
        }

        [ContextMenu("Initialize Deck")]
        private void InitDeck() {
            _cards = _cardGenerator.GenerateCards();
            _deck = GetComponent<Deck>();
        }

        [ContextMenu("Place Deck")]
        private void PlaceDeck() => _deck.PutAll(_cards);

        private void ClearTable() {
            _deck.Clear();
            foreach (var pile in _tableuPiles) pile.Clear();
        }
        
        [ContextMenu("Deal Cards")]
        private void DealCards() => StartCoroutine(DealCards_Coroutine());

        [ContextMenu("Deal Faced-Down Cards")]
        private void DealFacedDownCards() => StartCoroutine(DealFacedDownCards_Coroutine());

        [ContextMenu("Deal Faced-Up Cards")]
        private void DealFacedUpCards() => StartCoroutine(DealFacedUpCards_Coroutine());

        #region Coroutines 

        private IEnumerator DealCards_Coroutine() {
            PlaceDeck();
            yield return new WaitForSeconds(0.1f);
            DealFacedDownCards();
        }
        
        private IEnumerator DealFacedDownCards_Coroutine() {
            var WaitForAnimationToComplete = new WaitForSeconds(_dealingAnimationTime);
            for (int i = 0; i < _tableuPiles.Length - 1; i++)
                for (int j = i + 1; j < _tableuPiles.Length; j++) {
                    _tableuPiles[j].Put(_deck.Take());
                    yield return WaitForAnimationToComplete;
                }
            DealFacedUpCards();
        }

        private IEnumerator DealFacedUpCards_Coroutine() {
            var WaitForAnimationToComplete = new WaitForSeconds(_dealingAnimationTime);
            foreach (var stack in _tableuPiles) {
                var card = _deck.Take();
                stack.Drop(card);
                card.Flip();
                yield return WaitForAnimationToComplete;
            }
        }

        #endregion

        #endregion

    }
}
