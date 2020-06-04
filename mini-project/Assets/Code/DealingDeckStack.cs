﻿using System.Collections.Generic;
using UnityEngine;

namespace Amheklerior.Solitaire {

    public class DealingDeckStack : CardStackComponent {
        
        protected override void OnPut(Card card) {
            base.OnPut(card);
            card.Flip();
        }

        protected override void OnTake(Card card) {
            base.OnTake(card);
            card.transform.Translate(Vector3.back * 3f);
            if (_stack.HasCards) TopCard.gameObject.SetActive(true);
        }
        
        protected override void OnPutAll(ICollection<Card> cards) {
            foreach (var card in cards)
                card.transform.position = _stackPosition;
            _stack.TopCard.gameObject.SetActive(true);
        }
        
    }
}
