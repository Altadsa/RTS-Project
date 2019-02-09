using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace RTS
{
    public class ButtonTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] GameObject _tooltip;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _tooltip.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _tooltip.SetActive(false);
        }

        private void Start()
        {
            _tooltip.SetActive(false);
        }

        public void SetTooltipData(UnitBuildData data)
        {
            Text tText = _tooltip.GetComponentInChildren<Text>();
            tText.text = string.Format("{0}\nGold:{1}\nTimber:{2}\nFood:{3}", data.Unit.name, data.GoldCost, data.TimberCost, data.FoodCost);
        }

    } 
}
