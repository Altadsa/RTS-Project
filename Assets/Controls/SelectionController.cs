using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class SelectionController : MonoBehaviour
    {
        Layer _currentLayer;
        RaycastHit _layerHit;
        GameObject _hitGo;

        public List<GameObject> _selectedUnits { get; private set; }
        [HideInInspector]
        public List<GameObject> _selectableUnits;

        Building _selectedBuilding;

        public delegate void OnUpdateSelectedUnits(List<GameObject> selectedUnits);
        public event OnUpdateSelectedUnits updateSelectedUnits;

        private void Awake()
        {
            _selectedUnits = new List<GameObject>();
            _selectableUnits = new List<GameObject>();
            GetComponent<UnitRaycaster>().UpdateActiveLayer += UpdateActiveLayer;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || _selectedUnits.Count > 0)
            {
                UserInterface.Instance.LoadUnitSelection();
                DeselectBuilding();
            }
            if (!_selectedBuilding && _selectedUnits.Count <= 0) UserInterface.Instance.ClearUI();
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
            PlayerUnit pUnit = unit.GetComponent<PlayerUnit>();
            if (pUnit)
                SelectUnit(pUnit);
        }

        private void SelectUnitHit()
        {
            PlayerUnit iUnit = _hitGo.GetComponent<PlayerUnit>();
            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (!iUnit.isSelected)
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
            if (!Input.GetKey(KeyCode.LeftControl) && !_selectedBuilding)
            {
                DeselectAllUnits();
            }

            _selectableUnits.RemoveAll(unit => unit == null);
            foreach (GameObject iUnit in _selectableUnits)
            {
                Vector3 unitVpos = Camera.main.WorldToViewportPoint(iUnit.transform.position);
                bool isUnitInRect = selectionRect.Contains(unitVpos, true);
                if (isUnitInRect)
                {
                    SelectUnit(iUnit.GetComponent<PlayerUnit>());
                }
            }
        }

        private void SelectUnit(PlayerUnit iUnit)
        {
            _selectedUnits.Add(iUnit.gameObject);
            iUnit.Select();
            UpdateSelectedUnits();
            WorkerActions unitAction = _selectedUnits[0].GetComponent<WorkerActions>();
            if (unitAction)
            UserInterface.Instance.LoadUnitActionMenu(unitAction);
        }

        private void DeselectUnit(PlayerUnit iUnit)
        {
            _selectedUnits.Remove(iUnit.gameObject);
            iUnit.Deselect();
            UpdateSelectedUnits();
        }

        private void DeselectAllUnits()
        {
            if (_selectedBuilding) DeselectBuilding();
            if (_selectedUnits.Count > 0)
            {   
                _selectedUnits.RemoveAll(u => u == null);
                _selectedUnits.ForEach(delegate(GameObject unit) { unit.GetComponent<PlayerUnit>().Deselect(); });
                _selectedUnits.Clear();
                UpdateSelectedUnits();
            }
        }

        private void UpdateSelectedUnits()
        {
            updateSelectedUnits?.Invoke(_selectedUnits);
        }

        private void SelectBuilding()
        {
            DeselectAllUnits();
            Building building = _hitGo.GetComponent<Building>();
            if (!building) return;
            _selectedBuilding = building;
            _selectedBuilding.Select();
        }

        private void DeselectBuilding()
        {
            if (!_selectedBuilding) return;
            _selectedBuilding.Deselect();
            _selectedBuilding = null;
        }

    }
}
