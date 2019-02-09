using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class Headquarters : MonoBehaviour
    {
        [SerializeField] private Transform _resourceDropOff;
        public Vector3 DropOffPoint
        {
            get { return _resourceDropOff.position; }
        }

        public void DropOffResources(ResourceType resource, int amount)
        {
            switch (resource)
            {
                case ResourceType.Food:
                    ResourceData.AmendFood(amount);
                    break;
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
