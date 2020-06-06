using UnityEngine;
using Amheklerior.Solitaire.Util;
using System;

namespace Amheklerior.Solitaire {

    #region Interfaces

    public interface IDragDropOrigin { }

    public interface IDragDropDestination {
        bool ValidDropPositionFor(Card card);
        void Drop(Card card);
        void UndoDrop(Card card);
    }

    #endregion
    
    public class CardInputHandler : MonoBehaviour {

        private void OnMouseDown() {
            if (!_card.IsSelectable || Game.IsBusy) return;
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
        private IDragDropDestination _destination;
        private IDragDropOrigin _origin;
        
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

        private IDragDropDestination GetHoveredDropArea() {
            var rayOrigin = _cam.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10f;
            Debug.DrawLine(rayOrigin, rayOrigin + Vector3.forward * 100f, UnityEngine.Color.yellow);
            if (Physics.Raycast(rayOrigin, Vector3.forward, out RaycastHit hit, 100f)) {
                return hit.transform.GetComponentInParent<IDragDropDestination>();
            }
            return null;
        }

        private void StartDraggingCard() {
            _isBeingDragged = true;
            _initialPosition = _tranform.position;
            _delta = ComputeDelta();
            _origin = (IDragDropOrigin) _card.Stack ?? _card.Pile.Previous;
        }

        private void DragCard() {
            _card.DragTo(PointOnScreen);
            _destination = GetHoveredDropArea();
        }

        private void DropCard() {
            GlobalCommandExecutor.Execute(new SolitaireMove(_card, _origin, _destination));
            Game.IncrementMovesCounter();
        }

        private void RollBack() => _card.DragTo(_initialPosition);

        private void ClearData() {
            _isBeingDragged = false;
            _origin = null;
            _destination = null;
        }

        #endregion

        #region SolitaireMove inner class

        private class SolitaireMove : ICommand {

            public bool Reversible => true;

            public void Perform() {
                Debug.Log($"Move card {_movedCard} from {_origin} to {_destination}.");
                _destination.Drop(_movedCard);
                if (_origin is TableuPile pile)
                    pile.CardPileRoot = null;
                else if (_origin is CardStackComponent stack)
                    stack.Take();
                UpdateScore(_origin, _destination);
            }

            public void Undo() {
                Debug.Log($"Move card {_movedCard} from {_destination} back to {_origin}.");
                _destination.UndoDrop(_movedCard);
                if (_origin is TalonStack talon)
                    talon.Put(_movedCard);
                else
                    ((IDragDropDestination) _origin).Drop(_movedCard);
                UndoUpdateScore(_origin, _destination);
            }

            #region Internals

            private Card _movedCard;
            private IDragDropOrigin _origin;
            private IDragDropDestination _destination;

            public SolitaireMove(Card cardToMove, IDragDropOrigin origin, IDragDropDestination destination) {
                _movedCard = cardToMove;
                _origin = origin;
                _destination = destination;
            }

            private static void UpdateScore(IDragDropOrigin origin, IDragDropDestination destination) =>
                Game.UpdateScoreBy(GetScore(origin, destination));
            
            private static void UndoUpdateScore(IDragDropOrigin origin, IDragDropDestination destination) =>
                Game.UpdateScoreBy(-GetScore(origin, destination));
            
            
            private static int GetScore(IDragDropOrigin origin, IDragDropDestination destination) {
                if (destination is FoundationStack)
                    return (int) GameScore.MOVE_CARD_TO_FOUNDATION_STACK;

                if (origin is TalonStack)
                    return (int) GameScore.MOVE_CARD_FROM_TALON_TO_TABLEU_PILE;

                if (origin is TableuPile)
                    return (int) GameScore.MOVE_CARD_BETWEEN_TABLEU_PILES;

                if (origin is FoundationStack)
                    return (int) GameScore.MOVE_CARD_FROM_FOUNDATION_STACK_TO_TABLEU_PILE;

                else
                    return (int) GameScore.NO_SCORE;
            }

            #endregion

        }

        #endregion

    }
}
