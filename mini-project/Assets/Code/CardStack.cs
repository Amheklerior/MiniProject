using System;
using System.Collections.Generic;
using UnityEngine;

namespace Amheklerior.Solitaire {

    public class CardStack {

        public CardStack(Vector3 stackPositionOnTable) {
            _positionOnTable = stackPositionOnTable;
            _positionOffset = GetOffset();
        }

        public bool HasCards => _stack.Count > 0;

        public void Put(Card card) {
            var lastTopCard = HasCards ? TopCard : null;
            card.MoveTo(
                _positionOnTable + _positionOffset * _stack.Count,
                () => HideCardIfNecessary(lastTopCard));
            card.gameObject.SetActive(true);
            OnPut?.Invoke(card);
            _stack.Push(card);
        }

        public Card Take() {
            var card = _stack.Pop();
            if (OnlyTopCardIsVisible)
                SetTopCardVisible();
            OnTake?.Invoke(card);
            return card;
        }

        public ICollection<Card> TakeAll() => _stack.ToArray();

        public void Clear() => _stack.Clear();


        #region Internals 

        protected static readonly float DEPTH_OFFSET = -0.25f;
        protected static readonly Vector3 _defaultOffset = Vector3.zero;
        protected readonly Vector3 _positionOnTable;
        protected readonly Vector3 _positionOffset;
        protected Action<Card> OnPut;
        protected Action<Card> OnTake;
        protected Stack<Card> _stack = new Stack<Card>();

        protected Card TopCard => _stack.Peek();

        private bool OnlyTopCardIsVisible => _positionOffset == _defaultOffset;

        protected virtual Vector3 GetOffset() => _defaultOffset;
        
        private void SetTopCardVisible() {
            if (HasCards)
                TopCard.gameObject.SetActive(true);
        }

        private void HideCardIfNecessary(Card card) {
            if (OnlyTopCardIsVisible)
                card?.gameObject.SetActive(false);
        }

        #endregion
    }


    public class DealingStack : CardStack {

        public DealingStack(Vector3 stackPositionOnTable) : base(stackPositionOnTable) {
            OnPut = card => card.Flip();
            OnTake = card => card.gameObject.transform.position = 
                card.gameObject.transform.position + (Vector3.forward * DEPTH_OFFSET);
        }

        public void SetCards(ICollection<Card> newCards) {
            _stack = new Stack<Card>(newCards);
            TopCard.gameObject.SetActive(true);
        }
    }
    
    public class PlayingStack : CardStack {

        private static readonly float VERTICAL_OFFSET = -0.5f;
        protected override Vector3 GetOffset() => _defaultOffset + new Vector3(0f, VERTICAL_OFFSET, DEPTH_OFFSET);

        public PlayingStack(Vector3 stackPositionOnTable) : base(stackPositionOnTable) { }
    }
    
}
