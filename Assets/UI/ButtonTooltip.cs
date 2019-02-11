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

        public void SetTooltipData(ProductionData data)
        {
            Text tText = _tooltip.GetComponentInChildren<Text>();
            ResourceCost cost = data.Cost;
            tText.text = string.Format("{0}\nGold:{1}\nTimber:{2}\nFood:{3}", data.Name, cost.Gold, cost.Timber, cost.Food);
        }

    } 
}
