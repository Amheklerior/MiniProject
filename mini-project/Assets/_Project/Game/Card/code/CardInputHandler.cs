using UnityEngine;
using Amheklerior.Solitaire.Util;

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

        [SerializeField] private float _movementDeltaForCardSelection = 0.4f;
        
        private void OnMouseDown() {
            if (!_card.IsSelectable || Game.IsBusy) return;
            TakeGrabPosition();
        }

        private void OnMouseDrag() {
            if (!_card.IsSelectable || Game.IsBusy) return;
            DragCard();
            if (!_isSelected) CheckForSelection();
            else DetechDestination();
        }
        
        private void OnMouseUp() {
            if (!_card.IsSelectable || Game.IsBusy) return;
            if (_isSelected && IsOnValidDropPosition) {
                DropCard();
            } else {
                RollBack();
                ClearData();
            }
        }
        

        #region Internals

        private Transform _tranform;
        private Card _card;
        private Camera _cam;
        
        private bool _isSelected;
        private Vector3 _initialPosition;
        private Vector2 _grabPosition;
        private Vector2 _grabDelta;

        private IDragDropDestination _destination;
        private IDragDropOrigin _origin;

        private bool IsOnValidDropPosition => _destination != null && _destination.ValidDropPositionFor(_card);

        private Vector3 PointOnScreen {
            get {
                var pointer = _cam.ScreenToWorldPoint(Input.mousePosition) + (Vector3)_grabDelta;
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

        void TakeGrabPosition() {
            _grabPosition = PointOnScreen;
            _initialPosition = _tranform.position;
            _grabDelta = ComputeDelta();
        }

        private void CheckForSelection() {
            if (((Vector2) PointOnScreen - _grabPosition).magnitude > _movementDeltaForCardSelection)
                StartDraggingCard();
        }

        private void StartDraggingCard() {
            _isSelected = true;
            _origin = (IDragDropOrigin) _card.Stack ?? _card.Pile.Previous;
        }

        private void DragCard() => _card.Pile.DragTo(PointOnScreen);

        private void DetechDestination() => _destination = GetHoveredDropArea();
        
        private void DropCard() => GlobalCommandExecutor.Execute(new SolitaireMove(_card, _origin, _destination));
        
        private void RollBack() => _card.DropTo(_initialPosition);

        private void ClearData() {
            _isSelected = false;
            _origin = null;
            _destination = null;
        }

        #endregion

    }
}
