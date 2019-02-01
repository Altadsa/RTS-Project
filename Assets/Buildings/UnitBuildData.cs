using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace RTS
{
    [CreateAssetMenu(menuName = "RTS/UnitBuild Data")]
    public class UnitBuildData : ScriptableObject
    {
        [SerializeField] GameObject unit;
        [SerializeField] int _goldCost;
        [SerializeField] int _timberCost;
        [SerializeField] int _foodCost;
        
        public GameObject Unit { get { return unit; } }
        public int GoldCost { get { return _goldCost; } }
        public int TimberCost{ get { return _timberCost; } }
        public int FoodCost { get { return _foodCost; } }
    } 
}
