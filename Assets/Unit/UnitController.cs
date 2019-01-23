using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    [RequireComponent(typeof(ActionController))]
    [RequireComponent(typeof(SelectionController))]
    public class UnitController : MonoBehaviour
    {

        Layer[] selectableLayers =
        {
            Layer.Units,
            Layer.Interactable,
        };

        Layer[] actionLayers =
        {
            Layer.Attackables,
            Layer.Walkable
        };

        List<GameObject> selectedUnits;
        [HideInInspector]
        public List<GameObject> selectableUnits;

        Ray mousePos;
        Vector3 mousePos1, mousePos2;

        RaycastHit hit;
        ActionController actionController;
        SelectionController selectionController;

        public delegate void OnTargetAction(GameObject target);
        public event OnTargetAction onAttackTargetFound;

        public delegate void OnMoveAction(Vector3 mousePosition);
        public event OnMoveAction onRightMbClicked;

        public delegate void OnUpdateSelectedUnits(List<GameObject> selectedUnits);
        public event OnUpdateSelectedUnits updateSelectedUnits;

        private void Awake()
        {
            selectedUnits = new List<GameObject>();
            selectableUnits = new List<GameObject>();
        }

        private void Update()
        {
            HandleMouseInput();
        }

        private void HandleMouseInput()
        {
            Select();
            SelectAction();
        }

        private void Select()
        {
            if (Input.GetMouseButtonDown(0))
            {
                mousePos1 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                RaycastForSelection(selectableLayers);
            }
            if (Input.GetMouseButtonUp(0))
            {
                mousePos2 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                if (mousePos1 != mousePos2)
                {
                    SelectUnitsInBox();
                }
            }

        }

        private void RaycastForSelection(Layer[] layersToSelect)
        {
            foreach (Layer layer in layersToSelect)
            {
                int layerMask = 1 << (int)layer;
                Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                bool hasHit = Physics.Raycast(mouseRay, out hit, Mathf.Infinity, layerMask);
                if (hasHit)
                {
                    SelectLayers(layer);
                    return;
                }
            }
        }

        private void SelectLayers(Layer layerToSelect)
        {
            switch (layerToSelect)
            {
                case Layer.Units:
                    SelectUnitHit();
                    break;
                case Layer.Interactable:
                    DeselectAllUnits();
                    SelectBuilding();
                    break;
                case Layer.Attackables:
                    SetTargetForSelectedUnits();
                    break;
                case Layer.Walkable:
                    MoveUnits();
                    break;
            }
        }

        private void SelectUnitHit()
        {
            PlayerUnit iUnit = hit.collider.GetComponent<PlayerUnit>();
            if (Input.GetKey(KeyCode.LeftControl) && iUnit)
            {
                if (!iUnit.isSelected)
                {
                    SelectUnit(iUnit);
                }
                else
                {
                    DeselectUnit(iUnit);
                }
            }
            else
            {
                DeselectAllUnits();
                if (iUnit)
                {
                    SelectUnit(iUnit);
                }
            }
        }

        private void SelectUnitsInBox()
        {
            List<GameObject> removableUnits = new List<GameObject>();
            if (!Input.GetKey(KeyCode.LeftControl))
            {
                DeselectAllUnits();
            }

            float width = mousePos2.x - mousePos1.x;
            float height = mousePos2.y - mousePos1.y;
            Rect selectionRect = new Rect(mousePos1.x, mousePos1.y, width, height);

            foreach (GameObject iUnit in selectableUnits)
            {
                if (iUnit != null)
                {
                    if (selectionRect.Contains(Camera.main.WorldToViewportPoint(iUnit.transform.position), true))
                    {
                        SelectUnit(iUnit.GetComponent<PlayerUnit>());
                    }
                }
                else
                {
                    removableUnits.Add(iUnit);
                }

                if (removableUnits.Count > 0)
                {
                    foreach (GameObject removableUnit in removableUnits)
                    {
                        selectableUnits.Remove(removableUnit);
                    }
                    removableUnits.Clear();
                }
            }
        }

        private void SelectUnit(PlayerUnit iUnit)
        {
            selectedUnits.Add(iUnit.gameObject);
            iUnit.isSelected = true;
            onRightMbClicked += iUnit.OnRightMbClicked;
            onAttackTargetFound += iUnit.Target;
            updateSelectedUnits(selectedUnits);
        }

        private void DeselectUnit(PlayerUnit iUnit)
        {
            selectedUnits.Remove(iUnit.gameObject);
            iUnit.isSelected = false;
            onRightMbClicked -= iUnit.OnRightMbClicked;
            onAttackTargetFound += iUnit.Target;
            updateSelectedUnits(selectedUnits);
        }

        private void DeselectAllUnits()
        {
            if (selectedUnits.Count > 0)
            {
                foreach (GameObject iUnit in selectedUnits)
                {
                    onRightMbClicked -= iUnit.GetComponent<PlayerUnit>().OnRightMbClicked;
                    onAttackTargetFound += iUnit.GetComponent<PlayerUnit>().Target;
                    iUnit.GetComponent<PlayerUnit>().isSelected = false;
                }
                selectedUnits.Clear();
                updateSelectedUnits(selectedUnits);
            }
        }

        private void SelectBuilding()
        {
            Building building = hit.collider.GetComponent<Building>();
            building.LoadBuildingData();
        }

        public void SelectAction()
        {
            if (selectedUnits.Count <= 0) return;
            if (Input.GetMouseButtonDown(1))
            {
                RaycastForSelection(actionLayers);
            }
        }

        private void MoveUnits()
        {
            if (onRightMbClicked == null) { return; }
            onRightMbClicked(hit.point);
            onAttackTargetFound(null);
        }

        private void SetTargetForSelectedUnits()
        {
            if (onAttackTargetFound == null) { return; }
            foreach (GameObject iUnit in selectedUnits)
            {
                if (iUnit)
                {
                    iUnit.GetComponent<PlayerUnit>().Target(hit.collider.gameObject);
                }

            }
        }
    }
}
