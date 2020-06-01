using System;
using UnityEngine;

namespace Amheklerior.Solitaire {

    public class Dealer : MonoBehaviour {
        
        [SerializeField] private Deck _deck;
        [SerializeField] private Transform _dealingDeckTransform;
        [SerializeField] private Transform[] _playingColumnTransforms;
        
        private DealingStack _dealingDeck;
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


        #region Internals

        private Transform _transform;

        private void Init() {
            _dealingDeck = new DealingStack(_dealingDeckTransform.position);
            _playingColumns = new PlayingStack[_playingColumnTransforms.Length];
            for (int i = 0; i < _playingColumns.Length; i++)
                _playingColumns[i] = new PlayingStack(_playingColumnTransforms[i].position);
        }
        
        private void CreateDeck() {
            _transform = transform;
            _deck.OnCardCreated = card => card.transform.parent = _transform;
            _deck.GenerateCards();
        }

        private void ShuffleDeck() => _deck.Shuffle();

        private void ClearTable() {
            _dealingDeck.Clear();
            foreach (PlayingStack column in _playingColumns)
                column.Clear();
        }

        private void DealCards() {
            PlaceDeck();
            DealFacedDownCards();
            DealFacedUpCards();
        }
        
        [ContextMenu("Place Deck")]
        private void PlaceDeck() => _dealingDeck.SetCards(_deck.Cards);

        [ContextMenu("Deal Faced-Down Cards")]
        private void DealFacedDownCards() {
            for (int i = 0; i < _playingColumns.Length - 1; i++)
                for (int j = i + 1; j < _playingColumns.Length; j++) 
                    PlaceOnTable(_playingColumns[j], _dealingDeck.Take(), Card.FACING_DOWN);
        }

        [ContextMenu("Deal Faced-Up Cards")]
        private void DealFacedUpCards() {
            foreach (CardStack stack in _playingColumns)
                PlaceOnTable(stack, _dealingDeck.Take(), Card.FACING_UP);
        }

        private static void PlaceOnTable(CardStack stack, Card card, bool facingUp) {
            card.IsFacingUp = facingUp;
            stack.Put(card);
        }
        
        #endregion

    }
}