using UnityEngine;

namespace RTS
{
    [CreateAssetMenu(menuName = "RTS/Upgrades/Unit Upgrades")]
    public class UnitUpgradeData : UpgradeData
    {
        [SerializeField] FloatVar _upgradeValue;

        public override void OnComplete(Building productionBuilding)
        {
            Debug.Log(string.Format("{0} upgraded....", _upgradeTitle));
            _upgradeValue.Value *= 1.25f;
        }
    }
}
