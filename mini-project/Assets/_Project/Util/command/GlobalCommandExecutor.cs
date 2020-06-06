using UnityEngine;
using System;

namespace Amheklerior.Solitaire.Util {

    public static class GlobalCommandExecutor {
        
        public static void Execute(ICommand command) => Executor.Execute(command);

        public static void Execute(Action perform) => Executor.Execute(new Command(perform));

        public static void Execute(Action perform, Action undo) => Executor.Execute(new Command(perform, undo));

        public static bool CanUndo() => Executor.CanUndo;

        public static void Undo() => Executor.Undo();

        #region Internals 
        
        private static CommandExecutorComponent _cmdExecutor;

        private static CommandExecutorComponent Executor => _cmdExecutor ??
            (_cmdExecutor = new GameObject("Command Executor").AddComponent<CommandExecutorComponent>());
        
        #endregion

    }
}
