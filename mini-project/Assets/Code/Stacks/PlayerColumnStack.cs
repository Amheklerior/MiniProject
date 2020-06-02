using UnityEngine;

namespace Amheklerior.Solitaire {

    public class PlayerColumnStack : CardStackComponent {

        [SerializeField] protected float _verticalOffset = -0.2f;
        [SerializeField] protected float _depthOffset = -0.1f;

        protected override Vector3 Direction => new Vector3(0f, _verticalOffset, _depthOffset);

        /*
        protected override void OnPut(Card card) {
            base.OnPut(card);
            // specific behabior
        }

        protected override void OnTake(Card card) {
            base.OnPut(card);
            // specific behabior
        }
        */

    }

}
