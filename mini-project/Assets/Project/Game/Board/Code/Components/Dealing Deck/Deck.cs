﻿using System.Collections.Generic;
using UnityEngine;

namespace Amheklerior.Solitaire {

    public class Deck : CardStackComponent {

        protected override Vector3 NextStackPosition => _stackPosition;
        
        protected override void OnTake(CardController card) {
            base.OnTake(card);
            card.transform.Translate(Vector3.back * 3f);
            if (_stack.HasCards) TopCard.Activate();
        }

        protected override void OnPutAll(ICollection<CardController> cards) {
            base.OnPutAll(cards);
            foreach (var card in cards) {
                card.transform.position = _stackPosition;
                card.Hide();
            }
            _stack.TopCard.Activate();
        }
        
    }
}
