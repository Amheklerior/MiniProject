using UnityEngine;

namespace Amheklerior.Solitaire {

    public interface ICardDropArea {
        bool ValidDropPositionFor(Card card);
        void Drop(Card card);
    }
    
    public class CardPile : MonoBehaviour, ICardDropArea {

        [Header("Settings:")]
        [SerializeField] private Vector3 _pileOffset = new Vector3(0f, -0.2f, -0.1f);

        public void MoveTo(Vector3 position) {
            _transform.position = position;
            if (Next) Next.MoveTo(position + _pileOffset);
        }

        public bool ValidDropPositionFor(Card card) =>
            !Next &&
            _card.IsFacingUp &&
            _card.DifferentColor(card) &&
            _card.IsNextHighNumber(card);

        public void Drop(Card card) {
            card.Pile.MoveTo(_transform.position + _pileOffset);
            Next = card.Pile;
        }

        #region Internals

        private Card _card;
        private Transform _transform;

        private CardPile Next { get; set; }

        private void Awake() {
            _card = GetComponent<Card>();
            _transform = transform;
        }

        #endregion

    }
}