using UnityEngine;

namespace Amheklerior.Solitaire.OLD {
    
    [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
    public class Card : MonoBehaviour {

        public bool Selectable => true; // IsFacingUp;
        
        private bool _isSelected = false;

        private void OnMouseDown() {
            if (Selectable) {
                Debug.Log($"The {name} has been selected");
                _isSelected = true;
            }
        }

        private void OnMouseDrag() {
            if (_isSelected) {
                Debug.Log($"The {name} is dragged");
                var fingerPointer = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                fingerPointer.z = -10f;
                //MoveTo(fingerPointer);
            }
        }

        private void OnMouseUp() {
            _isSelected = false;
            Debug.Log($"The {name} has been dropped");
        }

        private void OnMouseUpAsButton() {
            var interactible = true; // TRUE IF THE CARD IS SHOWING THE BACK AND IS THE TOP CARD OF THE STACK
            if (interactible) {
                //Flip(); // Flip into place and show the front side.
            }
        }

    }
}
