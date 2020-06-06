using System.Collections.Generic;

namespace Amheklerior.Solitaire.Util {

    internal class CommandStack : Stack<ICommand> { }

    public class CommandExecutor : ICommandExecutor {

        private readonly CommandStack _cmdStack = new CommandStack();

        public bool CanUndo => _cmdStack.Count > 0;

        public int Count => _cmdStack.Count;

        public virtual ICommand LastExcecuted => _cmdStack.Peek();

        public virtual void Execute(ICommand cmd) {
            cmd.Perform();

            if (cmd.Reversible) _cmdStack.Push(cmd);
            else _cmdStack.Clear();
        }

        public virtual void Undo() => _cmdStack.Pop().Undo();

    }
}
