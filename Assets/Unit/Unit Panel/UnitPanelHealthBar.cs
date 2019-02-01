using System.Security.Cryptography;
using UnityEngine.UI;
using UnityEngine;

namespace RTS
{
    public class UnitPanelHealthBar : MonoBehaviour
    {

        [SerializeField] Image barImage;
        private UnitHealth _health;

        public void Initialize(UnitHealth health)
        {
            _health = health;
            health.onHealthChanged += UpdateUiHealth;
        }

        private void UpdateUiHealth(float hPercent)
        {
            barImage.fillAmount = hPercent;
            if (!(hPercent <= 0)) return;
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _health.onHealthChanged -= UpdateUiHealth;
        }


    } 
}
