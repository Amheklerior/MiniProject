using UnityEngine;

namespace Amheklerior.Solitaire.Util {

    public class CommandExecutorComponent : MonoBehaviour, ICommandExecutor {

        #region ICommandExecutor interface forwarding 

        public int Count => _executor.Count;
        public bool CanUndo => _executor.CanUndo;
        public ICommand LastExcecuted => _executor.LastExcecuted;
        public void Execute(ICommand command) => _executor.Execute(command);
        public void Undo() => _executor.Undo();

        #endregion

        #region Internals

        private ICommandExecutor _executor;

        private void Awake() => _executor = GetExecutorInstance();

        protected virtual ICommandExecutor GetExecutorInstance() => new CommandExecutor();

        #endregion

    }
}