using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class UpgradeManager : MonoBehaviour
    {
        [SerializeField] List<UpgradeData> _data;
        public static Dictionary<UpgradeData, bool> _playerUpgradeData = new Dictionary<UpgradeData, bool>();
        public static Dictionary<UpgradeData, bool> PuData { get { return _playerUpgradeData; } }

        private void Start()
        {
            foreach (var data in _data)
            {
                _playerUpgradeData.Add(data, false);
            }
        }

        public static void CompleteUpgrade(UpgradeData data)
        {
            _playerUpgradeData[data] = true;
        }

        [SerializeField] FloatVar _meleeDamage;
        [SerializeField] FloatVar _meleeArmour;
        public float MDamage { get { return _meleeDamage.Value; } }
        public int MArmour { get { return (int)_meleeArmour.Value; } }

    } 
}
