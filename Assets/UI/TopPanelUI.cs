using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

namespace RTS
{
    public class TopPanelUI : MonoBehaviour
    {
        ResourceData _resourceData;

        [SerializeField] private Text _timberText;
        [SerializeField] private Text _goldText;
        [SerializeField] private Text _foodText;

        private void Start()
        {
            _resourceData = GameManager.Default.ResourceData;
        }

        private void Update()
        {
            _timberText.text = _resourceData.Timber.ToString();
            _goldText.text = _resourceData.Gold.ToString();
            _foodText.text = string.Format("{0}/{1}", _resourceData.Food, _resourceData.MaxFood);
        }
    }
}