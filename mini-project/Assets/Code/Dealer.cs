using System;
using System.Collections;
using UnityEngine;

namespace Amheklerior.Solitaire {

    public class Dealer : MonoBehaviour {
        
        [SerializeField] private Deck _deck;
        [SerializeField] private Transform _dealingDeckTransform;
        [SerializeField] private Transform _pickupTransform;
        [SerializeField] private Transform[] _playingColumnTransforms;
        
        private DealingStack _dealingDeck;
        private CardStack _pickUpStack;
        private PlayingStack[] _playingColumns;

        private void Awake() {
            CreateDeck();
            Init();
        }

        private void Start() {
            ClearTable();
            ShuffleDeck();
            DealCards();
        }

        private void OnMouseUpAsButton() {
            if (_dealingDeck.HasCards)
                PlaceOnTable(_pickUpStack, _dealingDeck.Take(), Card.FACING_UP);
            else MoveBetweenStacks(_pickUpStack, _dealingDeck);
        }

        // TOTO --  find better place?
        private static void MoveBetweenStacks(CardStack origin, CardStack destination) {
            while (origin.HasCards) destination.Put(origin.Take());
        }


        #region Internals

        private static readonly float DEALING_ANIMATION_TIME = 0.05f;

        private Transform _transform;

        private void Init() {
            //_transform.position = _dealingDeckTransform.position + Vector3.back * 10;
            _dealingDeck = new DealingStack(_dealingDeckTransform.position);
            _pickUpStack = new CardStack(_pickupTransform.position);
            _playingColumns = new PlayingStack[_playingColumnTransforms.Length];
            for (int i = 0; i < _playingColumns.Length; i++)
                _playingColumns[i] = new PlayingStack(_playingColumnTransforms[i].position);
        }
        
        private void CreateDeck() {
            _transform = transform;
            _deck.OnCardCreated += card => {
                card.transform.parent = _transform;
                card.transform.position = _dealingDeckTransform.position;
            };
            _deck.GenerateCards();
        }

        private void ShuffleDeck() => _deck.Shuffle();

        private void ClearTable() {
            _dealingDeck.Clear();
            foreach (PlayingStack column in _playingColumns)
                column.Clear();
        }

        [ContextMenu("Place Deck")]
        private void DealCards() => StartCoroutine(DealCards_Coroutine());

        private void PlaceDeck() => _dealingDeck.SetCards(_deck.Cards);

        private IEnumerator DealCards_Coroutine() {
            PlaceDeck();
            yield return new WaitForSeconds(0.1f);
            DealFacedDownCards();
        }

        [ContextMenu("Deal Faced-Down Cards")]
        private void DealFacedDownCards() => StartCoroutine(DealFacedDownCards_Coroutine());

        private IEnumerator DealFacedDownCards_Coroutine() {
            var WaitForAnimationToComplete = new WaitForSeconds(DEALING_ANIMATION_TIME);
            for (int i = 0; i < _playingColumns.Length - 1; i++)
                for (int j = i + 1; j < _playingColumns.Length; j++) {
                    PlaceOnTable(_playingColumns[j], _dealingDeck.Take(), Card.FACING_DOWN);
                    yield return WaitForAnimationToComplete;
                }
            DealFacedUpCards();
        }

        [ContextMenu("Deal Faced-Up Cards")]
        private void DealFacedUpCards() => StartCoroutine(DealFacedUpCards_Coroutine());

        private IEnumerator DealFacedUpCards_Coroutine() {
            var WaitForAnimationToComplete = new WaitForSeconds(DEALING_ANIMATION_TIME);
            foreach (CardStack stack in _playingColumns) { 
                PlaceOnTable(stack, _dealingDeck.Take(), Card.FACING_UP);
                yield return WaitForAnimationToComplete;
            }
        }

        private static void PlaceOnTable(CardStack stack, Card card, bool facingUp) {
            if (card.IsFacingUp != facingUp)
                card.Flip();
            stack.Put(card);
        }
        
        #endregion

    }
}
