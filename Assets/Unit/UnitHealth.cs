using UnityEngine;

namespace RTS
{
    public class UnitHealth : Health
    {

        public override void TakeDamage(float damage)
        {
            if (health > 0)
            {
                float redDamage = (damage * DamageReduction());
                Debug.Log(string.Format("{0} takes {1} damage (Base: {2} Armour Reduction: {3}%)", name, redDamage, damage, (armourValue * 0.025f)*100));
                health -= redDamage;
                UpdateHealth();
            }
        }

        private float DamageReduction()
        {
            return 1 - (armourValue * 0.025f);
        }
    }
}