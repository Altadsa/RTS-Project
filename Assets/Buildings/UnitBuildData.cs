using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    [CreateAssetMenu(menuName = "RTS/UnitBuild Data")]
    public class UnitBuildData : ScriptableObject
    {
        [SerializeField] GameObject unit;
        [SerializeField] int cost;

        public GameObject Unit { get { return unit; } }
        public int Cost { get { return cost; } }
    } 
}
