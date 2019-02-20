using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class PlayerSelectionController : SelectionController
    {
        Layer _currentLayer;
        RaycastHit _layerHit;
        GameObject _hitGo;
        Building _selectedBuilding;
       private void Awake()
        {
            GetComponent<UnitRaycaster>().UpdateActiveLayer += UpdateActiveLayer;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || SelectedUnits.Count > 0)
            {
                UserInterface.Instance.LoadUnitSelection();
                DeselectBuilding();
            }
            if (!_selectedBuilding && SelectedUnits.Count <= 0) UserInterface.Instance.ClearUI();
        }

        private void UpdateActiveLayer(Layer newLayer, RaycastHit _layerHit)
        {
            _currentLayer = newLayer;
            this._layerHit = _layerHit;
            _hitGo = this._layerHit.collider.gameObject;
        }

        public void SelectionState()
        {
            if (UiRaycast.IsRaycastingToUi()) return;
            switch (_currentLayer)
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

        public void SelectUnitFromUI(GameObject unit)
        {
            DeselectAllUnits();
            Unit pUnit = unit.GetComponent<Unit>();
            if (pUnit)
                SelectUnit(pUnit);
        }

        private void SelectUnitHit()
        {
            Unit iUnit = _hitGo.GetComponentInParent<Unit>();
            if (!IsSelectable()) return;
            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (!iUnit._isSelected)
                    SelectUnit(iUnit);
                else
                    DeselectUnit(iUnit);
            }
            else
            {
                DeselectAllUnits();
                if (iUnit)
                    SelectUnit(iUnit);
            }
        }

        public void SelectUnitsInBox(Rect selectionRect)
        {
            if (UiRaycast.IsRaycastingToUi()) return;
            DeselectBuilding();
            if (!Input.GetKey(KeyCode.LeftControl) && !_selectedBuilding)
            {
                DeselectAllUnits();
            }

            SelectableUnits.RemoveAll(unit => unit == null);
            foreach (GameObject iUnit in SelectableUnits)
            {
                Vector3 unitVpos = Camera.main.WorldToViewportPoint(iUnit.transform.position);
                bool isUnitInRect = selectionRect.Contains(unitVpos, true);
                if (isUnitInRect)
                {
                    SelectUnit(iUnit.GetComponent<Unit>());
                    UpdateSelectedUnits();
                }
            }
        }

        private void SelectUnit(Unit iUnit)
        {
            SelectedUnits.Add(iUnit.gameObject);
            iUnit.Select();
            UpdateSelectedUnits();
            BuildAction buildAction = SelectedUnits[0].GetComponent<BuildAction>();
            if (buildAction)
                UserInterface.Instance.LoadBuildingMenu();
        }

        private void DeselectUnit(Unit iUnit)
        {
            SelectedUnits.Remove(iUnit.gameObject);
            iUnit.Deselect();
            UpdateSelectedUnits();
        }

        private void DeselectAllUnits()
        {
            if (_selectedBuilding) DeselectBuilding();
            if (SelectedUnits.Count > 0)
            {
                SelectedUnits.RemoveAll(u => u == null);
                SelectedUnits.ForEach(delegate (GameObject unit) { unit.GetComponent<Unit>().Deselect(); });
                SelectedUnits.Clear();
                UpdateSelectedUnits();
            }
        }



        private void SelectBuilding()
        {
            DeselectAllUnits();
            Building building = _hitGo.GetComponentInParent<Building>();
            if (!building || !IsSelectable()) return;
            _selectedBuilding = building;
            _selectedBuilding.Select();
        }

        private void DeselectBuilding()
        {
            if (!_selectedBuilding) return;
            _selectedBuilding.Deselect();
            _selectedBuilding = null;
        }

        bool IsSelectable()
        {
            GameObject unitParent = _hitGo.transform.parent.gameObject;
            bool isSelectableUnit = SelectableUnits.Contains(unitParent);
            bool isSelectableBuilding = SelectableBuildings.Contains(unitParent);
            if (isSelectableBuilding || isSelectableUnit)
            {
                return true;
            }
            return false;
        }

    } 
}
