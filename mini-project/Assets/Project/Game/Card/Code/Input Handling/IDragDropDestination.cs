
namespace Amheklerior.Solitaire {

    public interface IDragDropDestination {
        bool ValidDropPositionFor(CardController card);
        void Drop(CardController card);
        void UndoDrop(CardController card);
    }

}