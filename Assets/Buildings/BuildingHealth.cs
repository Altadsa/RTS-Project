using UnityEngine;

namespace RTS
{
    public class BuildingHealth : Health
    {
        public bool NeedsRepaired { get; private set; }

        public override void TakeDamage(float damage)
        {
            if (health > 0)
            {
                health -= damage;
                NeedsRepaired = true;
                UpdateHealth();
            }
        }

        public void Repair()
        {
            if (health < maxHealth)
            {
                health++;
                UpdateHealth();
                return;
            }
            NeedsRepaired = false;
        }
    } 
}
