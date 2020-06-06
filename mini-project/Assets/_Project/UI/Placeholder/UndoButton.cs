using UnityEngine;
using Amheklerior.Solitaire.Util;

namespace Amheklerior.Solitaire.UI {

    public interface IButton {
        void OnClick();
    }

    public class UndoButton : MonoBehaviour, IButton {

        [SerializeField] private IntVariable _movesCounter;

        public void OnClick() {
            if (!GlobalCommandExecutor.CanUndo()) return;
            GlobalCommandExecutor.Undo();
            _movesCounter.CurrentValue++;
        }

    }
}