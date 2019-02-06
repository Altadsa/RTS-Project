using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RTS
{
    public class BuildingInfo : MonoBehaviour
    {
        public Image _productionProgress;
        public Image _itemInProduction;
        public Image[] _itemsInQueue;
        public Sprite emptyQueuePosition;

        UnitBuildData[] _buildQueueCopy = new UnitBuildData[5];

        private static BuildingInfo _instance;
        private static readonly object padlock = new object();
        public static BuildingInfo Instance
        {
            get
            {
                lock (padlock)
                {
                    if (!_instance) { _instance = FindObjectOfType<BuildingInfo>(); }
                    return _instance;
                }
            }
        }

        public void LoadBuildingInfo(Building building)
        {
            building.UpdateInfo += UpdateInfo;
        }

        public void ClearBuildingInfo(Building building)
        {
            building.UpdateInfo -= UpdateInfo;
        }

        private void UpdateInfo(Building building, float progress)
        {
            _productionProgress.fillAmount = progress;
            if (building.Queue.Count <= 0)
            {
                ClearProductionItem();
                return;
            }
            ClearQueueCopy();
            UpdateQueue(building);
        }

        private void UpdateQueue(Building building)
        {
            var buildingQueue = building.Queue;
            buildingQueue.CopyTo(_buildQueueCopy, 0);
            _itemInProduction.sprite = _buildQueueCopy[0].QueueImage;
            for (int i = 1; i < _buildQueueCopy.Length; i++)
            {
                if (_buildQueueCopy[i] == null)
                    _itemsInQueue[i - 1].sprite = emptyQueuePosition;
                else
                    _itemsInQueue[i - 1].sprite = _buildQueueCopy[i].QueueImage;
            }
        }

        private void ClearProductionItem()
        {
            _itemInProduction.sprite = emptyQueuePosition;
        }

        private void ClearQueueCopy()
        {
            for (int i = 0; i < _buildQueueCopy.Length; i++)
            {
                _buildQueueCopy[i] = null;
            }
        }
    } 


}
