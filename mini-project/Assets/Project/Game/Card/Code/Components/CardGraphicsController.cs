using UnityEngine;

namespace Amheklerior.Solitaire {

    public class CardGraphicsController : MonoBehaviour {

        [Header("Dependencies:")]
        [SerializeField] private CardGraphicsProvider _cardSpritesProvider;

        [Space, Header("Sprite Renderers")]
        [SerializeField] private SpriteRenderer _backSpriteRenderer;
        [SerializeField] private SpriteRenderer _frontBaseSpriteRenderer;
        [SerializeField] private SpriteRenderer _numberSpriteRenderer;
        [SerializeField] private SpriteRenderer _seedSpriteRenderer;
        [SerializeField] private SpriteRenderer _bodySpriteRenderer;


        public bool IsFacingUp {
            get => _frontBaseSpriteRenderer.gameObject.activeInHierarchy;
            private set => _frontBaseSpriteRenderer.gameObject.SetActive(value);
        }

        public void Flip() => IsFacingUp = !IsFacingUp;

        public void Init(Color color, Seed seed, Number number) {
            var graphics = _cardSpritesProvider.GetGraphicsFor(color, seed, number);

            _backSpriteRenderer.sprite = graphics.Back;
            _frontBaseSpriteRenderer.sprite = graphics.FrontBase;
            _numberSpriteRenderer.sprite = graphics.Number;
            _numberSpriteRenderer.color = graphics.Color;
            _seedSpriteRenderer.sprite = graphics.Seed;
            _bodySpriteRenderer.sprite = graphics.Body;
        }

        #region Internals
        
        private void Awake() {
            if (!_cardSpritesProvider) {
                Debug.LogError("CARD: no sprites provider has been set.", this);
                throw new MissingReferenceException();
            }
            if (!_backSpriteRenderer ||
                !_frontBaseSpriteRenderer ||
                !_numberSpriteRenderer ||
                !_seedSpriteRenderer ||
                !_bodySpriteRenderer) 
            {
                Debug.LogError("CARD: One or more references to the card sprite renderer is missing.", this);
                throw new MissingReferenceException();
            }
        }
        
        #endregion
        
    }

}
