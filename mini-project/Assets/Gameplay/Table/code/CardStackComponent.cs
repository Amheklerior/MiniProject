using System.Collections.Generic;
using UnityEngine;

namespace Amheklerior.Solitaire {
    
    public abstract class CardStackComponent : MonoBehaviour, ICardStack {

        #region Fields

        private static readonly Vector3 DEFAULT_DIR = new Vector3(0f, 0f, -0.1f);

        protected Vector3 _stackPosition;
        protected Vector3 _stackDirection;
        protected CardStack _stack;

        #endregion
        
        // Think to move to init and let others call it?
        private void Awake() {
            _stack = new CardStack(OnPut, OnTake, OnPutAll, OnTakeAll); 
            _stackPosition = transform.position;
            _stackDirection = Direction;
            Init();
        }

        protected virtual void Init() { }

        protected virtual Vector3 Direction => DEFAULT_DIR;

        protected bool OnlyTopCardIsVisible => Direction == DEFAULT_DIR;

        #region Callbacks

        protected virtual void OnPut(Card card) { }
        protected virtual void OnTake(Card card) { }
        protected virtual void OnPutAll(ICollection<Card> cards) { }
        protected virtual void OnTakeAll(ICollection<Card> cards) { }

        #endregion
        
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

    }
}