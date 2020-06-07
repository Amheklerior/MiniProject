using System.Collections.Generic;

namespace Amheklerior.Solitaire {
    
    public interface ICardStack {
        bool HasCards { get; }
        CardController TopCard { get; }
        int CardCount { get; }
        void Put(CardController card);
        void PutAll(ICollection<CardController> cards);
        CardController Take();
        ICollection<CardController> TakeAll();
        void Clear();
    }

}