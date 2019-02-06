using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
            SelectUnitFromUI();
        }

        private void SelectUnitFromUI()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (selectedUnitsPanel.Count <= 0) return;
                _eventData = new PointerEventData(_eventSystem);
                _eventData.position = Input.mousePosition;
                List<RaycastResult> results = new List<RaycastResult>();
                _raycaster.Raycast(_eventData, results);
                foreach (Transform transform in transform)
                {
                    if (results[0].gameObject.transform == transform)
                    {
                        int index = transform.GetSiblingIndex();
                        _selectionController.SelectUnitFromUI(_selectionController._selectedUnits[index]);
                        return;
                    }
                }
            }
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
