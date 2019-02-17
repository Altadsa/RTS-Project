using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class Headquarters : MonoBehaviour, IDropoff
    {
        [SerializeField] private Transform _resourceDropOff;

        public Vector3 DropPoint => _resourceDropOff.position;

        private void OnEnable()
        {
            ResourceData.AmendMaxFood(12);   
        }

        public void DropResources(ResourceType resource, int amount)
        {
            switch (resource)
            {
                case ResourceType.Gold:
                    ResourceData.AmendGold(amount);
                    break;
                case ResourceType.Timber:
                    ResourceData.AmendTimber(amount);
                    break;
            }
        }

    }
}
