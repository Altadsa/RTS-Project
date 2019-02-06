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

        public delegate void UpdateBuildingInfo(Building building, float buildPercent);
        public event UpdateBuildingInfo UpdateInfo;

        public Queue<UnitBuildData> Queue { get { return _buildQueue; } }

        private void Update()
        {
            if (_buildQueue.Count > 0)
            {
                _timeSpentBuilding += Time.deltaTime;
                UnitBuildData data = _buildQueue.Peek();
                UpdateBuildInfo(data);
                if (_timeSpentBuilding >= data.BuildTime)
                {
                    transform.GetChild(0).transform.position = (transform.forward * 5) + transform.position;
                    GameObject unit = Instantiate(data.Unit, transform.GetChild(0));
                    _buildQueue.Dequeue();
                    if (_buildQueue.Count <= 0) UpdateInfo(this, 0);
                    _timeSpentBuilding = 0;

                }
            }

        }

        private void UpdateBuildInfo(UnitBuildData data)
        {
            if (UpdateInfo == null) return;
            float progress = _timeSpentBuilding / data.BuildTime;
            UpdateInfo(this, progress);
        }

        public void Select()
        {
            GetComponentInChildren<Projector>().enabled = true;
            LoadBuildingInterface();
        }

        public void Deselect()
        {
            GetComponentInChildren<Projector>().enabled = false;
        }

        public void LoadBuildingInterface()
        {
            SetupButtons();
            SetupBuildingInfo();
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
            UserInterface.Instance.LoadMenuButtons(buttons);
        }

        private void SetupBuildingInfo()
        {
            UserInterface.Instance.LoadBuildingSelection();
            BuildingInfo.Instance.LoadBuildingInfo(this);
        }

        private void BuildUnit(UnitBuildData unitInstance)
        {
            if (_buildQueue.Count >= 5) return;
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
