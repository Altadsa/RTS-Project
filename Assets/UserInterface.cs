using UnityEngine;
using System.Collections.Generic;

namespace RTS
{
    public class UserInterface : MonoBehaviour
    {
        [SerializeField] static GameObject _buttonsPanel;
        [SerializeField] static GameObject _selectionPanel;

        private void Start()
        {
            _buttonsPanel = GameObject.FindGameObjectWithTag("Buttons Panel");
        }

        public static void ClearButtonsMenu()
        {
            foreach (Transform child in _buttonsPanel.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public static void LoadMenuButtons(List<GameObject> buttons)
        {
            ClearButtonsMenu();
            foreach (GameObject button in buttons)
            {
                button.transform.SetParent(_buttonsPanel.transform);
            }
        }
    } 
}
