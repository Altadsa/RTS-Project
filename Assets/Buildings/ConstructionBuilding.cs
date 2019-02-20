using UnityEngine;

namespace RTS
{
    public class ConstructionBuilding : MonoBehaviour
    {
        [SerializeField] int _constructionWorkNeeded = 100;
        GameObject _buildingPrefab;
        int _constructionProgress = 0;

        public PlayerInformation Player { get; private set; }

        public void Setup(GameObject finishedBuilding, PlayerInformation player)
        {
            _buildingPrefab = finishedBuilding;
            if (Player == null) Player = player;
        }

        public void AddConstructionProgress()
        {
            Debug.Log(_constructionProgress);
            _constructionProgress += 10;
            if (_constructionProgress >= _constructionWorkNeeded)
            {
                ConstructBuilding();
            }
        }

        private void ConstructBuilding()
        {
            GameObject finishedBuilding = Instantiate(_buildingPrefab, transform.position, Quaternion.identity, transform.parent);
            Building building = finishedBuilding.GetComponent<Building>();
            building.SetPlayer(Player);
            Destroy(gameObject);
        }
    }
}
