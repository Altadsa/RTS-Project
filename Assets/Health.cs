using UnityEngine;

namespace RTS
{
    public abstract class Health : MonoBehaviour, IDamageable, IKillable
    {
        [SerializeField]
        protected float maxHealth = 100;
        protected float health = 100;

        private GameObject _healthUi;

        public float armourValue = 0;

        public delegate void OnHealthChanged(float healthAsPercentage);
        public event OnHealthChanged onHealthChanged;

        public abstract void TakeDamage(float damage);

        private void Awake()
        {
            health = maxHealth;
            _healthUi = GetComponentInChildren<HealthUI>().gameObject;
        }

        private void Start()
        {
            UpdateHealth();
            ToggleUi();
        }

        void Update()
        {
            HandleUnitHealth();
        }

        private void OnMouseOver()
        {
            _healthUi.SetActive(true);
        }

        private void OnMouseExit()
        {
            _healthUi.SetActive(false);
        }

        private void HandleUnitHealth()
        {
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

        private void ToggleUi()
        {
            _healthUi.SetActive(!_healthUi.activeInHierarchy);
        }
    }
}
