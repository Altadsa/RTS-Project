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
            FindObjectOfType<UnitController>().updateSelectedUnits += UpdatePanel;
            selectedUnitsPanel = GetChildren();
        }

        private void UpdatePanel(List<GameObject> selectedUnits)
        {
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
                Instantiate(unitPanelPrefab, transform);
            }
        }

        private void DestroyOldIcons()
        {
            foreach (Transform t in transform)
            {
                Destroy(t.gameObject);
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
