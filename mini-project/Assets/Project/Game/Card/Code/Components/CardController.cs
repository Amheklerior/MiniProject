using UnityEngine;
using Amheklerior.Solitaire.Util;
using System;

namespace Amheklerior.Solitaire {

    [RequireComponent(typeof(CardRenderer))]
    public class CardController : MonoBehaviour {

        [Header("Settings:")]
        [SerializeField] private float _movementAnimationTime = 0.2f;
        [SerializeField] private float _rotationAnimationTime = 0.2f;
        [SerializeField] private AudioEvent _cardSound;

        #region Info

        public Seed Seed => _cardData.Seed;
        public Number Number => _cardData.Number;
        public Color Color => _cardData.Color;

        public CardPileNode Pile { get; private set; }
        public CardStackComponent Stack { get; set; }

        public bool IsSelectable => _renderer.IsFacingUp;
        public bool IsFacingUp => _renderer.IsFacingUp;
        public bool IsAPile() => Pile.HasNext;

        #endregion

        #region Actions 

        public void Activate() => gameObject.SetActive(true);
        public void Deactivate() => gameObject.SetActive(false);

        public void Show() {
            if (!_renderer.IsFacingUp)
                _renderer.Flip();
        }

        public void Hide() {
            if (_renderer.IsFacingUp)
                _renderer.Flip();
        }

        public void DragTo(Vector3 position) => MoveTo(position, false);
        public void DropTo(Vector3 position) => Pile.MoveTo(position);

        public void PlaceTo(Vector3 position) {
            Game.StartAction();
            MoveTo(position, true, () => Game.EndAction());
        }

        public void MoveTo(Vector3 position, bool withAudio = true, Action onComplete = null) {
            _controller.Move(position, onComplete);
            if (withAudio) PlaySound();
        }

        [ContextMenu("Flip")]
        public void Flip() {
            Game.StartAction();
            _controller.Flip(() => Game.EndAction());
            _renderer.Flip();
            PlaySound();
        }

        [ContextMenu("Play Sound")]
        public void PlaySound() => _cardSound.Play(_audioSource);

        #endregion

        #region Comparing methods

        public bool SameColor(CardController other) => Color == other.Color;
        public bool DifferentColor(CardController other) => !SameColor(other);
        public bool IsNextLowNumber(CardController other) => (int) Number == (int) other.Number - 1;
        public bool IsNextHighNumber(CardController other) => (int) Number == (int) other.Number + 1;

        #endregion

        #region Internals

        private Card _cardData;
        private CardAnimator _controller;
        private CardRenderer _renderer;
        private AudioSource _audioSource;

        public void Init(Seed seed, Number number) {
            _cardData = new Card(seed, number);
            _controller = new CardAnimator(this, _movementAnimationTime, _rotationAnimationTime);
            _renderer = GetComponent<CardRenderer>();
            _renderer.Init(seed, number);
            _audioSource = GetComponent<AudioSource>();
            Pile = GetComponent<CardPileNode>();
        }

        public override string ToString() => name;

        #endregion

    }
}

