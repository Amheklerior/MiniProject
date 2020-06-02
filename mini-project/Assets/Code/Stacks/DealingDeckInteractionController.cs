using UnityEngine;

namespace Amheklerior.Solitaire {

    [RequireComponent(typeof(BoxCollider2D))]
    public class DealingDeckInteractionController : MonoBehaviour {

        [SerializeField] private DealingDeckStack _deckStack;

        private void OnMouseUpAsButton() {
            if (_deckStack.HasCards) Debug.Log("PICKUP CARD");
            else Debug.Log("REFILL STACK");
        }
    }

}
