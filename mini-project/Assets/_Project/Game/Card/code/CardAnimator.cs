using System;
using UnityEngine;
using DG.Tweening;
using Amheklerior.Solitaire.Util;

namespace Amheklerior.Solitaire {

    public class CardAnimator {

        public void Move(Vector3 destination, Action onComplete = null) {
            Game.StartAction();
            _cardSound.Play(_audioSource);
            _card.DOMove(destination, _movementAnimationTime).OnComplete(() => {
                onComplete?.Invoke();
                Game.EndAction();
            });
        }

        public void Flip() {
            Game.StartAction();
            _cardSound.Play(_audioSource);
            _card.DORotate(Vector3.up * 360f, _rotationAnimationTime, RotateMode.FastBeyond360).OnComplete(() => Game.EndAction());
        }

        #region Internals

        private readonly float _movementAnimationTime;
        private readonly float _rotationAnimationTime;

        private Transform _card;
        private AudioSource _audioSource;
        private AudioEvent _cardSound;

        public CardAnimator(
            Card card,
            AudioEvent cardSound,
            float movementAnimationTime = 0.2f,
            float rotationAnimationTime = 0.2f) 
        {
            _card = card.transform;
            _audioSource = card.GetComponent<AudioSource>();
            _movementAnimationTime = movementAnimationTime;
            _rotationAnimationTime = rotationAnimationTime;
            _cardSound = cardSound;
        }

        #endregion

    }
}
