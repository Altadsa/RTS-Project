using UnityEngine;
using UnityEngine.UI;

namespace RTS
{
    [RequireComponent(typeof(Canvas))]
    public class UnitUI : MonoBehaviour
    {
        UnitHealth unitHealth;
        RawImage barMask;
        Image healthBar;
        Canvas uiCanvas;

        void Awake()
        {
            unitHealth = GetComponentInParent<UnitHealth>();
            barMask = GetComponentInChildren<RawImage>();
            healthBar = GetComponentInChildren<Image>();
            uiCanvas = GetComponent<Canvas>();
            if (unitHealth && healthBar)
            {
                unitHealth.onHealthChanged += OnHealthChanged;
            }
            uiCanvas.worldCamera = Camera.main;
        }

        private void LateUpdate()
        {
            barMask.transform.LookAt(Camera.main.transform);
        }

        private void OnHealthChanged(float hPercent)
        {
            healthBar.fillAmount = hPercent;
        }

    } 
}
