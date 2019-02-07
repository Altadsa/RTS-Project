using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

namespace RTS
{
    public class TopPanelUI : MonoBehaviour
    {
        [SerializeField] private Text _timberText;
        [SerializeField] private Text _goldText;
        [SerializeField] private Text _foodText;

        private void Update()
        {
            _timberText.text = "Timber: " + ResourceData.Timber;
            _goldText.text = "Gold: " + ResourceData.Gold;
            _foodText.text = "Food: " + ResourceData.Food;
        }
    }
}