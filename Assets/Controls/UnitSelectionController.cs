using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class UnitSelectionController : MonoBehaviour
    {
        Layer currentLayer;
        RaycastHit layerHit;
        GameObject hitGO;

        List<GameObject> selectedUnits;
        [HideInInspector]
        public List<GameObject> selectableUnits;

        public delegate void OnUpdateSelectedUnits(List<GameObject> selectedUnits);
        public event OnUpdateSelectedUnits updateSelectedUnits;

        public delegate void UnitAction(RaycastHit targetHit);
        public event UnitAction assignAction;

        private void Awake()
        {
            selectedUnits = new List<GameObject>();
            selectableUnits = new List<GameObject>();
            GetComponent<UnitController>().updateLayer += UpdateLayer;
        }

        private void UpdateLayer(Layer newLayer, RaycastHit _layerHit)
        {
            currentLayer = newLayer;
            layerHit = _layerHit;
            hitGO = layerHit.collider.gameObject;
        }

        public void SelectionState()
        { 
            switch (currentLayer)
            {
                case Layer.Units:
                    SelectUnitHit();
                    break;
                case Layer.Interactable:
                    SelectBuilding();
                    break;
                case Layer.Walkable:
                    DeselectAllUnits();
                    break;
                default:
                    return;
            }
        }

        private void SelectUnitHit()
        {
            PlayerUnit iUnit = hitGO.GetComponent<PlayerUnit>();
            if (Input.GetKey(KeyCode.LeftControl))
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

        public void SelectUnitsInBox(Rect selectionRect)
        {
            List<GameObject> removableUnits = new List<GameObject>();
            if (!Input.GetKey(KeyCode.LeftControl))
            {
                DeselectAllUnits();
            }

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
            iUnit.Select();
            updateSelectedUnits(selectedUnits);
        }

        private void DeselectUnit(PlayerUnit iUnit)
        {
            selectedUnits.Remove(iUnit.gameObject);
            iUnit.Deselect();
            updateSelectedUnits(selectedUnits);
        }

        private void DeselectAllUnits()
        {
            if (selectedUnits.Count > 0)
            {
                foreach (GameObject iUnit in selectedUnits)
                {
                    PlayerUnit instance = iUnit.GetComponent<PlayerUnit>();
                    instance.Deselect();
                }
                selectedUnits.Clear();
                updateSelectedUnits(selectedUnits);
            }
        }

        private void SelectBuilding()
        {
            DeselectAllUnits();
            Building building = hitGO.GetComponent<Building>();
            building.LoadBuildingData();
        }

        public void AssignAction()
        {
            if (assignAction == null) { return; }
            assignAction(layerHit);
        }
    } 
}
