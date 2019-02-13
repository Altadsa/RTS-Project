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
        public List<IQueueable> Queue { get; private set; } = new List<IQueueable>();

        BuildingActions _actions;

        public delegate void UpdateBuildingInfo(Building building, float buildPercent);
        public event UpdateBuildingInfo UpdateInfo;

        private void Start()
        {
            _actions = GetComponent<BuildingActions>();
            SetDefaultSpawnPoint();
        }

        private void Update()
        {
            ProcessQueue();
        }

        private void ProcessQueue()
        {
            if (Queue.Count > 0)
            {
                _timeSpentBuilding += Time.deltaTime;
                IQueueable firstItem = Queue[0];
                float timeNeeded = firstItem.Time;
                UpdateBuildInfo(timeNeeded);
                if (_timeSpentBuilding >= timeNeeded)
                {
                    firstItem.OnProductionComplete(this);
                    Queue.Remove(firstItem);
                    if (Queue.Count <= 0) UpdateInfo(this, 0);
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
            Queue.Add(data);
        }

        public void RemoveFromQueue(IQueueable itemInQueue)
        {
            Queue.Remove(itemInQueue);
            if (Queue.Count <= 0) { UpdateInfo(this, 0); return; }
            var firstItem = Queue[0];
            if (itemInQueue != firstItem)
            {
                _timeSpentBuilding = 0;
                UpdateBuildInfo(firstItem.Time);
            }
        }

        public void Select()
        {
            GetComponentInChildren<Projector>().enabled = true;
            LoadBuildingInterface();
        }

        public void Deselect()
        {
            GetComponentInChildren<Projector>().enabled = false;
            BuildingInfo.Instance.ClearBuildingInfo(this);
            UserInterface.Instance.ClearUI();
        }

        public void LoadBuildingInterface()
        {
            SetupBuildingInfo();
            SetupButtons();
            if (Queue.Count > 0)
                UpdateBuildInfo(Queue[0].Time);
            else
                UpdateBuildInfo(0);
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
