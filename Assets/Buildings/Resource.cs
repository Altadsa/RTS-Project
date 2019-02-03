using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace RTS
{
    public class Resource : MonoBehaviour
    {
        [SerializeField] ResourceType _resourceType;
        [SerializeField] int _resourcesLeft = 1500;
        [SerializeField] int _loadWeight = 1;

        public int Mine()
        {
            DestroyDepletedResource();
            return _loadWeight;
            //switch (resourceType)
            //{
            //    case ResourceType.Timber: ResourceData.AmendTimber(1);
            //        break;
            //    case ResourceType.Gold: ResourceData.AmendGold(1);
            //        break;
            //    case ResourceType.Food: ResourceData.AmendFood(1);
            //        break;
            //    default:
            //        return;
            //}
        }

        public ResourceType ResourceType() { return _resourceType; }

        private void DestroyDepletedResource()
        {
            if (_resourcesLeft <= 0) Destroy(gameObject);
            _resourcesLeft--;
        }
    }
}