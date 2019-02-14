using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace RTS
{
    public class UserInterface : MonoBehaviour
    {
        [SerializeField] GameObject _buttonPrefab;
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

        private void Start()
        {
            ClearUI();
        }

        private void ClearButtonsMenu()
        {
            foreach (Transform child in _buttonsPanel.transform)
            {
                Destroy(child.gameObject);
            }
        }

        //public void LoadUnitActions(UnitAction unitAction)
        //{
        //    ClearButtonsMenu();
        //    foreach (var action in unitAction.Actions)
        //    {
        //        GameObject button = Instantiate(_buttonPrefab, _buttonsPanel.transform);
        //        button.GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(action); });
        //    }
        //}

        public void LoadBuildingMenu()
        {
            ClearButtonsMenu();
            BuildingMenu menu = _buttonsPanel.GetComponent<BuildingMenu>();
            foreach (var building in PlayerManager._availableBuildings)
            {
                GameObject button = Instantiate(_buttonPrefab, _buttonsPanel.transform);
                button.GetComponent<Image>().sprite = building.Icon;
                button.GetComponent<Button>().onClick.AddListener(delegate { menu.ConstructBuilding(building); });
                button.GetComponent<ButtonTooltip>().SetTooltipData(building);
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
