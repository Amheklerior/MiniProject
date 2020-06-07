using UnityEngine;
using Amheklerior.Core.EventSystem;

namespace Amheklerior.Solitaire.UI {

    public class Victory : GameEventListener {

        [SerializeField] private GameObject _game;
        [SerializeField] private GameObject _ingameUI;
        [SerializeField] private GameObject _victoryScreen;

        public override void OnEventRaised() {
            Debug.Log("YOU WON! Congrats..");
            _victoryScreen.SetActive(true);
            _game.SetActive(false);
            _ingameUI.SetActive(false);
        }
    }

}
