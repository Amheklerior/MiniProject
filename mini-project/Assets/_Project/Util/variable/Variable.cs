using UnityEngine;

namespace Amheklerior.Solitaire.Util {

    public abstract class Variable<T> : ScriptableObject {

        [Tooltip("The default value for this variable.")]
        [SerializeField] protected T _defaultValue;

        protected T _currentValue;

        public virtual T CurrentValue {
            get => _currentValue;
            set => _currentValue = value;
        }

        private void Awake() => ResetToDefaultValue();

        private void OnDisable() => ResetToDefaultValue();

        protected void ResetToDefaultValue() => _currentValue = _defaultValue;

    }

    #region Concrete scriptable-object definitions 

    [CreateAssetMenu(menuName = "Util/Variable/Integer")]
    public class IntVariable : Variable<int> { }

    [CreateAssetMenu(menuName = "Util/Variable/Float")]
    public class FloatVariable : Variable<float> { }

    [CreateAssetMenu(menuName = "Util/Variable/String")]
    public class StringVariable : Variable<string> { }

    [CreateAssetMenu(menuName = "Util/Variable/Vector2")]
    public class Vector2Variable : Variable<Vector2> { }

    [CreateAssetMenu(menuName = "Util/Variable/Vector3")]
    public class Vector3Variable : Variable<Vector3> { }

    #endregion

}
