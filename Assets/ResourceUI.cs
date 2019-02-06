using UnityEngine;
using TMPro;

namespace RTS
{
    public class ResourceUI : MonoBehaviour
    {
        [SerializeField] TMP_Text _resourceText;
        Resource _resource;

        private void Start()
        {
            _resource = GetComponentInParent<Resource>();
            _resource.updateResources += UpdateResource;
        }

        private void UpdateResource(int currentResources)
        {
            _resourceText.text = string.Format("{0}: {1}", _resource.name, currentResources);
        }
    } 
}
