using UnityEngine;

namespace RTS
{
    public class ConstructionBuilding : MonoBehaviour
    {
        int _constructionProgress = 0;
        [SerializeField] int _constructionWorkNeeded = 100;
        GameObject _buildingPrefab;

        public void Setup(GameObject finishedBuilding)
        {
            _buildingPrefab = finishedBuilding;
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
            Destroy(gameObject);
        }
    }
}
