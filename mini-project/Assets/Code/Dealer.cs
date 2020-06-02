using System.Collections;
using UnityEngine;

namespace Amheklerior.Solitaire {

    public class Dealer : MonoBehaviour {

        [SerializeField] private float _dealingAnimationTime = 0.05f;
        [SerializeField] private GameObject _cardPrototype;

        // Table?
        [SerializeField] private DealingDeckStack _dealingStack;
        [SerializeField] private PlayerColumnStack[] _playerColumns;

        private void Awake() {
            if (!_dealingStack) {
                Debug.LogError("Dealing deck reference is not set.", _dealingStack);
                throw new MissingReferenceException();
            }
            if (_playerColumns == null || _playerColumns.Length == 0) {
                Debug.LogError("Player column references are not set.", this);
                throw new MissingReferenceException();
            }
            CreateDeck();
        }

        private void Start() {
            ClearTable();
            ShuffleDeck();
            DealCards();
        }


        #region Internals

        private Deck _deck;

        [ContextMenu("Generate cards")]
        private void CreateDeck() {
            var transform = gameObject.transform;
            _deck = new Deck(_cardPrototype, card => card.transform.parent = transform);
            _deck.GenerateCards();
        }
        
        private void ShuffleDeck() => _deck.Shuffle();
        


        // Table?
        private void ClearTable() {
            _dealingStack.Clear();
            foreach (var playerColumn in _playerColumns)
                playerColumn.Clear();
        }

        // Table?
        private void DealCards() => StartCoroutine(DealCards_Coroutine());

        // Table?
        private IEnumerator DealCards_Coroutine() {
            PlaceDeck();
            yield return new WaitForSeconds(0.1f);
            DealFacedDownCards();
        }

        // Table?
        [ContextMenu("Place Deck")]
        private void PlaceDeck() => _dealingStack.PutAll(_deck.Cards);


        [ContextMenu("Deal Faced-Down Cards")]
        private void DealFacedDownCards() => StartCoroutine(DealFacedDownCards_Coroutine());

        private IEnumerator DealFacedDownCards_Coroutine() {
            var WaitForAnimationToComplete = new WaitForSeconds(_dealingAnimationTime);
            for (int i = 0; i < _playerColumns.Length - 1; i++)
                for (int j = i + 1; j < _playerColumns.Length; j++) {
                    PlaceOnTable(_playerColumns[j], _dealingStack.Take(), CardData.FACING_DOWN);
                    yield return WaitForAnimationToComplete;
                }
            //DealFacedUpCards();
        }

        [ContextMenu("Deal Faced-Up Cards")]
        private void DealFacedUpCards() => StartCoroutine(DealFacedUpCards_Coroutine());

        private IEnumerator DealFacedUpCards_Coroutine() {
            var WaitForAnimationToComplete = new WaitForSeconds(_dealingAnimationTime);
            foreach (var stack in _playerColumns) {
                PlaceOnTable(stack, _dealingStack.Take(), CardData.FACING_UP);
                yield return WaitForAnimationToComplete;
            }
        }
        
        private static void PlaceOnTable(CardStackComponent stack, Card card, bool facingUp) {
            stack.Put(card);
            if (card.IsFacingUp != facingUp) card.Flip();
        }

        #endregion

    }

}
