using System.Collections;
using UnityEngine;
using Amheklerior.Solitaire.Util;

namespace Amheklerior.Solitaire {

    [RequireComponent(typeof(Deck), typeof(BoxCollider2D))]
    public class DeckInputHandler : MonoBehaviour {

        [Header("Settings:")]
        [SerializeField] private TalonStack _talon;
        [SerializeField] private float _resetDeckAnimationTime = 0.05f;

        private void OnMouseUpAsButton() {
            if (Game.IsBusy) return;
            Game.IncrementMovesCounter();
            if (_deck.HasCards) {
                GlobalCommandExecutor.Execute(() => PlaceACardToTalon(), () => PlaceACardBackToDeck());
            } else {
                GlobalCommandExecutor.Execute(() => ResetDeck(), () => UndoResetDeck());
            }
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
        private void PlaceACardToTalon() {
            var card = _deck.Take();
            card.Flip();
            _talon.Put(card);
        }

        [ContextMenu("Undo - Place a Card to the Talon Stack")]
        private void PlaceACardBackToDeck() {
            var card = _talon.Take();
            card.transform.Translate(Vector3.back * 3f);
            card.Flip();
            _deck.Put(card);
        }

        [ContextMenu("Reset Deck")]
        private void ResetDeck() {
            StartCoroutine(ResetDeck_Coroutine());
            Game.UpdateScoreBy(GameScore.RESET_DECK);
        }

        [ContextMenu("Undo - Reset Deck")]
        private void UndoResetDeck() {
            StartCoroutine(UndoResetDeck_Coroutine());
            Game.UpdateScoreBy(-(int) GameScore.RESET_DECK);
        }

        #region Coroutines 

        private IEnumerator ResetDeck_Coroutine() {
            var waitAnimationTime = new WaitForSeconds(_resetDeckAnimationTime);
            while (_talon.HasCards) {
                PlaceACardBackToDeck();
                yield return waitAnimationTime;
            }
        }

        private IEnumerator UndoResetDeck_Coroutine() {
            var waitAnimationTime = new WaitForSeconds(_resetDeckAnimationTime);
            while (_deck.HasCards) {
                PlaceACardToTalon();
                yield return waitAnimationTime;
            }
        }

        #endregion

        #endregion

    }
}
