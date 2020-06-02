using System;
using System.Collections;
using UnityEngine;

namespace Amheklerior.Solitaire {

    [RequireComponent(typeof(BoxCollider2D))]
    public class DealingDeckInteractionController : MonoBehaviour {

        [SerializeField] private DealingDeckStack _deckStack;
        [SerializeField] private PickUpDeckStack _pickUpStack;

        [SerializeField] private float _resetDeckAnimationTime = 0.05f;

        private void Awake() => CheckDependencies();
        
        private void OnMouseUpAsButton() {
            if (_deckStack.HasCards) DiscoverPickupCard();
            else ResetDeck();
        }

        #region Internals

        private void DiscoverPickupCard() => _pickUpStack.Put(_deckStack.Take());

        private void ResetDeck() => StartCoroutine(ResetDeck_Coroutine());

        private IEnumerator ResetDeck_Coroutine() {
            var waitAnimationTime = new WaitForSeconds(_resetDeckAnimationTime);
            while (_pickUpStack.HasCards) {
                _deckStack.Put(_pickUpStack.Take());
                yield return waitAnimationTime;
            }
        }

        private void CheckDependencies() {
            if (!_deckStack) {
                Debug.LogError("Dealing deck reference is not set.", _deckStack);
                throw new MissingReferenceException();
            }
            if (!_pickUpStack) {
                Debug.LogError("Pickup deck reference is not set.", _pickUpStack);
                throw new MissingReferenceException();
            }
        }

        #endregion

    }
}
