using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class MineInfo : MonoBehaviour
    {
        [SerializeField] GameObject _uiCanvas;
        ResourceUI _ui;
        Resource _resource;


        private void Start()
        {
            _resource = GetComponent<Resource>();
            _ui = _uiCanvas.GetComponent<ResourceUI>();
            _resource.updateResources += UpdateResources;
            _uiCanvas.SetActive(false);
        }

        private void OnMouseOver()
        {
            _uiCanvas.SetActive(true);
        }

        private void OnMouseExit()
        {
            _uiCanvas.SetActive(false);
        }

        private void UpdateResources(int amount)
        {
            _ui.UpdateResource(amount);
        }
    } 
}
