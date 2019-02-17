using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    [RequireComponent(typeof(UpgradeManager))]
    public class PlayerManager : MonoBehaviour
    {
        public List<BuildingBuildData> _dataToSet = new List<BuildingBuildData>();
        public static List<BuildingBuildData> _availableBuildings { get; private set; }

        public static int a = 10;

        void Start()
        {
            _availableBuildings = new List<BuildingBuildData>();
            _availableBuildings.AddRange(_dataToSet);
            ResourceData.AmendGold(500); ResourceData.AmendTimber(500);
        }

    } 
}
