using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RTS
{
    [RequireComponent(typeof(ActionRelay))]
    [RequireComponent(typeof(SelectionController))]
    public class UnitRaycaster : MonoBehaviour
    {

        readonly Layer[] validLayers =
        {
            Layer.Ui,
            Layer.Units,
            Layer.Interactable,
            Layer.Attackables,
            Layer.Walkable
        };
        
        Ray mousePos;
        RaycastHit hit;

        public delegate void OnLayerUpdated(Layer newLayer, RaycastHit layerHit);
        public event OnLayerUpdated UpdateActiveLayer;

        private void Update()
        {
            RaycastForSelection();
        }

        private void RaycastForSelection()
        {
            foreach (Layer layer in validLayers)
            {
                int layerMask = 1 << (int)layer;
                Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                bool hasHit = Physics.Raycast(mouseRay, out hit, Mathf.Infinity, layerMask);
                if (hasHit && UpdateActiveLayer != null)
                {
                    UpdateActiveLayer(layer, hit);
                    return;
                }
            }
        }



    }
}
