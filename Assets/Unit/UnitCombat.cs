using UnityEngine;
using UnityEngine.AI;

namespace RTS
{
    public class UnitCombat : UnitAction
    {
        public float _baseDamage;
        [SerializeField] FloatVar _meleeDmgModifier;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            //Debug.Log(string.Format("Base Damage: {0}, Modified Damage: {1} ({2}% from modifier.", _baseDamage, _baseDamage * _meleeDmgModifier.Value, 1 - _meleeDmgModifier.Value));
            AttackTargetIfPossible();
        }

        private void AttackTargetIfPossible()
        {
            if (!_target) { return; }
            _actionCooldown += Time.deltaTime;
            if (_agent.destination != _target.transform.position) _agent.SetDestination(_target.transform.position);
            Attack();
        }

        private void Attack()
        {
            bool canAttack = (_actionCooldown >= _timeToAction) && IsWithinAttackRange();
            if (canAttack)
            {
                if (_target.GetComponent<EnemyUnit>())
                {
                    _target.GetComponent<EnemyUnit>().TakeDamage(_baseDamage * _meleeDmgModifier.Value);
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
