using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RTS
{
    public class BuildingActions : MonoBehaviour
    {
        Building _building;

        [SerializeField] GameObject _actionButtonPrefab;
        [SerializeField] List<UnitBuildData> _unitData;
        [SerializeField] List<UpgradeData> _upgradeData;
        [SerializeField] List<IQueueable> _data = new List<IQueueable>();

        private void Start()
        {
            _building = GetComponent<Building>();
            _data.AddRange(_unitData);
            _data.AddRange(_upgradeData);
        }

        public List<GameObject> CreateUnitButtons()
        {
            List<GameObject> nButtons = new List<GameObject>();
            nButtons.AddRange(CreateButtonsFromData(_data));
            return nButtons;
        }

        private List<GameObject> CreateButtonsFromData(List<IQueueable> queueData)
        {
            List<GameObject> nButtons = new List<GameObject>();
            foreach (var data in queueData)
            {
                GameObject nButton = Instantiate(_actionButtonPrefab);
                nButton.GetComponent<Image>().sprite = data.Icon();
                nButton.GetComponent<Button>().onClick.AddListener(delegate { BuyAndAddToQueue(data); });
                nButton.GetComponent<ButtonTooltip>().SetTooltipData(data);
                nButtons.Add(nButton);
            }
            return nButtons;
        }

        private void BuyAndAddToQueue(IQueueable item)
        {
            Vector3Int cost = item.Cost();
            if (!item.RequirementsMet()) return;
            bool canBuild = (_building.Queue.Count < 5) && CanBuy(cost);
            if (!canBuild) return;
            _building.AddToQueue(item);
            ResourceData.AmendGold(-cost.x);
            ResourceData.AmendFood(-cost.z);
            ResourceData.AmendTimber(-cost.y);
        }

        private bool CanBuy(Vector3Int cost)
        {
            if (cost.x > ResourceData.Gold) return false;
            if (cost.y > ResourceData.Timber) return false;
            if (cost.z > ResourceData.Food) return false;
            return true;
        }

    } 
}
