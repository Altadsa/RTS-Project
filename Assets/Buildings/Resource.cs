using UnityEngine;

namespace RTS
{
    public class Resource : MonoBehaviour
    {
        [SerializeField] ResourceType _resourceType;
        [SerializeField] int _resourcesLeft = 1500;
        [SerializeField] float _loadWeight = 1;
        [SerializeField] float _timeToWork = 2;
        Canvas _canvas;

        private void LateUpdate()
        {
            if (_canvas && _canvas.isActiveAndEnabled)
                _canvas.transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        }

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