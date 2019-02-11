using UnityEngine;

namespace RTS
{
    [CreateAssetMenu(menuName = "RTS/Producible/Unit Upgrades")]
    public class UnitUpgradeData : ProductionData
    {
        [SerializeField] FloatVar _valueToUpgrade;
        [SerializeField] float _upgradeValue;

        public override void OnComplete(Building productionBuilding)
        {
            Debug.Log(string.Format("{0} upgraded....", _title));
            _valueToUpgrade.Value += _upgradeValue;
        }
    }
}
