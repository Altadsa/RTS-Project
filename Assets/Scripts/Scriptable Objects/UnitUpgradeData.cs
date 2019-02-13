using UnityEngine;

namespace RTS
{
    [CreateAssetMenu(menuName = "RTS/Producible/Unit Upgrades")]
    public class UnitUpgradeData : ProductionData, IQueueable, IRequireable
    {
        [SerializeField] FloatVar _valueToUpgrade;
        [SerializeField] float _upgradeValue;
        [SerializeField] float _time;
        public float Time { get { return _time; } set { _time = value; } }

        public void OnProductionComplete(Building productionBuilding)
        {
            Debug.Log(string.Format("{0} upgraded....", _title));
            _valueToUpgrade.Value += _upgradeValue;
            OnItemComplete();
        }

        public void OnItemComplete()
        {
            UpgradeManager.CompleteUpgrade(this);
        }
    }
}
