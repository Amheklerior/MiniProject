using UnityEngine;

namespace Amheklerior.Solitaire {

    [CreateAssetMenu(menuName = "Solitaire/Card/Sprite Provider")]
    public class SpriteProvider : ScriptableObject {

        [SerializeField] private Sprite[] _sprites;

        public Sprite Get(int index) => _sprites?[index];

    }
}