using System;
using UnityEngine;

namespace Amheklerior.Solitaire {

    [RequireComponent(typeof(Collider))]
    public class PlayerStackInteractionController : MonoBehaviour {

        [SerializeField] private PlayerColumnStack _playerStack;

        public bool Interactible => _playerStack.HasCards && !_playerStack.PlayableCard;

        private void OnMouseUpAsButton() {
            //if (!Interactible || _playerStack.TopCard.IsFacingUp) return;
            _playerStack.PlayableCard = _playerStack.Take();
            _playerStack.PlayableCard.Flip();
        }
        /*
        public void OnDrop(IDraggable dropped) {
            if (dropped is Card droppedCard) {
                var topCard = _playerStack.TopCard;
                if (!topCard) {
                    if (droppedCard.Number != Number.K) DragAndDrop.RollBack();

                    _playerStack.PlayableCard = droppedCard;
                    droppedCard.Prev = null;
                }
                //if (!topCard.IsFacingUp) DragAndDrop.RollBack();
                if (droppedCard.SameColor(topCard) || !droppedCard.IsNextLowNumber(topCard)) DragAndDrop.RollBack();

                topCard.Next = droppedCard;
                if (droppedCard.Prev) droppedCard.Prev.Pile = null;
                droppedCard.Prev = topCard;
            }
        }
        */
    }
}
