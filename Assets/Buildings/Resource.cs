using UnityEngine;

namespace RTS
{
    public class Resource : MonoBehaviour
    {
        [SerializeField] ResourceType _resourceType;
        [SerializeField] int _resourcesLeft = 1500;
        [SerializeField] float _loadWeight = 1;
        [SerializeField] float _timeToWork = 2;

        public delegate void OnResourceChanged(int currentResouces);
        public event OnResourceChanged updateResources;

        public float WorkTime() { return _timeToWork; }

        public float Gather()
        {
            DestroyDepletedResource();
            return _loadWeight;
        }

        public ResourceType ResourceType() { return _resourceType; }

        private void DestroyDepletedResource()
        {
            if (_resourcesLeft <= 0) Destroy(gameObject);
            _resourcesLeft--;
        }
    }
}