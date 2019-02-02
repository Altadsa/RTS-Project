using UnityEngine;

namespace RTS
{
    public class BuildingHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] float maxHealth = 250;
        float health;

        public delegate void OnHealthChanged(float health, float maxHealth);
        public event OnHealthChanged updateBuildingHealth;

        private void Awake()
        {
            health = maxHealth;
        }

        private void Update()
        {
            UpdateHealth();
            DestroyBuilding();
        }

        private void UpdateHealth()
        {
            if (updateBuildingHealth != null)
            {
                updateBuildingHealth(health, maxHealth);
            }
        }

        private void DestroyBuilding()
        {
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }

        public void TakeDamage(float damage)
        {
            if (health > 0)
            {
                health -= damage;
            }
        }
    } 
}
