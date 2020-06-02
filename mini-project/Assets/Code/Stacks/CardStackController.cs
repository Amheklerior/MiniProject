using System.Collections.Generic;
using UnityEngine;

namespace Amheklerior.Solitaire {
    
    public abstract class CardStackComponent : MonoBehaviour, ICardStack {

        private static readonly Vector3 DEFAULT_DIR = Vector3.zero;

        protected Vector3 _stackPosition;
        protected Vector3 _stackDirection;
        protected CardStackController _stack;

        private void Awake() {
            _stack = new CardStackController(OnPut, OnTake, OnPutAll, OnTakeAll); 
            _stackPosition = transform.position;
            _stackDirection = Direction;
            Init();
        }

        protected virtual void Init() { }

        protected virtual Vector3 Direction => DEFAULT_DIR;

        protected bool OnlyTopCardIsVisible => Direction == DEFAULT_DIR;

        #region Callbacks

        protected virtual void OnPut(Card card) => card.Move(_stackPosition + _stackDirection * _stack.Count);
        protected virtual void OnTake(Card card) { }
        protected virtual void OnPutAll(ICollection<Card> cards) { }
        protected virtual void OnTakeAll(ICollection<Card> cards) { }

        #endregion
        
        #region CardStack interface forwarding

        public bool HasCards => _stack.HasCards;
        public Card TopCard => _stack.TopCard;
        public int Count => _stack.Count;
        public void Put(Card card) => _stack.Put(card);
        public void PutAll(ICollection<Card> cards) => _stack.PutAll(cards);
        public Card Take() => _stack.Take();
        public ICollection<Card> TakeAll() => _stack.TakeAll();
        public void Clear() => _stack.Clear();

        #endregion

    }


    public class PickableStack : CardStackComponent {

        protected override void OnPut(Card card) {
            base.OnPut(card);
            // specific behabior
        }

        protected override void OnTake(Card card) {
            base.OnPut(card);
            // specific behabior
        }

    }

    public class SeedStack : CardStackComponent {

        protected override void OnPut(Card card) {
            base.OnPut(card);
            // specific behabior
        }

        protected override void OnTake(Card card) {
            base.OnPut(card);
            // specific behabior
        }

    }

}