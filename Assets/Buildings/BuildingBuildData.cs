using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    [CreateAssetMenu(menuName = "RTS/Building Build Data")]
    public class BuildingBuildData : ScriptableObject
    {
        [SerializeField] GameObject _constructionPrefab;
        [SerializeField] GameObject _buildingPrefab;
        [SerializeField] int _goldCost;
        [SerializeField] int _timberCost;
        [SerializeField] Sprite _buildingIcon;

        public GameObject ConstructionPrefab { get { return _constructionPrefab; } }
        public GameObject BuildingPrefab { get { return _buildingPrefab; } }
        public int GoldCost { get { return _goldCost; } }
        public int TimberCost { get { return _timberCost; } }
        public Sprite Icon { get { return _buildingIcon; } }
    } 
}
