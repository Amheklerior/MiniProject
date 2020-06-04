
namespace Amheklerior.Solitaire {

    public class PickUpDeckStack : CardStackComponent {

        protected override void OnPut(Card card) {
            base.OnPut(card);
            card.Flip();
        }
        
    }
}
