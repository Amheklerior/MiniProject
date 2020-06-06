using UnityEngine;

namespace Amheklerior.Solitaire.Util {

    public class CommandExecutorComponent : MonoBehaviour {

        private ICommandExecutor _executor;

        private void Awake() => _executor = GetExecutorInstance();
        
        public void Execute(ICommand command) => _executor.Execute(command);

        public void Undo() {
            if (!_executor.CanUndo) return;
            _executor.Undo();
        }

        protected virtual ICommandExecutor GetExecutorInstance() => new CommandExecutor();

    }
}