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
            Transform unitParent = GameObject.FindGameObjectWithTag("Active Units").transform;
            GameObject nUnit = Instantiate(_unit, productionBuilding.spawnPoint, Quaternion.identity, unitParent);
            Debug.Log("Unit Built!");
        }

    } 
}
