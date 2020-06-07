using UnityEngine;
using Amheklerior.Core.EventSystem;
using Amheklerior.Core.Command;

namespace Amheklerior.Solitaire {

    public class AutostackHandler : MonoBehaviour, IEventListener<CardController> {
        
        [SerializeField] protected CardEvent _autostackEvent;

        private IDragDropDestination _autostackDestination;

        public void OnEventRaised(CardController card) {
            if (_autostackDestination.ValidDropPositionFor(card))
                GlobalCommandExecutor.Execute(new SolitaireMove(card, card.Pile.Previous ?? (IDragDropOrigin) card.Stack, _autostackDestination));
        }

        protected virtual void Awake() {
            if (!_autostackEvent) 
                Debug.LogWarning("No card autostack event has been set in the inspector.", this);
            _autostackDestination = GetComponent<IDragDropDestination>();
        }

        protected virtual void OnEnable() => _autostackEvent.Subscribe(OnEventRaised);
        protected virtual void OnDisable() => _autostackEvent.Unsubscribe(OnEventRaised);
        
    }
}