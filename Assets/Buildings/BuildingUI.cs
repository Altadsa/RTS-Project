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
            buildingHealth = GetComponentInParent<BuildingHealth>();
            barMask = GetComponentInChildren<RawImage>();
            healthBar = GetComponentInChildren<Image>();
            uiCanvas = GetComponent<Canvas>();
            if (buildingHealth && healthBar)
            {
                buildingHealth.updateBuildingHealth += OnHealthChanged;
            }
            uiCanvas.worldCamera = Camera.main;
        }

        private void LateUpdate()
        {
            barMask.transform.LookAt(Camera.main.transform);
        }

        private void OnHealthChanged(float health, float maxHealth)
        {
            float healthPercentage = health / maxHealth;
            healthBar.fillAmount = healthPercentage;
        }

    }
}
