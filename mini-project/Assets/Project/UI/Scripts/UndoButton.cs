using UnityEngine;
using Amheklerior.Core.Command;

namespace Amheklerior.Solitaire.UI {

    public interface IButton {
        void OnClick();
    }

    public class UndoButton : MonoBehaviour, IButton {
        
        public void OnClick() {
            if (!GlobalCommandExecutor.CanUndo()) return;
            GlobalCommandExecutor.Undo();
        }

    }
}