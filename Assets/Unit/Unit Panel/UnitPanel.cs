using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RTS
{
    public class UnitPanel : MonoBehaviour
    {
        public GameObject unitPanelPrefab;
        EventSystem _eventSystem;
        GraphicRaycaster _raycaster;
        PointerEventData _eventData;
        List<GameObject> selectedUnitsPanel = new List<GameObject>();
        SelectionController _selectionController;

        private void Start()
        {
            _selectionController = FindObjectOfType<SelectionController>();
            _selectionController.updateSelectedUnits += UpdatePanel;
            _raycaster = GetComponent<GraphicRaycaster>();
        }

        private void Update()
        {
            SelectUnitFromUi();
        }

        private void SelectUnitFromUi()
        {
            if (!Input.GetMouseButtonUp(0)) return;
            if (selectedUnitsPanel.Count <= 0) return;
            SelectUnitFromRaycastResults(RaycastToUi());
        }

        private void SelectUnitFromRaycastResults(List<RaycastResult> results)
        {
            if (results.Count <= 0) return;
            foreach (Transform child in transform)
            {
                if (results[0].gameObject.transform != child) continue;
                int index = child.GetSiblingIndex();
                _selectionController.SelectUnitFromUI(_selectionController._selectedUnits[index]);
                return;
            }
        }

        private List<RaycastResult> RaycastToUi()
        {
            _eventData = new PointerEventData(_eventSystem);
            _eventData.position = Input.mousePosition;
            var results = new List<RaycastResult>();
            _raycaster.Raycast(_eventData, results);
            return results;
        }

        private void UpdatePanel(List<GameObject> selectedUnits)
        {
            selectedUnitsPanel = GetChildren();
            if (selectedUnitsPanel.Count != selectedUnits.Count)
            {
                CreateNewUnitIcons(selectedUnits);
            }
        }

        private void CreateNewUnitIcons(List<GameObject> selectedUnits)
        {
            DestroyOldIcons();
            foreach (GameObject o in selectedUnits)
            {
                if (!o) return;
                GameObject newIcon = Instantiate(unitPanelPrefab, transform);
                UnitHealth health = o.GetComponent<UnitHealth>();
                UnitPanelHealthBar uHealth = newIcon.GetComponent<UnitPanelHealthBar>();
                if (!health || !uHealth) return;
                uHealth.Initialize(health);
            }
        }

        private void DestroyOldIcons()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        private List<GameObject> GetChildren()
        {
            List<GameObject> children = new List<GameObject>();
            foreach (Transform child in transform)
            {
                children.Add(child.gameObject);
            }

            return children;
        }
    }
}