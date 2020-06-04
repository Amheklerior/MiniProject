
namespace Amheklerior.Solitaire {

    #region Enums

    public enum Seed {
        HEARTS = 0,
        SQUARES = 1,
        CLUBS = 2,
        SPADES = 3
    }

    public enum Number {
        A = 0,
        TWO = 1,
        THREE = 2,
        FOUR = 3,
        FIVE = 4,
        SIX = 5,
        SEVEN = 6,
        EIGHT = 7,
        NINE = 8,
        TEN = 9,
        J = 10,
        Q = 11,
        K = 12
    }

    public enum Color {
        RED = 0,
        BLACK = 1
    }

    #endregion
    
    public struct CardData {
        public static readonly bool FACING_UP = true;
        public static readonly bool FACING_DOWN = false;

        public CardData(Seed seed, Number number) {
            Seed = seed;
            Number = number;
        }

        public Seed Seed { get; }
        public Number Number { get; }
        public Color Color => (Color) ((int) Seed / 2);
        
    }

}
