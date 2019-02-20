using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RTS
{
    public class BuildingActions : MonoBehaviour
    {
        Building _building;
        ResourceData _playerResourceData;

        [SerializeField] GameObject _actionButtonPrefab;
        [SerializeField] List<ProductionData> _data = new List<ProductionData>();

        private void Start()
        {
            _building = GetComponent<Building>();
            _playerResourceData = _building.Player.ResourceData;
        }

        public List<GameObject> CreateUnitButtons()
        {
            List<GameObject> nButtons = new List<GameObject>();
            nButtons.AddRange(CreateButtonsFromData(_data));
            return nButtons;
        }

        private List<GameObject> CreateButtonsFromData(List<ProductionData> queueData)
        {
            List<GameObject> nButtons = new List<GameObject>();
            foreach (var data in queueData)
            {
                GameObject nButton = Instantiate(_actionButtonPrefab);
                nButton.GetComponent<Image>().sprite = data.Icon;
                nButton.GetComponent<Button>().onClick.AddListener(delegate { BuyAndAddToQueue(data); });
                nButton.GetComponent<ButtonTooltip>().SetTooltipData(data);
                nButtons.Add(nButton);
            }
            return nButtons;
        }

        private void BuyAndAddToQueue(ProductionData item)
        {
            ResourceCost cost = item.Cost;
            if (!item.RequirementsMet()) return;
            bool canBuild = (_building.Queue.Count < 5) && CanBuy(cost);
            if (!canBuild) return;
            var dataToAdd = item as IQueueable;
            _building.AddToQueue(dataToAdd);
            _playerResourceData.AmendGold(-cost.Gold);
            _playerResourceData.AmendFood(cost.Food);
            _playerResourceData.AmendTimber(-cost.Timber);
        }

        private bool CanBuy(ResourceCost cost)
        {
            if (cost.Gold > _playerResourceData.Gold) return false;
            if (cost.Timber > _playerResourceData.Timber) return false;
            if ((cost.Food + _playerResourceData.Food) >= _playerResourceData.MaxFood) return false;
            return true;
        }

    } 
}
