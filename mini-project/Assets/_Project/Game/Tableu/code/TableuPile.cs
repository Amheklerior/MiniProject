using UnityEngine;

namespace Amheklerior.Solitaire {

    public class TableuPile : CardStackComponent, IDragDropOrigin, IDragDropDestination {

        [Header("Settings:")]
        [SerializeField] protected float _verticalOffset = -0.2f;
        [SerializeField] protected float _depthOffset = -0.1f;

        public CardPile CardPileRoot {
            get => _pile;
            set {
                _pile = value;
                if (_pile) Link(_pile.Card);
            }
        }

        public bool IsEmpty => !HasCards && !CardPileRoot;

        public bool ValidDropPositionFor(Card card) => IsEmpty && card.Number is Number.K;
        
        public void Drop(Card card) {
            card.DropTo((Vector3) _stackPosition + _stackDirection * _stack.CardCount);
            card.Pile.DetachPrevious();
            CardPileRoot = card.Pile;
        }
        
        public void UndoDrop(Card card) {
            Unlink(card);
            CardPileRoot = null;
        }
        
        public void FlipTopCard() {
            var topCard = _stack.Take();
            topCard.Flip();
            CardPileRoot = topCard.Pile;
        }

        public void UndoFlipTopCard() {
            var flippedCard = CardPileRoot.Card;
            flippedCard.Flip();
            _stack.Put(flippedCard);
            CardPileRoot = null;
        }

        #region Internals

        private CardPile _pile;
        private TableuPileInputHandler _inputHandler;

        private void SetDependencies() {
            _inputHandler = GetComponentInChildren<TableuPileInputHandler>();
            _inputHandler.Init(_verticalOffset, _depthOffset);
        }

        protected override Vector3 Direction => new Vector3(0f, _verticalOffset, _depthOffset);

        protected override void Init() {
            base.Init();
            SetDependencies();
        }

        protected override void OnPut(Card card) {
            base.OnPut(card);
            _inputHandler.UpdateColliderPosition(TableuPileInputHandler.UpdateDirection.FORWARD);
        }

        protected override void OnTake(Card card) {
            base.OnTake(card);
            _inputHandler.UpdateColliderPosition(TableuPileInputHandler.UpdateDirection.BACKWARD);
        }

        #endregion

    }
}
