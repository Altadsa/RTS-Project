using UnityEngine;
using System.Collections.Generic;

namespace RTS
{

    public abstract class ProductionData : ScriptableObject
    {
        [SerializeField] protected string _title;
        [SerializeField] protected string _description;
        [SerializeField] protected Sprite _icon;
        [SerializeField] protected ResourceCost _cost;
        [SerializeField] protected List<IRequireable> _requirements = new List<IRequireable>();

        public bool RequirementsMet()
        {
            foreach (var req in _requirements)
            {
                if (UpgradeManager.PuData[req] == false) { Debug.Log("You haven't met all the requirements..."); return false; }
            }
            return true;
        }

        public string Name { get { return _title; } }
        public string Description { get { return _description; } }
        public Sprite Icon { get { return _icon; } }
        public ResourceCost Cost { get { return _cost; } }
        public List<IRequireable> Requirements { get { return _requirements; } }

    }

}
