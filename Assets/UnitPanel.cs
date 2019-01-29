using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class UnitPanel : MonoBehaviour
    {
        public GameObject unitPanelPrefab;
        List<GameObject> selectedUnitsPanel = new List<GameObject>();

        private void Start()
        {
            FindObjectOfType<UnitSelectionController>().updateSelectedUnits += UpdatePanel;
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
