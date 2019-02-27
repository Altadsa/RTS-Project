using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class UpgradeManager : MonoBehaviour
    {

        [SerializeField]
        List<ProductionData> _data;
        public static Dictionary<IRequireable, bool> _playerUpgradeData = new Dictionary<IRequireable, bool>();
        public static Dictionary<IRequireable, bool> PuData { get { return _playerUpgradeData; } }

        private void Start()
        {
            foreach (var data in _data)
            {
                var iData = (IRequireable)data;
                if (iData == null) continue;
                _playerUpgradeData.Add(iData, false);
            }
        }

        public static void CompleteUpgrade(IRequireable data)
        {
            _playerUpgradeData[data] = true;
        }

        [SerializeField] FloatVar _meleeDamage;
        [SerializeField] FloatVar _meleeArmour;
        public float MDamage { get { return _meleeDamage.Value; } }
        public int MArmour { get { return (int)_meleeArmour.Value; } }

    } 
}
