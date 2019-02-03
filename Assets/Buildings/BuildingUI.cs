using UnityEngine;
using UnityEngine.UI;

namespace RTS
{
    [RequireComponent(typeof(Canvas))]
    public class BuildingUI : MonoBehaviour
    {
        BuildingHealth buildingHealth;
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
            buildingHealth = GetComponentInParent<BuildingHealth>();
            barMask = GetComponentInChildren<RawImage>();
            healthBar = GetComponentInChildren<Image>();
            uiCanvas = GetComponent<Canvas>();
            if (buildingHealth && healthBar)
            {
                buildingHealth.onHealthChanged += OnHealthChanged;
            }
            uiCanvas.worldCamera = Camera.main;
        }

        private void OnHealthChanged(float healthAsPercentage)
        {
            healthBar.fillAmount = healthAsPercentage;
        }

        private void UpdateBuildProgress(float buildPercentage)
        {

        }

    }
}
