using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class Headquarters : MonoBehaviour, IDropoff
    {
        [SerializeField] private Transform _resourceDropOff;
        Building _building;
        ResourceData _resourceData;

        public Vector3 DropPoint => _resourceDropOff.position;

        private void Start()
        {
            _building = GetComponent<Building>();
            _resourceData = _building.Player.ResourceData;
            _resourceData.AmendMaxFood(12);   
        }

        public void DropResources(ResourceType resource, int amount)
        {
            switch (resource)
            {
                case ResourceType.Gold:
                    _resourceData.AmendGold(amount);
                    break;
                case ResourceType.Timber:
                    _resourceData.AmendTimber(amount);
                    break;
            }
        }

    }
}
