using UnityEngine;

namespace RTS
{
    public abstract class Health : MonoBehaviour, IDamageable, IKillable
    {
        [SerializeField]
        protected float maxHealth = 100;

        protected float health = 100;

        public delegate void OnHealthChanged(float healthAsPercentage);
        public event OnHealthChanged onHealthChanged;

        private void Awake()
        {
            health = maxHealth;
        }

        private void Start()
        {
            UpdateHealth();
        }

        void Update()
        {
            HandleUnitHealth();
        }

        private void HandleUnitHealth()
        {
            //UpdateHealth();
            Kill();
        }

        public void UpdateHealth()
        {
            if (onHealthChanged != null)
            {
                float hPercent = health / maxHealth;
                onHealthChanged(hPercent);
            }
        }

        public void Kill()
        {
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }

        public virtual void TakeDamage(float damage) { }
    }
}
