﻿using System;
using UnityEngine;
using DG.Tweening;

namespace Amheklerior.Solitaire {

    public class CardController {

        private readonly float _movementAnimationTime;
        private readonly float _rotationAnimationTime;

        private Transform _card;

        public CardController(Transform card, float movementAnimationTime = 0.2f, float rotationAnimationTime = 0.2f) {
            _card = card;
            _movementAnimationTime = movementAnimationTime;
            _rotationAnimationTime = rotationAnimationTime;
        }

        public void Move(Vector3 destination, Action onComplete = null) =>
            _card.DOMove(destination, _movementAnimationTime)
                .OnComplete(() => onComplete?.Invoke());
                    

        public void Flip() =>
            _card.DORotate(Vector3.up * 360f, _rotationAnimationTime, RotateMode.FastBeyond360);

    }
}
