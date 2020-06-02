using UnityEngine;

namespace Amheklerior.Solitaire {

    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerStackInteractionController : MonoBehaviour {

        [SerializeField] private PlayerColumnStack _playerStack;

        public bool Interactible => _playerStack.HasCards && !_playerStack.PlayableCard;

        private void OnMouseUpAsButton() {
            if (!Interactible || _playerStack.TopCard.IsFacingUp) return;
            _playerStack.PlayableCard = _playerStack.Take();
            _playerStack.PlayableCard.Flip();
        }

    }
}
