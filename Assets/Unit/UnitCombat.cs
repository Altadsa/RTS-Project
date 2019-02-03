using UnityEngine;
using UnityEngine.AI;

namespace RTS
{
    public class UnitCombat : UnitAction
    {
        public float damage;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            AttackTargetIfPossible();
        }

        private void AttackTargetIfPossible()
        {
            if (!_target) { return; }
            _actionCooldown += Time.deltaTime;
            if (_agent.destination != _target.transform.position) _agent.SetDestination(_target.transform.position);
            //Debug.Log(_agent.pathStatus);
            Attack();
        }

        private void Attack()
        {
            bool canAttack = (_actionCooldown >= _timeToAction) && IsWithinAttackRange();
            if (canAttack)
            {
                if (_target.GetComponent<EnemyUnit>())
                {
                    _target.GetComponent<EnemyUnit>().TakeDamage(damage);
                    _actionCooldown = 0; 
                }
                if (_target.GetComponent<BuildingHealth>())
                {
                    _target.GetComponent<BuildingHealth>().TakeDamage(damage);
                    _actionCooldown = 0;
                }
            }
        }

        bool IsWithinAttackRange()
        {
            float distance = Vector3.Distance(transform.position, _target.transform.position);
            if (distance <= actionRange)
            {
                return true;
            }
            return false;
        }

        private void OnTriggerStay(Collider other)
        {
            if (_target || !_agent.isStopped) return;
            if (other.GetComponent<EnemyUnit>())
            {
                _target = other.gameObject;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            if (_target)
            Gizmos.DrawWireSphere(_target.transform.position, actionRange);
        }
    } 
}
