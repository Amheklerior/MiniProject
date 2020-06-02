using UnityEngine;

namespace Amheklerior.Solitaire {

    [RequireComponent(typeof(SpriteRenderer))]
    public class Card : MonoBehaviour {

        [Header("Image Provider:")]
        [SerializeField] private CardImageProvider _cardImageProvider;

        [Header("Animation:")]
        [SerializeField] private float _movementAnimationTime = 0.2f;
        [SerializeField] private float _rotationAnimationTime = 0.2f;

        private SpriteRenderer _renderer;
        private Sprite _front;
        private Sprite _back;
        private CardData _cardData;
        private CardController _controller;


        private void Awake() {
            if (!_cardImageProvider) {
                Debug.LogError("CARD: no image provider has been set.", _cardImageProvider);
                throw new MissingReferenceException();
            }
            _renderer = GetComponent<SpriteRenderer>();
            _controller = new CardController(this, _movementAnimationTime, _rotationAnimationTime);
        }

        public void SetCardData(CardData data) {
            _cardData = data;
            _front = _cardImageProvider.GetFrontImageFor(_cardData.Seed, _cardData.Number);
            _back = _cardImageProvider.BackImage;
        }

        public bool IsFacingUp {
            get => CardSprite == _front ? CardData.FACING_UP : CardData.FACING_DOWN;
            set => CardSprite = value ? _front : _back;
        }


        public void Move(Vector3 destination) {
            // Can Move ?
            _controller.Move(destination); // Add an OnComplete callback ?
                                           // other stuff
        }

        public void Flip() {
            // Can Flip ?
            _controller.Flip();
            IsFacingUp = !IsFacingUp;
            // other stuff
        }


        #region Internals

        private Sprite CardSprite {
            get => _renderer.sprite;
            set => _renderer.sprite = value;
        }

        #endregion

    }
}
