using UnityEngine;

namespace RTS
{
    public class UnitHealth : MonoBehaviour
    {
        [SerializeField]
        float maxHealth = 100;

        float health = 100;

        public int armourValue = 0;

        public delegate void OnHealthChanged(float health, float maxHealth);
        public event OnHealthChanged onHealthChanged;

        private void Awake()
        {
            health = maxHealth;
        }

        void Update()
        {
            HandleUnitHealth();
        }

        private void HandleUnitHealth()
        {
            UpdateHealth();
            KillUnit();
        }

        private void UpdateHealth()
        {
            if (onHealthChanged != null)
            {
                onHealthChanged(health, maxHealth);
            }
        }

        private void KillUnit()
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
                float redDamage = (damage * DamageReduction());
                Debug.Log(string.Format("{0} takes {1} damage (Base: {2} Armour Reduction: {3}%)", name, redDamage, damage, (armourValue * 0.025f)*100));
                health -= redDamage;
            }
        }

        private float DamageReduction()
        {
            return 1 - (armourValue * 0.025f);
        }
    }

}