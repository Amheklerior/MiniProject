using System.Collections.Generic;
using UnityEngine;

namespace Amheklerior.Solitaire {

    public class CardStack {

        public CardStack(Vector3 stackPositionOnTable) {
            _positionOnTable = stackPositionOnTable;
            _positionOffset = GetOffset();
        }

        public void Put(Card card) {
            if (OnlyTopCardIsVisible)
                SetTopCardVisibility(false);

            card.Position = _positionOnTable + _positionOffset * _stack.Count;
            card.gameObject.SetActive(true);
            _stack.Push(card);
        }

        public Card Take() {
            var card = _stack.Pop();
            if (OnlyTopCardIsVisible)
                SetTopCardVisibility(true);
            return card;
        }

        public void Clear() => _stack.Clear();

        #region Internals 

        protected static readonly Vector3 _defaultOffset = new Vector3(0f, 0f, -0.2f);
        protected readonly Vector3 _positionOnTable;
        protected readonly Vector3 _positionOffset;
        protected Stack<Card> _stack = new Stack<Card>();

        private bool OnlyTopCardIsVisible => _positionOffset == _defaultOffset;
        protected Card TopCard => _stack.Peek();

        protected virtual Vector3 GetOffset() => _defaultOffset;

        private void SetTopCardVisibility(bool visible) {
            if (_stack.Count > 0) TopCard.gameObject.SetActive(visible);
        }

        #endregion

    }




    public class DealingStack : CardStack {

        public DealingStack(Vector3 stackPositionOnTable) : base(stackPositionOnTable) { }

        public void SetCards(ICollection<Card> newCards) {
            _stack.Clear();
            foreach (Card card in newCards) {
                _stack.Push(card);
                card.Position = _positionOnTable + _positionOffset * _stack.Count;
            }
            TopCard.gameObject.SetActive(true);
        }
    }

    
    public class PlayingStack : CardStack {

        private static readonly float OFFSET = 0.5f;
        protected override Vector3 GetOffset() => _defaultOffset + Vector3.down * OFFSET;

        public PlayingStack(Vector3 stackPositionOnTable) : base(stackPositionOnTable) { }
        
        /*
        public override Card Take() {
            if (!TopCard.IsFacingUp) TopCard.Flip();
            return base.Take();
        }
        */
    }


    /*
     * TODO -- IF IMPLEMENTING THE SERVE THREE

    public class ServingStack : CardStack {
    
        private static readonly float OFFSET = 0.5f;
        protected override Vector3 GetOffset() => _defaultOffset + Vector3.right * OFFSET;

        public ServingStack(Vector3 stackPositionOnTable) : base(stackPositionOnTable) { }

        public override void Put(Card card) {
            if (_stack.Count != 0) TopCard.gameObject.SetActive(false);
            base.Put(card);
        }

    }
    */

}

