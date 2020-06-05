using UnityEngine;
using Amheklerior.Solitaire.Command;

namespace Amheklerior.Solitaire.UI {

    public interface IButton {
        void OnClick();
    }

    public class UndoButton : MonoBehaviour, IButton {

        public void OnClick() => GlobalCommandExecutor.Undo();
    
    }

}