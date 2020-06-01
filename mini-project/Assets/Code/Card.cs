using UnityEngine;
using DG.Tweening;

namespace Amheklerior.Solitaire {
    
    [RequireComponent(typeof(SpriteRenderer))]
    public class Card : MonoBehaviour {

        public static readonly float MOVEMENT_ANIMATION_TIME = 0.2f;
        public static readonly bool FACING_UP = true;
        public static readonly bool FACING_DOWN = false;
        
        public bool IsFacingUp {
            get => CardSprite == _front ? FACING_UP : FACING_DOWN;
            set => CardSprite = value ? _front : _back;
        }
        
        public Vector3 Position {
            get => transform.position;
            set => transform.DOMove(value, MOVEMENT_ANIMATION_TIME);
        }

        public void Init(Seed seed, Number number, CardImageProvider cardImageProvider) {
            _front = cardImageProvider.GetFrontImageFor(seed, number);
            _back = cardImageProvider.BackImage;
            _transform = transform;
            _renderer = GetComponent<SpriteRenderer>();
            IsFacingUp = false;
        }

        public void Flip() => IsFacingUp = !IsFacingUp;


        #region Internals


        private Transform _transform;
        private SpriteRenderer _renderer;

        private Sprite _front;
        private Sprite _back;
        
        private Sprite CardSprite {
            get => _renderer.sprite;
            set => _renderer.sprite = value;
        }

        #endregion
    }

}