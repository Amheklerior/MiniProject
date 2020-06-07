using System;

namespace Amheklerior.Solitaire.Util {

    public class Command : ICommand {
        
        private readonly Action _action;
        private readonly Action _undo;
        
        public Command(Action action, Action undo = null) {
            _action = action;
            _undo = undo;
        }

        public bool Reversible => _undo != null;

        public void Perform() => _action.Invoke();

        public void Undo() {
            if (Reversible) _undo.Invoke();
        }

    }
}
