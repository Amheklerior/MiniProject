using UnityEngine;
using Amheklerior.Core.EventSystem;

namespace Amheklerior.Solitaire.UI {

    public class Controller : GameEventListener {

        [SerializeField] private GameObject _inGamePanel;
        [SerializeField] private GameObject _winningPanel;

        public override void OnEventRaised() {
            Debug.Log("YOU WON");
            _inGamePanel.SetActive(false);
            _winningPanel.SetActive(true);
        }

    }
}
