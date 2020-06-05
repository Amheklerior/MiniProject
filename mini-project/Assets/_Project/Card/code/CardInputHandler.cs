﻿using UnityEngine;
using Amheklerior.Solitaire.Command;

namespace Amheklerior.Solitaire {

    public interface ICardDragArea { }

    public interface ICardDropArea {
        bool ValidDropPositionFor(Card card);
        void Drop(Card card);
        void UndoDrop(Card card);
    }

    public class CardInputHandler : MonoBehaviour {

        private void OnMouseDown() {
            if (!_card.IsSelectable) return;
            StartDraggingCard();
        }

        private void OnMouseDrag() {
            if (!_isBeingDragged) return;
            DragCard();
        }

        private void OnMouseUp() {
            if (!_isBeingDragged) return;
            if (IsOnValidDropPosition) DropCard();
            else {
                RollBack();
                ClearData();
            }
        }


        #region Internals

        private Transform _tranform;
        private Card _card;
        private Camera _cam;
        
        private bool _isBeingDragged;
        private Vector3 _initialPosition;
        private Vector2 _delta;
        private ICardDropArea _destination;
        private ICardDragArea _origin;

        private bool IsOnValidDropPosition => _destination != null && _destination.ValidDropPositionFor(_card);

        private Vector3 PointOnScreen {
            get {
                var pointer = _cam.ScreenToWorldPoint(Input.mousePosition) + (Vector3)_delta;
                pointer.z = -10f;
                return pointer;
            }
        }

        private void Awake() {
            _tranform = transform;
            _card = GetComponent<Card>();
            _cam = Camera.main;
        }

        private Vector2 ComputeDelta() => _initialPosition - _cam.ScreenToWorldPoint(Input.mousePosition);

        private ICardDropArea GetHoveredDropArea() {
            var rayOrigin = _cam.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10f;
            Debug.DrawLine(rayOrigin, rayOrigin + Vector3.forward * 100f, UnityEngine.Color.yellow);
            if (Physics.Raycast(rayOrigin, Vector3.forward, out RaycastHit hit, 100f)) {
                return hit.transform.GetComponentInParent<ICardDropArea>();
            }
            return null;
        }

        private void StartDraggingCard() {
            _isBeingDragged = true;
            _initialPosition = _tranform.position;
            _delta = ComputeDelta();
            _origin = (ICardDragArea) _card.Stack ?? _card.Pile.Previous;
        }

        private void DragCard() {
            _card.DragTo(PointOnScreen);
            _destination = GetHoveredDropArea();
        }

        private void DropCard() => GlobalCommandExecutor.Execute(
            () => MoveCardOnDrop(_card, _origin, _destination), 
            () => UndoMoveCardOnDrop(_card, _origin, _destination));

        private void RollBack() => _card.DragTo(_initialPosition);

        private void ClearData() {
            _isBeingDragged = false;
            _origin = null;
            _destination = null;
        }

        #region Undoable Command

        private void MoveCardOnDrop(Card movedCard, ICardDragArea origin, ICardDropArea destination) {
            destination.Drop(movedCard);

            if (origin is TableuPile pile) {
                pile.CardPileRoot = null;

            } else if (origin is CardStackComponent stack) {
                stack.Take();

            }
        }

        private void UndoMoveCardOnDrop(Card movedCard, ICardDragArea origin, ICardDropArea destination) {
            destination.UndoDrop(movedCard);

            if (origin is TalonStack talon) {
                talon.Put(movedCard);

            //} else if (origin is TableuPile pile) {
              //  pile.CardPileRoot = movedCard.Pile;
                
            } else ((ICardDropArea) origin).Drop(movedCard);
            
        }

        #endregion
        
        #endregion

    }
}