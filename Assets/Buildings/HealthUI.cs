using UnityEngine;
using UnityEngine.UI;

namespace RTS
{
    [RequireComponent(typeof(Canvas))]
    public class HealthUI : MonoBehaviour
    {
        Health _health;
        RawImage barMask;
        Image healthBar;
        Canvas uiCanvas;

        void Awake()
        {
            Initialize();
        }

        private void LateUpdate()
        {
            barMask.transform.LookAt(Camera.main.transform);
        }

        private void Initialize()
        {
            _health = GetComponentInParent<Health>();
            barMask = GetComponentInChildren<RawImage>();
            healthBar = GetComponentInChildren<Image>();
            uiCanvas = GetComponent<Canvas>();
            if (_health && healthBar)
            {
                _health.onHealthChanged += OnHealthChanged;
            }
            uiCanvas.worldCamera = Camera.main;
        }

        private void OnHealthChanged(float healthAsPercentage)
        {
            healthBar.fillAmount = healthAsPercentage;
        }

    }
}
