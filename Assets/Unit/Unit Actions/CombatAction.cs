using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class CombatAction : UnitAction
    {
        [SerializeField] float _baseDamage;
        [SerializeField] FloatVar _dmgModifier;

        public override bool IsTargetValid(GameObject target)
        {
            StopAllCoroutines();
            _target = target;
            if (!IsTargetAttackable()) return false;
            StartCoroutine(AttackTarget());
            return true;
        }

        IEnumerator AttackTarget()
        {
            Health targetHealth = _target.GetComponentInParent<Health>();
            while (targetHealth)
            {
                yield return new WaitForEndOfFrame();
                Vector3 targetPos = _target.transform.position;
                _agent.isStopped = false;
                _agent.SetDestination(targetPos);
                if (DistanceToTarget(targetPos) <= actionRange)
                {
                    _agent.isStopped = true;
                    targetHealth.TakeDamage(Damage);
                    yield return new WaitForSeconds(_timeToAction);
                }
                else continue;
            }
        }

        float Damage
        {
            get { return _baseDamage * _dmgModifier.Value; }
        }

        bool IsTargetAttackable()
        {
            Unit targetUnit = _target.GetComponentInParent<Unit>();
            bool hasHealth = _target.GetComponentInParent<Health>();
            bool isNotPlayer = targetUnit.PlayerOwner != _unit.PlayerOwner;
            if (hasHealth && isNotPlayer) return true;
            return false;
        }

        private void OnTriggerStay(Collider other)
        {
            
        }
    } 
}
