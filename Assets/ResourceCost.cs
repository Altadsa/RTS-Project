using UnityEngine;

namespace RTS
{
    [System.Serializable]
    public class ResourceCost
    {
        [SerializeField] int _goldCost;
        [SerializeField] int _timberCost;
        [SerializeField] int _foodCost;

        public int Gold { get { return _goldCost; } }
        public int Timber { get { return _timberCost; } }
        public int Food { get { return _foodCost; } }
    }
}
