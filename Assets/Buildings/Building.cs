using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

namespace RTS
{
    [RequireComponent(typeof(BuildingActions))]
    public class Building : MonoBehaviour
    {
        public Vector3 spawnPoint;
        float _timeSpentBuilding = 0;
        List<IQueueable> _buildQueue = new List<IQueueable>();

        BuildingActions _actions;

        public delegate void UpdateBuildingInfo(Building building, float buildPercent);
        public event UpdateBuildingInfo UpdateInfo;

        public List<IQueueable> Queue { get { return _buildQueue; } }

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
                IQueueable data = _buildQueue[0];
                float timeNeeded = data.TimeNeeded();
                UpdateBuildInfo(timeNeeded);
                if (_timeSpentBuilding >= timeNeeded)
                {
                    data.OnComplete(this);
                    var upgrade = data as UpgradeData;
                    if (upgrade) UpgradeManager.CompleteUpgrade(upgrade);

                    _buildQueue.Remove(data);
                    if (_buildQueue.Count <= 0) UpdateInfo(this, 0);
                    _timeSpentBuilding = 0;
                }
            }
        }

        private void UpdateBuildInfo(float data)
        {
            if (UpdateInfo == null) return;
            float progress = _timeSpentBuilding / data;
            UpdateInfo(this, progress);
        }

        public void AddToQueue(IQueueable data)
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
