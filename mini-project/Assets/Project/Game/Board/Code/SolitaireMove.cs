using UnityEngine;
using Amheklerior.Core.Command;

namespace Amheklerior.Solitaire {

    public class SolitaireMove : ICommand {

        public bool Reversible => true;

        public void Perform() {
            Debug.Log($"Move card {_movedCard} from {_origin} to {_destination}.");
            _destination.Drop(_movedCard);
            if (_origin is TableuPile pile)
                pile.CardPileRoot = null;
            else if (_origin is CardStackComponent stack)
                stack.Take();

            UpdateScore(_origin, _destination);
            Game.IncrementMovesCounter();
        }

        public void Undo() {
            Debug.Log($"Move card {_movedCard} from {_destination} back to {_origin}.");
            _destination.UndoDrop(_movedCard);
            if (_origin is TalonStack talon)
                talon.Put(_movedCard);
            else
                ((IDragDropDestination) _origin).Drop(_movedCard);

            UndoUpdateScore(_origin, _destination);
            Game.IncrementMovesCounter();
        }

        #region Internals

        private CardController _movedCard;
        private IDragDropOrigin _origin;
        private IDragDropDestination _destination;

        public SolitaireMove(CardController cardToMove, IDragDropOrigin origin, IDragDropDestination destination) {
            _movedCard = cardToMove;
            _origin = origin;
            _destination = destination;
        }

        private static void UpdateScore(IDragDropOrigin origin, IDragDropDestination destination) =>
            Game.UpdateScoreBy(GetScore(origin, destination));

        private static void UndoUpdateScore(IDragDropOrigin origin, IDragDropDestination destination) =>
            Game.UpdateScoreBy(-GetScore(origin, destination));


        private static int GetScore(IDragDropOrigin origin, IDragDropDestination destination) {
            if (destination is FoundationStack)
                return (int) GameScore.MOVE_CARD_TO_FOUNDATION_STACK;

            if (origin is TalonStack)
                return (int) GameScore.MOVE_CARD_FROM_TALON_TO_TABLEU_PILE;

            if (origin is TableuPile)
                return (int) GameScore.MOVE_CARD_BETWEEN_TABLEU_PILES;

            if (origin is FoundationStack)
                return (int) GameScore.MOVE_CARD_FROM_FOUNDATION_STACK_TO_TABLEU_PILE;

            else
                return (int) GameScore.NO_SCORE;
        }

        #endregion

    }
}