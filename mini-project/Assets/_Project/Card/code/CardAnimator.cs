﻿using System;
using UnityEngine;
using DG.Tweening;

namespace Amheklerior.Solitaire {

    public class CardAnimator {

        public void Move(Vector3 destination, Action onComplete = null) {
            Game.StartAction();
            _card.DOMove(destination, _movementAnimationTime).OnComplete(() => {
                onComplete?.Invoke();
                Game.EndAction();
            });
        }

        public void Flip() {
            Game.StartAction();
            //_card.DORotate(Vector3.up * 360f, 0f).OnComplete(() => 
                _card.DORotate(Vector3.up * 360f, _rotationAnimationTime, RotateMode.FastBeyond360).OnComplete(() => Game.EndAction());
        }

        #region Internals

        private readonly float _movementAnimationTime;
        private readonly float _rotationAnimationTime;

        private Transform _card;

        public CardAnimator(Card card, float movementAnimationTime = 0.2f, float rotationAnimationTime = 0.2f) {
            _card = card.transform;
            _movementAnimationTime = movementAnimationTime;
            _rotationAnimationTime = rotationAnimationTime;
        }

        #endregion

    }
}
