using UnityEngine;
using UnityEngine.UI;
using Amheklerior.Core.Time;

namespace Amheklerior.Solitaire.UI {

    public class Clock : MonoBehaviour {

        void Update() => _text.text = $"{Minute}:{Second}";

        #region Internals 

        private Text _text;
        private Timer _timer = new Timer();
        
        private void Awake() => _text = GetComponent<Text>();
        private void Start() => _timer.Start();
        private void LateUpdate() => _timer.Tick(Time.deltaTime);

        private string Minute => Format(InMinutes(_timer.Current));
        private string Second => Format(RemainingSeconds(_timer.Current));

        private static int InMinutes(float seconds) => (int) seconds / 60;
        private static int RemainingSeconds(float seconds) => (int) seconds % 60;
        private static string Format(int number) => number.ToString().PadLeft(2, '0');
        
        #endregion

    }
}
