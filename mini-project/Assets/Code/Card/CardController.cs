using System;
using UnityEngine;
using DG.Tweening;

namespace Amheklerior.Solitaire {

    public class CardController {
        
        public void Move(Vector3 destination, Action onComplete = null) =>
            _card.DOMove(destination, _movementAnimationTime)
                .OnComplete(() => onComplete?.Invoke());
        
        public void Flip() =>
            _card.DORotate(Vector3.up * 180f, 0f).OnComplete(() =>
                _card.DORotate(Vector3.up * 360f, _rotationAnimationTime, RotateMode.FastBeyond360));
        

        #region Internals

        private readonly float _movementAnimationTime;
        private readonly float _rotationAnimationTime;

        private Transform _card;
        private Card _cardComponent;

        public CardController(Card card, float movementAnimationTime = 0.2f, float rotationAnimationTime = 0.2f) {
            _cardComponent = card;
            _card = card.transform; 
            _movementAnimationTime = movementAnimationTime;
            _rotationAnimationTime = rotationAnimationTime;
        }

        #endregion

    }
}
