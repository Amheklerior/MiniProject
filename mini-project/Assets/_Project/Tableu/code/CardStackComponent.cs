using System.Collections.Generic;
using UnityEngine;

namespace Amheklerior.Solitaire {
    
    public abstract class CardStackComponent : MonoBehaviour, ICardStack {

        #region Fields

        private static readonly Vector3 DEFAULT_DIR = new Vector3(0f, 0f, -0.1f);

        protected Vector2 _stackPosition;
        protected Vector3 _stackDirection;
        protected CardStack _stack;

        #endregion
        
        private void Awake() {
            _stack = new CardStack(OnPut, OnTake, OnPutAll, OnTakeAll); 
            _stackPosition = transform.position;
            _stackDirection = Direction;
            Init();
        }

        protected virtual Vector3 Direction => DEFAULT_DIR;

        protected bool OnlyTopCardIsVisible => Direction == DEFAULT_DIR;

        #region CardStack interface forwarding

        public virtual bool HasCards => _stack.HasCards;
        public virtual Card TopCard => _stack.TopCard;
        public virtual int CardCount => _stack.CardCount;
        public virtual void Put(Card card) => _stack.Put(card);
        public virtual void PutAll(ICollection<Card> cards) => _stack.PutAll(cards);
        public virtual Card Take() => _stack.Take();
        public virtual ICollection<Card> TakeAll() => _stack.TakeAll();
        public virtual void Clear() => _stack.Clear();

        #endregion

        #region Callbacks

        protected virtual Vector3 NextStackPosition => (Vector3) _stackPosition + _stackDirection * _stack.CardCount;

        protected virtual void Init() { }

        protected virtual void OnPut(Card card) {
            card.MoveTo(NextStackPosition);
            Link(card);
        }

        protected virtual void OnTake(Card card) => Unlink(card);

        protected virtual void OnPutAll(ICollection<Card> cards) => Link(cards);

        protected virtual void OnTakeAll(ICollection<Card> cards) => Unlink(cards);

        #endregion
        
        #region Internals

        protected void Link(Card card) => card.Stack = this;
        private void Link(ICollection<Card> cards) {
            foreach (var card in cards) card.Stack = this;
        }

        protected void Unlink(Card card) => card.Stack = null;
        private void Unlink(ICollection<Card> cards) {
            foreach (var card in cards) card.Stack = null;
        }

        public override string ToString() => name;

        #endregion

    }
}