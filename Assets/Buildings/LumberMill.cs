using UnityEngine;

namespace RTS
{
    public class LumberMill : MonoBehaviour, IDropoff
    {
        [SerializeField] private Transform _resourceDropOff;
        Building _building;
        ResourceData _resourceData;

        void Start()
        {
            _building = GetComponent<Building>();
            _resourceData = _building.Player.ResourceData;
        }
        public Vector3 DropPoint => _resourceDropOff.position;

        public void DropResources(ResourceType type, int amount)
        {
            switch (type)
            {
                default: _resourceData.AmendTimber(amount);
                    return;
            }
        }

    } 
}
