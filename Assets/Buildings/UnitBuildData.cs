using UnityEngine;

namespace RTS
{
    [CreateAssetMenu(menuName = "RTS/Producible/UnitBuild Data")]
    public class UnitBuildData : ProductionData
    {
        [SerializeField] GameObject _unit;       
        public GameObject Unit { get { return _unit; } }

        public override void OnComplete(Building productionBuilding)
        {
            GameObject nUnit = Instantiate(_unit, productionBuilding.spawnPoint, Quaternion.identity, productionBuilding.transform);
            Debug.Log("Unit Built!");
        }

    } 
}
