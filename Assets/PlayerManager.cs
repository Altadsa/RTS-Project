using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class PlayerManager : MonoBehaviour
    {
        public List<BuildingBuildData> _dataToSet = new List<BuildingBuildData>();
        public static List<BuildingBuildData> _availableBuildings { get; private set; }

        void Start()
        {
            _availableBuildings = new List<BuildingBuildData>();
            _availableBuildings.AddRange(_dataToSet);
            ResourceData.AmendFood(500); ResourceData.AmendGold(500); ResourceData.AmendTimber(500);
        }

    } 
}
