using UnityEngine;

namespace Amheklerior.Solitaire {

    [RequireComponent(typeof(SpriteRenderer))]
    public class CardRenderer : MonoBehaviour {

        [Header("Dependencies:")]
        [SerializeField] private CardImageProvider _cardImageProvider;

        public bool IsFacingUp {
            get => CardSprite == _front ? CardData.FACING_UP : CardData.FACING_DOWN;
            private set => CardSprite = value ? _front : _back;
        }

        public void Flip() => IsFacingUp = !IsFacingUp;

        public void Init(Seed seed, Number number) {
            _front = _cardImageProvider.GetFrontImageFor(seed, number);
            _back = _cardImageProvider.BackImage;
        }

        #region Internals

        private SpriteRenderer _renderer;
        private Sprite _front;
        private Sprite _back;

        private void Awake() {
            if (!_cardImageProvider) {
                Debug.LogError("CARD: no image provider has been set.", _cardImageProvider);
                throw new MissingReferenceException();
            }
            _renderer = GetComponent<SpriteRenderer>();
        }

        private Sprite CardSprite {
            get => _renderer.sprite;
            set => _renderer.sprite = value;
        }

        #endregion

    }
}