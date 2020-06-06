using UnityEngine;

namespace Amheklerior.Solitaire {

    public class CardPile : MonoBehaviour, IDragDropOrigin, IDragDropDestination {

        [Header("Settings:")]
        [SerializeField] private Vector3 _pileOffset = new Vector3(0f, -0.2f, -0.1f);

        public Card Card { get; private set; }

        public bool HasNext => Next != null;

        public void MoveTo(Vector3 position) {
            Card.MoveTo(position);
            if (Next) Next.MoveTo(position + _pileOffset);
        }

        public bool ValidDropPositionFor(Card card) =>
            Card.Stack?.GetType() != typeof(TalonStack) && 
            !Next && 
            Card.IsFacingUp &&
            Card.DifferentColor(card) &&
            Card.IsNextHighNumber(card);

        public void Drop(Card card) {
            card.DragTo(_transform.position + _pileOffset);
            AttachNext(card.Pile);
            card.Stack = null;
        }

        public void UndoDrop(Card card) => DetachNext();


        #region Internals

        private Transform _transform;

        public CardPile Next { get; private set; }
        public CardPile Previous { get; private set; }

        private void Awake() {
            Card = GetComponent<Card>();
            _transform = transform;
        }

        private void AttachNext(CardPile pile) {
            pile.Previous?.DetachNext();
            pile.Previous = this;
            Next = pile;
        }
        
        private void DetachNext() {
            if (!Next) return;
            Next.Previous = null;
            Next = null;
        }

        public void DetachPrevious() {
            if (!Previous) return;
            Previous.Next = null;
            Previous = null;
        }

        public override string ToString() => Card.name;

        #endregion

    }
}