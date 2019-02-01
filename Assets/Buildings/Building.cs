using UnityEngine;
using UnityEngine.UI;
using GEV;
using UnityEngine.Serialization;

namespace RTS
{
    public class Building : MonoBehaviour
    {
        public GameObject buildButton;
        public UnitBuildData[] unitPrefabs;
        GameObject spawnPoint;

        [FormerlySerializedAs("onBuildingInfoLoaded")] [SerializeField] private ScriptableEvent onBuildingSelected;
        [SerializeField] private ScriptableEvent onBuildingDeselected;
        
        public void Select()
        {
            GetComponentInChildren<Projector>().enabled = true;
            onBuildingSelected.Raise();   
            LoadBuildingData();
        }

        public void Deselect()
        {
            GetComponentInChildren<Projector>().enabled = false;
            onBuildingDeselected.Raise();
        }

        public void LoadBuildingData()
        {
            GameObject buildingWindow = GameObject.FindGameObjectWithTag("Units Menu");

            if (buildingWindow)
            {
                UserInterface.ClearBuildMenu();
                SetupButtons(buildingWindow);
            }
        }

        private void SetupButtons(GameObject windowToSetup)
        {
            foreach (UnitBuildData unitInstance in unitPrefabs)
            {
                GameObject buttonInstance = Instantiate(buildButton, windowToSetup.GetComponent<RectTransform>());
                buttonInstance.GetComponentInChildren<Text>().text = unitInstance.name;
                buttonInstance.GetComponent<Button>().onClick.AddListener(delegate { SpawnUnit(unitInstance); });
            }
        }

        private void SpawnUnit(UnitBuildData unitInstance)
        {
            bool canAfford = !(unitInstance.GoldCost > ResourceData.Gold);
            if (unitInstance.TimberCost > ResourceData.Timber) canAfford = false;
            if (unitInstance.FoodCost > ResourceData.Food) canAfford = false;
            if (canAfford)
            {
                transform.GetChild(0).transform.position = (transform.forward * 5) + transform.position;
                GameObject unit = Instantiate(unitInstance.Unit, transform.GetChild(0));
                ResourceData.AmendGold(-unitInstance.GoldCost);
                ResourceData.AmendFood(-unitInstance.FoodCost);
                ResourceData.AmendTimber(-unitInstance.TimberCost);
            }
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
