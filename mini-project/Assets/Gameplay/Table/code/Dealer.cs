using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Amheklerior.Solitaire {

    [RequireComponent(typeof(Deck))]
    public class Dealer : MonoBehaviour {

        #region Inspector interface 

        [Header("Settings:")]
        [SerializeField] private CardGenerator _cardGenerator;
        
        /*
        [SerializeField] private PlayerColumnStack[] _playerColumns;
        [Space, Header("Animation Settings:")]
        [SerializeField] private float _dealingAnimationTime = 0.05f;
        */

        #endregion
        
        void Awake() {
            CheckDependencies();
            InitDeck();
        }

        void Start() {
            //ClearTable();
            ShuffleDeck();
            PlaceDeck();
            //DealCards();
        }


        #region Internals

        private Deck _deck;
        private ICollection<Card> _cards;

        public void ShuffleDeck() => _cards = _cards.OrderBy(card => UnityEngine.Random.value).ToArray();

        private void CheckDependencies() {
            if (!_cardGenerator) {
                Debug.LogError("No card generator has been found.", _cardGenerator);
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


        /*
        private void ClearTable() {
            _dealingStack.Clear();
            foreach (var playerColumn in _playerColumns)
                playerColumn.Clear();
        }

        private void CheckDependencies() {
            if (!_dealingStack) {
                Debug.LogError("Dealing deck reference is not set.", _dealingStack);
                throw new MissingReferenceException();
            }
            if (_playerColumns == null || _playerColumns.Length == 0) {
                Debug.LogError("Player column references are not set.", this);
                throw new MissingReferenceException();
            }
        }
        */

        /*
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
            for (int i = 0; i < _playerColumns.Length - 1; i++)
                for (int j = i + 1; j < _playerColumns.Length; j++) {
                    _playerColumns[j].Put(_dealingStack.Take());
                    yield return WaitForAnimationToComplete;
                }
            DealFacedUpCards();
        }

        private IEnumerator DealFacedUpCards_Coroutine() {
            var WaitForAnimationToComplete = new WaitForSeconds(_dealingAnimationTime);
            foreach (var stack in _playerColumns) {
                stack.PlayableCard = _dealingStack.Take();
                stack.PlayableCard.Flip();
                yield return WaitForAnimationToComplete;
            }
        }

        #endregion
        */
        #endregion

    }
}
