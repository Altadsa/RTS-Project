using UnityEngine;
using UnityEngine.AI;

namespace RTS
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(UnitHealth))]
    public abstract class Unit : MonoBehaviour
    {

        protected NavMeshAgent agent;

        protected GameObject target;

        public void TakeDamage(float damage)
        {
            GetComponentInChildren<UnitHealth>().TakeDamage(damage);
        }

    }
}
