using UnityEngine;

namespace Amheklerior.Solitaire {

    public class InputHandler : MonoBehaviour {

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
        private ICardDropArea _dropArea;

        private bool IsOnValidDropPosition => _dropArea != null && _dropArea.ValidDropPositionFor(_card);

        private Vector3 PointOnScreen {
            get {
                var pointer = _cam.ScreenToWorldPoint(Input.mousePosition);
                pointer.z = -10f;
                return pointer;
            }
        }

        private void Awake() {
            _tranform = transform;
            _card = GetComponent<Card>();
            _cam = Camera.main;
        }

        private ICardDropArea GetHoveredDropArea() {
            var rayOrigin = _cam.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 15f;
            Debug.DrawLine(rayOrigin, rayOrigin + Vector3.forward * 100f, UnityEngine.Color.yellow);
            if (Physics.Raycast(rayOrigin, Vector3.forward, out RaycastHit hit, 100f)) {
                return hit.transform.GetComponentInParent<ICardDropArea>();
            }
            return null;
        }

        private void StartDraggingCard() {
            _isBeingDragged = true;
            _initialPosition = _tranform.position;
        }

        private void DragCard() {
            _card.DragTo(PointOnScreen);
            _dropArea = GetHoveredDropArea();
        }

        private void DropCard() => _dropArea.Drop(_card);

        private void RollBack() => _card.DragTo(_initialPosition);

        private void ClearData() {
            _isBeingDragged = false;
            _dropArea = null;
        }

        #endregion

    }
}