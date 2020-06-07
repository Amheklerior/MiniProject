using UnityEngine;
using Amheklerior.Core.Command;

namespace Amheklerior.Solitaire {

    [RequireComponent(typeof(Collider))]
    public class TableuPileInputHandler : MonoBehaviour {

        public enum UpdateDirection {
            FORWARD,
            BACKWARD
        }

        private void OnMouseUpAsButton() {
            if (!Interactible || Game.IsBusy) return;
            GlobalCommandExecutor.Execute(() => _pile.FlipTopCard(), () => _pile.UndoFlipTopCard());
        }

        public void UpdateColliderPosition(UpdateDirection direction) {
            if (direction == UpdateDirection.FORWARD)
                _collider.localPosition += _step;
            else
                _collider.localPosition -= _step;
        }

        public void Init(float verticalOffset, float depthOffset) {
            _pile = GetComponentInParent<TableuPile>();
            _collider = transform;
            _step = new Vector3(0f, verticalOffset, depthOffset);
        }


        #region Internals

        private Vector3 _step;
        private Transform _collider;
        private TableuPile _pile;

        private bool Interactible => _pile.HasCards && !_pile.CardPileRoot;

        #endregion

    }
}
