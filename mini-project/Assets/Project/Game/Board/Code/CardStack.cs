using System;
using System.Collections.Generic;

namespace Amheklerior.Solitaire {
    
    public class CardStack : ICardStack {

        public bool HasCards => _stack.Count > 0;

        public CardController TopCard => _stack.Peek();

        public int CardCount { get; private set; }

        public void Put(CardController card) {
            _stack.Push(card);
            OnPut?.Invoke(card);
            CardCount++;
        }

        public CardController Take() {
            var card = _stack.Pop();
            OnTake?.Invoke(card);
            CardCount--;
            return card;
        }

        public void PutAll(ICollection<CardController> cards) {
            _stack = new Stack<CardController>(cards);
            OnPutAll?.Invoke(cards);
            CardCount += cards.Count;
        }

        public ICollection<CardController> TakeAll() {
            var cards = _stack.ToArray();
            OnTakeAll?.Invoke(cards);
            CardCount = 0;
            return cards;
        }

        public void Clear() => _stack.Clear();
        
        #region Internals 

        protected readonly Action<CardController> OnPut;
        protected readonly Action<CardController> OnTake;
        protected readonly Action<ICollection<CardController>> OnPutAll;
        protected readonly Action<ICollection<CardController>> OnTakeAll;

        private Stack<CardController> _stack = new Stack<CardController>();

        public CardStack(
            Action<CardController> onPut = null, 
            Action<CardController> onTake = null, 
            Action<ICollection<CardController>> onPutAll = null, 
            Action<ICollection<CardController>> onTakeAll = null) 
        {
            OnPut = onPut;
            OnTake = onTake;
            OnPutAll = onPutAll;
            OnTakeAll = onTakeAll;
        }

        #endregion

    }
}
