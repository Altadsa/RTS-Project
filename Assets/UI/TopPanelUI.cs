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
            _timberText.text =  ResourceData.Timber.ToString();
            _goldText.text =  ResourceData.Gold.ToString();
            _foodText.text = string.Format("{0}/{1}", ResourceData.Food, ResourceData.MaxFood);
        }
    }
}