using UnityEngine;
using UnityEngine.UI;

namespace RTS
{
    public class Building : MonoBehaviour
    {
        public GameObject buildButton;
        public GameObject[] unitPrefabs;
        GameObject spawnPoint;

        public void Select()
        {
            GetComponentInChildren<Projector>().enabled = true;
            LoadBuildingData();
        }

        public void Deselect()
        {
            GetComponentInChildren<Projector>().enabled = false;
            UserInterface.ClearBuildMenu();
        }

        public void LoadBuildingData()
        {
            GameObject buildingWindow = GameObject.FindGameObjectWithTag("Build Panel");

            if (buildingWindow)
            {
                UserInterface.ClearBuildMenu();
                SetupButtons(buildingWindow);
            }
        }

        private void SetupButtons(GameObject windowToSetup)
        {
            foreach (GameObject unitInstance in unitPrefabs)
            {
                GameObject buttonInstance = Instantiate(buildButton, windowToSetup.GetComponent<RectTransform>());
                buttonInstance.GetComponentInChildren<Text>().text = unitInstance.name;
                buttonInstance.GetComponent<Button>().onClick.AddListener(delegate { SpawnUnit(unitInstance); });
            }
        }

        private void SpawnUnit(GameObject unitInstance)
        {
            transform.GetChild(0).transform.position = (transform.forward * 5) + transform.position;
            GameObject unit = Instantiate(unitInstance, transform.GetChild(0));
        }

        private void OnDrawGizmos()
        {
            if (spawnPoint)
            {
                Gizmos.DrawSphere(spawnPoint.transform.position, 2);
            }
        }

    } 
}
