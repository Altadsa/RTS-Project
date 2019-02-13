using UnityEngine;

namespace RTS
{
    [CreateAssetMenu(menuName = "RTS/Building Build Data")]
    public class BuildingBuildData : ProductionData, IRequireable
    {
        [SerializeField] GameObject _constructionPrefab;
        [SerializeField] GameObject _buildingPrefab;

        public GameObject ConstructionPrefab { get { return _constructionPrefab; } }
        public GameObject BuildingPrefab { get { return _buildingPrefab; } }

        public void OnItemComplete()
        {
            UpgradeManager.CompleteUpgrade(this);
        }
    } 
}
