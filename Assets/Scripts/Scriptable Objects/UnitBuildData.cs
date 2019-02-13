using UnityEngine;

namespace RTS
{
    [CreateAssetMenu(menuName = "RTS/Producible/UnitBuild Data")]
    public class UnitBuildData : ProductionData, IQueueable
    {
        [SerializeField] GameObject _unit; 
        public GameObject Unit { get { return _unit; } }

        [SerializeField] float _time;
        public float Time { get { return _time; } set { _time = value; } }

        public void OnProductionComplete(Building productionBuilding)
        {
            Transform unitParent = GameObject.FindGameObjectWithTag("Active Units").transform;
            GameObject nUnit = Instantiate(_unit, productionBuilding.spawnPoint, Quaternion.identity, unitParent);
            Debug.Log("Unit Built!");
        }

    } 
}
