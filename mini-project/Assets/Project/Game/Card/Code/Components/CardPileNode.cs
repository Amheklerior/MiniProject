using UnityEngine;

namespace Amheklerior.Solitaire {

    public class CardPileNode : MonoBehaviour, IDragDropOrigin, IDragDropDestination {

        [Header("Settings:")]
        [SerializeField] private Vector3 _pileOffset;

        public CardController Card { get; private set; }

        public bool HasNext => Next != null;

        public void MoveTo(Vector3 position, bool withAudio = true) {
            Card.MoveTo(position, withAudio);
            if (Next) Next.MoveTo(position + _pileOffset, withAudio); 
        }

        public bool ValidDropPositionFor(CardController card) =>
            Card.Stack?.GetType() != typeof(TalonStack) && 
            !Next && 
            Card.IsFacingUp &&
            Card.DifferentColor(card) &&
            Card.IsNextHighNumber(card);

        public void DragTo(Vector3 position) {
            Card.DragTo(position);
            if (Next) Next.DragTo(position + _pileOffset);
        }

        public void Drop(CardController card) {
            card.DropTo(_transform.position + _pileOffset);
            AttachNext(card.Pile);
            card.Stack = null;
        }

        public void UndoDrop(CardController card) => DetachNext();


        #region Internals

        private Transform _transform;

        public CardPileNode Next { get; private set; }
        public CardPileNode Previous { get; private set; }

        private void Awake() {
            Card = GetComponent<CardController>();
            _transform = transform;
            _pileOffset = _pileOffset == null ? new Vector3(0f, -0.2f, -0.1f) : _pileOffset;
        }

        private void AttachNext(CardPileNode pile) {
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