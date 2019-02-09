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

        private void Start()
        {
            _building = GetComponent<Building>();
        }

        public List<GameObject> CreateUnitButtons()
        {
            List<GameObject> nButtons = new List<GameObject>();
            foreach (var data in _unitData)
            {
                GameObject nButton = Instantiate(_actionButtonPrefab);
                nButton.GetComponent<Image>().sprite = data.QueueImage;
                nButton.GetComponent<Button>().onClick.AddListener(delegate { BuildUnit(data); });
                nButton.GetComponent<ButtonTooltip>().SetTooltipData(data);
                nButtons.Add(nButton);
            }
            return nButtons;
        }

        private void BuildUnit(UnitBuildData unitInstance)
        {
            bool canBuild = (_building.Queue.Count < 5) && CanBuyUnit(unitInstance);
            if (!canBuild) return;
            _building.AddToQueue(unitInstance);
            ResourceData.AmendGold(-unitInstance.GoldCost);
            ResourceData.AmendFood(-unitInstance.FoodCost);
            ResourceData.AmendTimber(-unitInstance.TimberCost);
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
