using System.Collections;
using UnityEngine;

namespace Amheklerior.Solitaire {

    [RequireComponent(typeof(Deck), typeof(BoxCollider2D))]
    public class DeckInputHandler : MonoBehaviour {

        [Header("Settings:")]
        [SerializeField] private TalonStack _talon;
        [SerializeField] private float _resetDeckAnimationTime = 0.05f;

        private void OnMouseUpAsButton() {
            if (_deck.HasCards) PlaceACardToTalon();
            else ResetDeck();
        }

        #region Internals

        private Deck _deck;

        private void Awake() {
            _deck = GetComponent<Deck>();
            CheckDependencies();
        }

        private void CheckDependencies() {
            if (!_deck) {
                Debug.LogError("Dealing deck reference is not set.", this);
                throw new MissingReferenceException();
            }
            if (!_talon) {
                Debug.LogError("Pickup deck reference is not set.", this);
                throw new MissingReferenceException();
            }
        }

        [ContextMenu("Place a Card to the Talon Stack")]
        private void PlaceACardToTalon() => _talon.Put(_deck.Take());

        [ContextMenu("Reset Deck")]
        private void ResetDeck() => StartCoroutine(ResetDeck_Coroutine());

        private IEnumerator ResetDeck_Coroutine() {
            var waitAnimationTime = new WaitForSeconds(_resetDeckAnimationTime);
            while (_talon.HasCards) {
                _deck.Put(_talon.Take());
                yield return waitAnimationTime;
            }
        }
        
        #endregion

    }
}
