using UnityEngine;

namespace RTS
{
    [CreateAssetMenu(menuName = "RTS/Producible/Unit Upgrades")]
    public class UnitUpgradeData : ProductionData
    {
        [SerializeField] FloatVar _upgradeValue;

        public override void OnComplete(Building productionBuilding)
        {
            Debug.Log(string.Format("{0} upgraded....", _title));
            _upgradeValue.Value *= 1.25f;
        }
    }
}
