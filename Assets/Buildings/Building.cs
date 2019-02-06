using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using GEV;
using UnityEngine.Serialization;

namespace RTS
{
    public class Building : MonoBehaviour
    {
        public GameObject buildButton;
        public UnitBuildData[] unitPrefabs;
        GameObject spawnPoint;
        float _timeSpentBuilding = 0;
        Queue<UnitBuildData> _buildQueue = new Queue<UnitBuildData>(5);

        [FormerlySerializedAs("onBuildingInfoLoaded")] [SerializeField] private ScriptableEvent onBuildingSelected;
        [SerializeField] private ScriptableEvent onBuildingDeselected;

        public delegate void OnBuildQueueUpdate(float percentage);
        public event OnBuildQueueUpdate UpdateBuildQueue;

        private void Update()
        {
            if (_buildQueue.Count > 0)
            {
                _timeSpentBuilding += Time.deltaTime;
                if (_timeSpentBuilding >= _buildQueue.Peek().BuildTime)
                {
                    Debug.Log(_buildQueue.Peek().BuildTime);
                    transform.GetChild(0).transform.position = (transform.forward * 5) + transform.position;
                    GameObject unit = Instantiate(_buildQueue.Peek().Unit, transform.GetChild(0));
                    _buildQueue.Dequeue();
                    _timeSpentBuilding = 0;
                }
            }
        }

        public void Select()
        {
            GetComponentInChildren<Projector>().enabled = true;
            onBuildingSelected.Raise();   
            LoadBuildingInterface();
        }

        public void Deselect()
        {
            GetComponentInChildren<Projector>().enabled = false;
            onBuildingDeselected.Raise();
        }

        public void LoadBuildingInterface()
        {
            SetupButtons();
        }

        private void SetupButtons()
        {
            List<GameObject> buttons = new List<GameObject>();
            foreach (UnitBuildData unitInstance in unitPrefabs)
            {
                GameObject buttonInstance = Instantiate(buildButton);
                buttonInstance.GetComponentInChildren<Text>().text = unitInstance.name;
                buttonInstance.GetComponent<Button>().onClick.AddListener(delegate { BuildUnit(unitInstance); });
                buttons.Add(buttonInstance);
            }
            UserInterface.LoadMenuButtons(buttons);
        }

        private void SetupBuildingInfo(GameObject infoWindow)
        {

        }

        private void BuildUnit(UnitBuildData unitInstance)
        { 
            if (CanBuyUnit(unitInstance))
            {
                _buildQueue.Enqueue(unitInstance);
                ResourceData.AmendGold(-unitInstance.GoldCost);
                ResourceData.AmendFood(-unitInstance.FoodCost);
                ResourceData.AmendTimber(-unitInstance.TimberCost);
            }
        }

        private bool CanBuyUnit(UnitBuildData data)
        {
            bool canAfford = !(data.GoldCost > ResourceData.Gold);
            if (data.TimberCost > ResourceData.Timber) canAfford = false;
            if (data.FoodCost > ResourceData.Food) canAfford = false;
            return canAfford;
        }

    } 
}
