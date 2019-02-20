using UnityEngine;

namespace RTS
{
    public class House : MonoBehaviour
    {
        Building _building;
        ResourceData _resourceData;
        [SerializeField] int _foodValue;

        void Start()
        {
            _building = GetComponent<Building>();
            _resourceData = _building.Player.ResourceData;
            _resourceData.AmendMaxFood(_foodValue);
        }

        private void OnDestroy()
        {
            _resourceData.AmendMaxFood(-_foodValue);
        }
    } 
}
