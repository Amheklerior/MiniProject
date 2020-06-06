﻿using UnityEngine;
using Amheklerior.Solitaire.Util;
using System;

namespace Amheklerior.Solitaire {

    [RequireComponent(typeof(CardRenderer))]
    public class Card : MonoBehaviour {

        [Header("Settings:")]
        [SerializeField] private float _movementAnimationTime = 0.2f;
        [SerializeField] private float _rotationAnimationTime = 0.2f;
        [SerializeField] private AudioEvent _cardSound;

        #region Info

        public Seed Seed => _cardData.Seed;
        public Number Number => _cardData.Number;
        public Color Color => _cardData.Color;

        public CardPile Pile { get; private set; }
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

        public void MoveTo(Vector3 position) {
            _controller.Move(position);
            PlaySound();
        }

        public void DragTo(Vector3 position) => Pile.MoveTo(position);
        public void DropTo(Vector3 position) => Pile.MoveTo(position, true);

        [ContextMenu("Flip")]
        public void Flip() {
            _controller.Flip();
            _renderer.Flip();
            PlaySound();
        }

        [ContextMenu("Play Sound")]
        public void PlaySound() => _cardSound.Play(_audioSource);

        #endregion

        #region Comparing methods

        public bool SameColor(Card other) => Color == other.Color;
        public bool DifferentColor(Card other) => !SameColor(other);
        public bool IsNextLowNumber(Card other) => (int) Number == (int) other.Number - 1;
        public bool IsNextHighNumber(Card other) => (int) Number == (int) other.Number + 1;

        #endregion

        #region Internals

        private CardData _cardData;
        private CardAnimator _controller;
        private CardRenderer _renderer;
        private AudioSource _audioSource;

        public void Init(Seed seed, Number number) {
            _cardData = new CardData(seed, number);
            _controller = new CardAnimator(this, _movementAnimationTime, _rotationAnimationTime);
            _renderer = GetComponent<CardRenderer>();
            _renderer.Init(seed, number);
            _audioSource = GetComponent<AudioSource>();
            Pile = GetComponent<CardPile>();
        }

        public override string ToString() => name;

        #endregion

    }
}

