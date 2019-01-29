using UnityEngine;
using UnityEngine.AI;

namespace RTS
{
    public class UnitCombat : MonoBehaviour
    {

        public float attackRange = 2;
        public float attackSpeed = 1;
        public float damage;
        float timeSinceAttack = 0;

        GameObject target;
        NavMeshAgent agent;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            AttackTargetIfPossible();
        }

        public void Target(GameObject _target)
        {
            agent.isStopped = false;
            target = _target;
        }

        private void AttackTargetIfPossible()
        {
            timeSinceAttack += Time.deltaTime;
            if (!target) { return; }
            agent.SetDestination(target.transform.position);
            if (agent.remainingDistance >= attackRange)
            {
                agent.isStopped = false;
                agent.SetDestination(target.transform.position);
            }
            else
            {
                agent.isStopped = true;
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                Attack();
            }
        }

        private void Attack()
        {
            bool canAttack = (timeSinceAttack >= attackSpeed) && IsWithinAttackRange();
            if (canAttack)
            {
                if (target.GetComponent<EnemyUnit>())
                {
                    target.GetComponent<EnemyUnit>().TakeDamage(damage);
                    timeSinceAttack = 0; 
                }
                if (target.GetComponent<BuildingHealth>())
                {
                    target.GetComponent<BuildingHealth>().TakeDamage(damage);
                    timeSinceAttack = 0;
                }
            }
        }

        bool IsWithinAttackRange()
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance <= attackRange)
            {
                return true;
            }
            return false;
        }

        private void OnTriggerStay(Collider other)
        {
            if (target || !agent.isStopped) return;
            if (other.GetComponent<EnemyUnit>())
            {
                target = other.gameObject;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            if (target)
            Gizmos.DrawWireSphere(target.transform.position, attackRange);
        }
    } 
}
