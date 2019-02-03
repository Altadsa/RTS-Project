using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class Headquarters : MonoBehaviour
    {

        public void DropOffResources(ResourceType resource, int amount)
        {
            switch (resource)
            {
                case ResourceType.Food:
                    AddFood(amount);
                    break;
                case ResourceType.Gold:
                    AddGold(amount);
                    break;
                case ResourceType.Timber:
                    AddTimber(amount);
                    break;
            }
        }

        private void AddGold(int amount)
        {
            ResourceData.AmendGold(amount);
        }

        private void AddTimber(int amount)
        {
            ResourceData.AmendTimber(amount);
        }

        private void AddFood(int amount)
        {
            ResourceData.AmendFood(amount);
        }

    }
}
