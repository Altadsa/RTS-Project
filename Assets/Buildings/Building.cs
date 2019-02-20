using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

namespace RTS
{
    [RequireComponent(typeof(BuildingActions))]
    public class Building : MonoBehaviour
    {
        public Vector3 SpawnPoint;
        public List<IQueueable> Queue { get; private set; } = new List<IQueueable>();

        float _timeSpentBuilding = 0;
        BuildingActions _actions;
        PlayerInformation _player;
        SelectionController _selectionController;

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

        public void SetPlayer(PlayerInformation player)
        {
            if (_player == null)
            {
                _player = player;
                _selectionController = FindObjectsOfType<SelectionController>()
                                        .Where(s => s.Player == _player)
                                        .FirstOrDefault();
                if (!_selectionController) return;
                _selectionController.SelectableBuildings.Add(gameObject);
            }
        }
        public PlayerInformation Player { get { return _player; } }

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
            SpawnPoint = (transform.forward * 5) + transform.position;
        }

    }
}
