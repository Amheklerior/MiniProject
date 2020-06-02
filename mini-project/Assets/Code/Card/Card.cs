using UnityEngine;

namespace Amheklerior.Solitaire {

    [RequireComponent(typeof(SpriteRenderer))]
    public class Card : MonoBehaviour {

        #region Inspector Interface

        [Header("Dependencies:")]
        [SerializeField] private CardImageProvider _cardImageProvider;

        [Space, Header("Animation:")]
        [SerializeField] private float _movementAnimationTime = 0.2f;
        [SerializeField] private float _rotationAnimationTime = 0.2f;

        #endregion
        
        public Card Next { get; set; }
        public Card Prev { get; set; }

        public bool IsFacingUp {
            get => CardSprite == _front ? CardData.FACING_UP : CardData.FACING_DOWN;
            set => CardSprite = value ? _front : _back;
        }

        void Awake() {
            CheckDependencies();
            _renderer = GetComponent<SpriteRenderer>();
            _controller = new CardController(this, _movementAnimationTime, _rotationAnimationTime);
        }

        public void SetCardData(CardData data) {
            _cardData = data;
            _front = _cardImageProvider.GetFrontImageFor(_cardData.Seed, _cardData.Number);
            _back = _cardImageProvider.BackImage;
        }

        public void Flip() {
            _controller.Flip();
            IsFacingUp = !IsFacingUp;
        }

        public void Move(Vector3 destination) => _controller.Move(destination);
        
        #region Internals

        private SpriteRenderer _renderer;
        private Sprite _front;
        private Sprite _back;
        private CardData _cardData;
        private CardController _controller;


        private void CheckDependencies() {
            if (!_cardImageProvider) {
                Debug.LogError("CARD: no image provider has been set.", _cardImageProvider);
                throw new MissingReferenceException();
            }
        }

        private Sprite CardSprite {
            get => _renderer.sprite;
            set => _renderer.sprite = value;
        }

        #endregion

    }
}
