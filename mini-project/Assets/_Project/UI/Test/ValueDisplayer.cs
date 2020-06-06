using UnityEngine;
using UnityEngine.UI;
using Amheklerior.Solitaire.Util;

[RequireComponent(typeof(Text))]
public class ValueDisplayer : MonoBehaviour {

    [SerializeField] private IntVariable _valueToDisplay;

    private Text _text;

    private void Awake() => _text = GetComponent<Text>();

    public void Update() => _text.text = _valueToDisplay.CurrentValue.ToString();
    
    // TODO -- Use events instead of an int variable

}
