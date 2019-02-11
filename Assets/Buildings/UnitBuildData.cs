using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RTS
{
    [CreateAssetMenu(menuName = "RTS/UnitBuild Data")]
    public class UnitBuildData : ScriptableObject, IQueueable
    {
        [SerializeField] GameObject unit;
        [SerializeField] Vector3Int _cost;
        [SerializeField] float _buildTime;
        [SerializeField] Sprite _queueImage;
        [SerializeField] UpgradeData[] _requirements;
            
        public GameObject Unit { get { return unit; } }
        public float BuildTime { get { return _buildTime; } }
        public Sprite QueueImage { get { return _queueImage; } }

        public string Name() { return unit.name; }
        public Sprite Icon() { return _queueImage;}
        public Vector3Int Cost() { return _cost; }
        public float TimeNeeded() { return _buildTime; }

        public void OnComplete(Building productionBuilding)
        {
            GameObject nUnit = Instantiate(unit, productionBuilding.spawnPoint, Quaternion.identity, productionBuilding.transform);
            Debug.Log("Unit Built!");
        }

        public bool RequirementsMet()
        {
            foreach (var req in _requirements)
            {
                if (UpgradeManager.PuData[req] == false) { Debug.Log("You haven't met all the requirements..."); return false;  }
            }
            return true;
        }

    } 
}
