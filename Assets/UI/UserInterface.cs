using UnityEngine;
using System.Collections.Generic;

namespace RTS
{
    public class UserInterface : MonoBehaviour
    {
        [SerializeField] GameObject _buttonsPanel;
        [SerializeField] GameObject _buildingSelection;
        [SerializeField] GameObject _unitSelection;

        private static UserInterface _instance;
        private static readonly object padlock = new object();
        public static UserInterface Instance
        {
            get
            {
                lock(padlock)
                {
                    if (!_instance) { _instance = FindObjectOfType<UserInterface>();  }
                    return _instance;
                }
            }
        }

        private void ClearButtonsMenu()
        {
            foreach (Transform child in _buttonsPanel.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void LoadMenuButtons(List<GameObject> buttons)
        {
            ClearButtonsMenu();
            foreach (GameObject button in buttons)
            {
                button.transform.SetParent(_buttonsPanel.transform);
                button.transform.localScale = Vector3.one;
            }
        }

        public void LoadBuildingSelection()
        {
            _buildingSelection.SetActive(true);
            _unitSelection.SetActive(false);
        }

        public void LoadUnitSelection()
        {
            _unitSelection.SetActive(true);
            _buildingSelection.SetActive(false);
        }

        public void ClearUI()
        {
            _unitSelection.SetActive(false);
            _buildingSelection.SetActive(false);
            ClearButtonsMenu();
        }
    } 
}
