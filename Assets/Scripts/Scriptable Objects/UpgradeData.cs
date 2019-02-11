using UnityEngine;
using System.Collections.Generic;

namespace RTS
{

    public abstract class UpgradeData : ScriptableObject, IQueueable
    {
        [SerializeField] protected string _upgradeTitle;
        [SerializeField] protected Sprite _upgradeIcon;
        [SerializeField] protected Vector3Int _cost;
        [SerializeField] protected float _timeNeeded;
        [SerializeField] protected List<UpgradeData> _requirements;

        public abstract void OnComplete(Building productionBuilding);

        public bool RequirementsMet()
        {
            foreach (var req in _requirements)
            {
                if (UpgradeManager.PuData[req] == false) { Debug.Log("You haven't met all the requirements..."); return false; }
            }
            return true;
        }

        public string Name() { return _upgradeTitle; }
        public Sprite Icon() { return _upgradeIcon; }
        public List<UpgradeData> Requirements { get { return _requirements; } }
        public float TimeNeeded() { return _timeNeeded; }
        public Vector3Int Cost() { return _cost; }
    }

}
