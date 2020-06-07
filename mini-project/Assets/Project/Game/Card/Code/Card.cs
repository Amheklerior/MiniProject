
namespace Amheklerior.Solitaire {
    
    public struct Card {
        public static readonly int CARDS_COUNT = 52;
        public static readonly int CARDS_PER_SEED = 13;
        public static readonly bool FACING_UP = true;
        public static readonly bool FACING_DOWN = false;

        public Card(Seed seed, Number number) {
            Seed = seed;
            Number = number;
        }

        public Seed Seed { get; }
        public Number Number { get; }
        public Color Color => (Color) ((int) Seed / 2);
    }

}
