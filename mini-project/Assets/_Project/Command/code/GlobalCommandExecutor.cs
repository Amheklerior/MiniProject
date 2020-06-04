using UnityEngine;

namespace Amheklerior.Solitaire.Command {

    public static class GlobalCommandExecutor {
        
        private static CommandExecutorComponent _cmdExecutor;
        
        public static CommandExecutorComponent Executor {
            get {
                if (_cmdExecutor == null) {
                    _cmdExecutor = new GameObject("Command Executor").AddComponent<CommandExecutorComponent>();
                }
                return _cmdExecutor;
            }
        }

        public static void Execute(ICommand command) => Executor.Execute(command);

        public static void Undo() => Executor.Undo();

    }
}
