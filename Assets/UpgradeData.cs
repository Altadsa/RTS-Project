using UnityEngine;

namespace RTS
{
    public class UpgradeData : ScriptableObject
    {
        [SerializeField] float _timeNeeded;
        public float TimeNeeded { get { return _timeNeeded; } }
    }
}
