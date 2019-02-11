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
            tText.text = string.Format("{0}\n{1}\nGold:{2}\nTimber:{3}\nFood:{4}\nRequirements:{5}", data.Name, data.Description, cost.Gold, cost.Timber, cost.Food, data.Requirements);
        }

    } 
}
