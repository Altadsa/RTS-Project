using UnityEngine;

namespace RTS
{
    public class LumberMill : MonoBehaviour, IDropoff
    {
        [SerializeField] private Transform _resourceDropOff;

        public Vector3 DropPoint => _resourceDropOff.position;

        public void DropResources(ResourceType type, int amount)
        {
            switch (type)
            {
                default: ResourceData.AmendTimber(amount);
                    return;
            }
        }

    } 
}
