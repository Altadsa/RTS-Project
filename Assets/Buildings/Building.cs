using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

namespace RTS
{
    [RequireComponent(typeof(BuildingActions))]
    public class Building : MonoBehaviour
    {
        Vector3 spawnPoint;
        float _timeSpentBuilding = 0;
        List<UnitBuildData> _buildQueue = new List<UnitBuildData>();

        BuildingActions _actions;

        public delegate void UpdateBuildingInfo(Building building, float buildPercent);
        public event UpdateBuildingInfo UpdateInfo;

        public List<UnitBuildData> Queue { get { return _buildQueue; } }

        private void Start()
        {
            _actions = GetComponent<BuildingActions>();
        }

        private void Update()
        {
            ProcessQueue();
        }

        private void ProcessQueue()
        {
            if (_buildQueue.Count > 0)
            {
                _timeSpentBuilding += Time.deltaTime;
                UnitBuildData data = _buildQueue[0];
                UpdateBuildInfo(data);
                if (_timeSpentBuilding >= data.BuildTime)
                {
                    transform.GetChild(0).transform.position = spawnPoint;
                    GameObject unit = Instantiate(data.Unit, transform.GetChild(0));
                    _buildQueue.Remove(data);
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

        public void AddToQueue(UnitBuildData data)
        {
            _buildQueue.Add(data);
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
            SetupBuildingInfo();
            SetupButtons();
        }

        private void SetupButtons()
        {
            UserInterface.Instance.LoadMenuButtons(_actions.CreateUnitButtons());
        }

        private void SetupBuildingInfo()
        {
            UserInterface.Instance.LoadBuildingSelection();
            BuildingInfo.Instance.LoadBuildingInfo(this);
        }

        private void SetDefaultSpawnPoint()
        {
            spawnPoint = (transform.forward * 5) + transform.position;
        }

    }
}
