using UnityEngine;

namespace Amheklerior.Solitaire {
    
    [CreateAssetMenu(menuName = "Solitaire/Card/Card Graphics Provider")]
    public class CardGraphicsProvider : ScriptableObject {
        
        [SerializeField] private Sprite _backSprite;
        [SerializeField] private Sprite _frontBaseSprite;
        [SerializeField] private SpriteProvider _numberSpriteProvider;
        [SerializeField] private SpriteProvider _seedSpriteProvider;
        [SerializeField] private SpriteProvider _figureSpriteProvider;
        
        public CardGraphics GetGraphicsFor(Color color, Seed seed, Number number) {
            var back = _backSprite;
            var frontBg = _frontBaseSprite;
            var topLeft = _numberSpriteProvider.Get((int) number);
            var topRight = _seedSpriteProvider.Get((int) seed);
            var body = IsFigure(number) 
                ? _figureSpriteProvider.Get(FigureIndex(color, number)) :
                _seedSpriteProvider.Get((int) seed);
            var col = IsRed(color) ? UnityEngine.Color.red : UnityEngine.Color.black;

            return new CardGraphics {
                Back = back,
                FrontBase = frontBg,
                Number = topLeft,
                Seed = topRight,
                Body = body,
                Color = col
            };
        }

        private bool IsRed(Color color) => color == Color.RED;
        private bool IsFigure(Number number) => (int) number > 10;
        private int FigureIndex(Color color, Number number) => (int) number - 11 + (IsRed(color) ? 0 : 3);
    }
}
