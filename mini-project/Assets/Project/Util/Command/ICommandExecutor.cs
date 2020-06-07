
namespace Amheklerior.Solitaire.Util {

    public interface ICommandExecutor {
        ICommand LastExcecuted { get; }
        int Count { get; }
        bool CanUndo { get; }
        void Execute(ICommand command);
        void Undo();
    }

}
