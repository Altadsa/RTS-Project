using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    [RequireComponent(typeof(UnitActionController))]
    [RequireComponent(typeof(UnitSelectionController))]
    public class UnitController : MonoBehaviour
    {
        readonly Layer[] selectableLayers =
        {
            Layer.Walkable,
            Layer.Units,
            Layer.Interactable,
            Layer.Attackables
        };

        Ray mousePos;
        RaycastHit hit;
        Vector3 mousePos1, mousePos2;

        UnitSelectionController selectionController;
        UnitActionController actionController;

        public delegate void OnLayerUpdated(Layer newLayer, RaycastHit layerHit);
        public event OnLayerUpdated updateLayer;

        private void Awake()
        {
            selectionController = GetComponent<UnitSelectionController>();
            actionController = GetComponent<UnitActionController>();
        }

        private void Update()
        {
            RaycastForSelection();
            SelectUnits();
        }

        private void SelectUnits()
        {
            if (Input.GetMouseButtonDown(0))
            {
                mousePos1 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                selectionController.SelectionState();
            }
            if (Input.GetMouseButtonUp(0))
            {
                mousePos2 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                if (mousePos1 != mousePos2)
                {
                    SelectUnitsInBox();
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                selectionController.AssignAction();
            }
        }

        private void RaycastForSelection()
        {
            foreach (Layer layer in selectableLayers)
            {
                int layerMask = 1 << (int)layer;
                Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                bool hasHit = Physics.Raycast(mouseRay, out hit, Mathf.Infinity, layerMask);
                if (hasHit && updateLayer != null)
                {
                    updateLayer(layer, hit);
                }
            }
        }

        private void SelectUnitsInBox()
        {
            selectionController.SelectUnitsInBox(DrawRect());
        }

        private Rect DrawRect()
        {
            float width = mousePos2.x - mousePos1.x;
            float height = mousePos2.y - mousePos1.y;
            return new Rect(mousePos1.x, mousePos1.y, width, height);
        }

    }
}
