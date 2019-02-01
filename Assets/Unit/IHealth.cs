using UnityEngine;

namespace RTS
{
    public interface IHealth
    {
        void Kill();
        void TakeDamage(float damage);
        void UpdateHealth();
    }
}