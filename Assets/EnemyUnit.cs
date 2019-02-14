using UnityEngine;
using UnityEngine.AI;

namespace RTS
{
    public class EnemyUnit : MonoBehaviour
    {
        //public float attackRange = 2;
        //public float attackSpeed = 1;
        //public float damage;
        //float timeSinceAttack = 0;

        private void Awake()
        {
        }

        private void Update()
        {

        }

        //private void AttackTargetIfPossible()
        //{
        //    timeSinceAttack += Time.deltaTime;
        //    if (!target) { return; }
        //    agent.SetDestination(target.transform.position);
        //    if (agent.remainingDistance >= attackRange)
        //    {
        //        agent.isStopped = false;
        //        agent.SetDestination(target.transform.position);
        //    }
        //    else
        //    {
        //        agent.isStopped = true;
        //        GetComponent<Rigidbody>().velocity = Vector3.zero;
        //        Attack();
        //    }
        //}

        //private void Attack()
        //{
        //    bool canAttack = (timeSinceAttack >= attackSpeed) && IsWithinAttackRange();
        //    if (canAttack)
        //    {
        //        target.GetComponent<PlayerUnit>().TakeDamage(damage);
        //        timeSinceAttack = 0;
        //    }
        //}

        //bool IsWithinAttackRange()
        //{
        //    float distance = Vector3.Distance(transform.position, target.transform.position);
        //    if (distance <= attackRange)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //private void OnTriggerStay(Collider other)
        //{
        //    if (!target)
        //    {
        //        if (other.GetComponent<PlayerUnit>())
        //        {
        //            target = other.gameObject;
        //        }
        //    }
        //}

    } 
}
