using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace RTS
{
    public class ButtonTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] GameObject _tooltip;
        [SerializeField] Text _title;
        [SerializeField] Text _desc;
        [SerializeField] Text _costs;

        private void Start()        
        {
            _tooltip.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _tooltip.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _tooltip.SetActive(false);
        }

        public void SetTooltipData(ProductionData data)
        {
            _title.text = data.Name;
            _desc.text = data.Description;
            ResourceCost cost = data.Cost;
            _costs.text = string.Format(": {0}\n: {1}\n: {2}", cost.Gold, cost.Timber, cost.Food);
        }

    } 
}
