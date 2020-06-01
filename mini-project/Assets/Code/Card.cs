using UnityEngine;
using System;
using DG.Tweening;

namespace Amheklerior.Solitaire {
    
    [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
    public class Card : MonoBehaviour {

        public static readonly float MOVEMENT_ANIMATION_TIME = 0.2f;
        public static readonly float ROTATION_ANIMATION_TIME = 0.2f;
        public static readonly bool FACING_UP = true;
        public static readonly bool FACING_DOWN = false;
        
        public bool IsFacingUp {
            get => CardSprite == _front ? FACING_UP : FACING_DOWN;
            set => CardSprite = value ? _front : _back;
        }

        public bool Selectable => IsFacingUp;
        
        public void MoveTo(Vector3 position, Action onComplete = null) {
            transform
                .DOMove(position, MOVEMENT_ANIMATION_TIME)
                .OnComplete(() => onComplete?.Invoke());
        }

        public void Flip() {
            transform.DORotate(Vector3.up * 360f, ROTATION_ANIMATION_TIME, RotateMode.FastBeyond360);
            IsFacingUp = !IsFacingUp;
        }

        public void Init(Seed seed, Number number, CardImageProvider cardImageProvider) {
            _front = cardImageProvider.GetFrontImageFor(seed, number);
            _back = cardImageProvider.BackImage;
            GrabRefs();
            InitCollider(GetComponent<BoxCollider2D>());
            IsFacingUp = false;
        }

        private void OnMouseDown() {
            if (Selectable) Debug.Log($"The {name} has been selected");
        }
        
        #region Internals

        private Transform _transform;
        private SpriteRenderer _renderer;

        private Sprite _front;
        private Sprite _back;
        
        private Sprite CardSprite {
            get => _renderer.sprite;
            set => _renderer.sprite = value;
        }

        private void GrabRefs() {
            _transform = transform;
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void InitCollider(BoxCollider2D collider) {
            collider.size = _renderer.size;
            collider.isTrigger = true;
        }

        #endregion

    }
}
