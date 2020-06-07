
namespace Amheklerior.Solitaire.Util {

    public interface ICommand {
        bool Reversible { get; }
        void Perform();
        void Undo();
    }

}
