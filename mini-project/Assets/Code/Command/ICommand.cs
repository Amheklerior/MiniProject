
namespace Amheklerior.Solitaire.Command {

    public interface ICommand {
        bool Reversible { get; }
        void Perform();
        void Undo();
    }

}
