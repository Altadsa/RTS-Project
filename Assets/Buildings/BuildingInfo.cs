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
        public Sprite _emptyQueuePosition;

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
            UpdateQueue(building);
        }

        private void UpdateQueue(Building building)
        {
            var buildingQueue = building.Queue;
            _itemInProduction.sprite = buildingQueue[0].Icon();
            for (int i = 1; i < 5; i++)
            {
                if (i >= buildingQueue.Count)
                    _itemsInQueue[i - 1].sprite = _emptyQueuePosition;
                else
                    _itemsInQueue[i - 1].sprite = buildingQueue[i].Icon();
            }
        }

        private void ClearProductionItem()
        {
            _itemInProduction.sprite = _emptyQueuePosition;
        }
    } 


}
