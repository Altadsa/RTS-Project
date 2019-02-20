using UnityEngine;
using TMPro;

namespace RTS
{
    public class ResourceUI : MonoBehaviour
    {
        [SerializeField] TMP_Text _resourceText;
        Canvas _canvas;
        Transform _mCamTransform;
        Resource _resource;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _canvas.worldCamera = Camera.main;
            _mCamTransform = Camera.main.transform;
            _resource = GetComponentInParent<Resource>();
        }

        private void LateUpdate()
        {
            transform.LookAt(2 * transform.position - _mCamTransform.position);
        }

        public void UpdateResource(int currentResources)
        {
            _resourceText.text = string.Format("{0}:\n{1}", _resource.name, currentResources);
        }
    } 
}
