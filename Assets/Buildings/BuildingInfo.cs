using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RTS
{
    public class BuildingInfo : MonoBehaviour
    {
        [SerializeField] GameObject _buttonPrefab;
        [SerializeField] GameObject[] _queuePositions;
        public Image _productionProgress;

        int _lastQueueSize = 0;

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
            CreateQueueButtons(building);
        }

        private void CreateQueueButtons(Building buildingToCreateFor)
        {
            List<ProductionData> queue = buildingToCreateFor.Queue;
            if (_lastQueueSize != queue.Count)
            {
                ClearOldButtons();
                for (int i = 0; i < queue.Count; i++)
                {
                    GameObject qButton = Instantiate(_buttonPrefab, _queuePositions[i].transform);
                    qButton.GetComponent<Image>().sprite = queue[i].Icon;
                    ProductionData data = queue[i];
                    Button buttonComponent = qButton.GetComponent<Button>();
                    buttonComponent.onClick.AddListener(delegate { RefundCost(data.Cost); });
                    buttonComponent.onClick.AddListener(delegate { RemoveFromQueue(buildingToCreateFor, data); });
                    buttonComponent.onClick.AddListener(delegate { DestroyQueueButton(qButton); });
                }
                _lastQueueSize = queue.Count;
            }
        }

        private void ClearOldButtons()
        {
            foreach (var obj in _queuePositions)
            {
                if (obj.transform.childCount <= 0) return;
                GameObject toDest = obj.GetComponentInChildren<Button>().gameObject;
                if (toDest)
                {
                    Destroy(toDest); 
                }
            }
        }


        private void RefundCost(ResourceCost cost)
        {
            ResourceData.AmendGold(cost.Gold);
            ResourceData.AmendTimber(cost.Timber);
            ResourceData.AmendFood(cost.Food);
        }

        private void RemoveFromQueue(Building building, ProductionData dataToRemove)
        {
            building.RemoveFromQueue(dataToRemove);
        }

        private void DestroyQueueButton(GameObject buttonToRemove)
        {
            Destroy(buttonToRemove);
        }
    } 


}
