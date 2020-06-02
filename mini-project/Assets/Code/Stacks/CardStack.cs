using System;
using System.Collections.Generic;

namespace Amheklerior.Solitaire {

    public interface ICardStack {
        bool HasCards { get; }
        Card TopCard { get; }
        int Count { get; }
        void Put(Card card);
        void PutAll(ICollection<Card> cards);
        Card Take();
        ICollection<Card> TakeAll();
        void Clear();
    }


    public class CardStackController : ICardStack {

        public bool HasCards => _stack.Count > 0;

        public Card TopCard => _stack.Peek();

        public int Count => _cardCount;

        public void Put(Card card) {
            _stack.Push(card);
            OnPut?.Invoke(card);
            _cardCount++;
        }

        public Card Take() {
            var card = _stack.Pop();
            OnTake?.Invoke(card);
            _cardCount--;
            return card;
        }

        public void PutAll(ICollection<Card> cards) {
            _stack = new Stack<Card>(cards);
            OnPutAll?.Invoke(cards);
            _cardCount += cards.Count;
        }

        public ICollection<Card> TakeAll() {
            var cards = _stack.ToArray();
            OnTakeAll?.Invoke(cards);
            _cardCount = 0;
            return cards;
        }

        public void Clear() => _stack.Clear();


        #region Internals 

        protected readonly Action<Card> OnPut;
        protected readonly Action<Card> OnTake;
        protected readonly Action<ICollection<Card>> OnPutAll;
        protected readonly Action<ICollection<Card>> OnTakeAll;

        private Stack<Card> _stack = new Stack<Card>();
        private int _cardCount;
        
        public CardStackController(
            Action<Card> onPut = null, 
            Action<Card> onTake = null, 
            Action<ICollection<Card>> onPutAll = null, 
            Action<ICollection<Card>> onTakeAll = null) 
        {
            OnPut = onPut;
            OnTake = onTake;
            OnPutAll = onPutAll;
            OnTakeAll = onTakeAll;
        }

        #endregion

    }
}
