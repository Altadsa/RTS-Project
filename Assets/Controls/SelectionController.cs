using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class SelectionController : MonoBehaviour
    {
        Layer _currentLayer;
        RaycastHit _layerHit;
        GameObject _hitGo;

        List<GameObject> _selectedUnits;
        [HideInInspector]
        public List<GameObject> selectableUnits;

        Building _selectedBuilding;

        public delegate void OnUpdateSelectedUnits(List<GameObject> selectedUnits);
        public event OnUpdateSelectedUnits updateSelectedUnits;

        private void Awake()
        {
            _selectedUnits = new List<GameObject>();
            selectableUnits = new List<GameObject>();
            GetComponent<UnitRaycaster>().UpdateActiveLayer += UpdateActiveLayer;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || _selectedUnits.Count > 0)
            {
                DeselectBuilding();
            }
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
                case Layer.Ui:
                    //Do nothing
                    break;
                default:
                    return;
            }
        }

        private void SelectUnitHit()
        {
            PlayerUnit iUnit = _hitGo.GetComponent<PlayerUnit>();
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
            if (UiRaycast.IsRaycastingToUi()) return;
            List<GameObject> removableUnits = new List<GameObject>();
            if (!Input.GetKey(KeyCode.LeftControl) && !_selectedBuilding)
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

                if (removableUnits.Count <= 0) continue;
                foreach (GameObject removableUnit in removableUnits)
                {
                    selectableUnits.Remove(removableUnit);
                }
                removableUnits.Clear();
            }
        }

        private void SelectUnit(PlayerUnit iUnit)
        {
            _selectedUnits.Add(iUnit.gameObject);
            iUnit.Select();
            UpdateSelectedUnits();
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
            List<GameObject> deadUnits = new List<GameObject>();
            if (_selectedUnits.Count > 0)
            {
                foreach (GameObject iUnit in _selectedUnits)
                {
                    if (!iUnit) deadUnits.Add(iUnit);
                    else
                    {
                        PlayerUnit instance = iUnit.GetComponent<PlayerUnit>();
                        instance.Deselect();
                    }
                }
                foreach (var deadUnit in deadUnits)
                {
                    _selectedUnits.Remove(deadUnit);
                }
                deadUnits.Clear();
                _selectedUnits.Clear();
                UpdateSelectedUnits();
            }
        }

        private void UpdateSelectedUnits()
        {
            if (updateSelectedUnits != null)
                updateSelectedUnits(_selectedUnits);
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
