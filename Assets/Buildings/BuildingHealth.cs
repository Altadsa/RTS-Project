using UnityEngine;

namespace RTS
{
    public class BuildingHealth : Health
    {
        public override void TakeDamage(float damage)
        {
            if (health > 0)
            {
                health -= damage;
                UpdateHealth();
            }
        }
    } 
}
