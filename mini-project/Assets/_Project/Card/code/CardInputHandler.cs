using System;
using UnityEngine;

namespace Amheklerior.Solitaire {

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
            else RollBack();
            ClearData();
        }


        #region Internals

        private Transform _tranform;
        private Card _card;
        private Camera _cam;
        
        private bool _isBeingDragged;
        private Vector3 _initialPosition;
        private Vector2 _delta;
        private CardStackComponent _initialStack;
        private ICardDropArea _dropArea;

        private bool IsOnValidDropPosition => _dropArea != null && _dropArea.ValidDropPositionFor(_card);

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
            _initialStack = _card.Stack;
        }

        private void DragCard() {
            _card.DragTo(PointOnScreen);
            _dropArea = GetHoveredDropArea();
        }

        private void DropCard() {
            _dropArea.Drop(_card);

            if (_initialStack is TableuPile pile)
                pile.CardPileRoot = null;

            else _initialStack?.Take();
            
        }

        private void RollBack() => _card.DragTo(_initialPosition);
        
        private void ClearData() {
            _isBeingDragged = false;
            _initialStack = null;
            _dropArea = null;
        }

        #endregion

    }
}