using UnityEngine;

namespace RTS
{
    [CreateAssetMenu(menuName = "RTS/Fields/Float Variable")]
    public class FloatVar : ScriptableObject
    {
        [SerializeField] float _value = 0;
        public float Value { get { return _value; } set { _value = value; } }
    }
}
