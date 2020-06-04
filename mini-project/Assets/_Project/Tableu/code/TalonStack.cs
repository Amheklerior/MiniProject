using UnityEngine;

namespace Amheklerior.Solitaire {

    public class TalonStack : CardStackComponent {
        
        protected override void OnPut(Card card) {
            base.OnPut(card);
            card.Flip();
        }
        
    }
}
