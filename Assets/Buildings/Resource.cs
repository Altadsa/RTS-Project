using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace RTS
{
    public class Resource : MonoBehaviour
    {
        [SerializeField] private ResourceType resourceType;
        public void Mine()
        {
            switch (resourceType)
            {
                case ResourceType.Timber: ResourceData.AmendTimber(1);
                    break;
                case ResourceType.Gold: ResourceData.AmendGold(1);
                    break;
                case ResourceType.Food: ResourceData.AmendFood(1);
                    break;
                default:
                    return;
            }
        }
    }
}