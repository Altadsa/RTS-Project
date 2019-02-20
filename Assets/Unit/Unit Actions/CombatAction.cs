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
            if (!target.GetComponentInParent<Health>()) return false;
            _target = target;
            if (!IsTargetAttackable()) return false;
            StartCoroutine(AttackTarget());
            return true;
        }

        IEnumerator AttackTarget()
        {
            Vector3 targetPos = _target.transform.position;
            _agent.SetDestination(targetPos);
            while (DistanceToTarget(targetPos) > actionRange)
            {
                yield return new WaitForEndOfFrame();
            }
            Health targetHealth = _target.GetComponentInParent<Health>();
            while (targetHealth)
            {
                targetHealth.TakeDamage(Damage);
                yield return new WaitForSeconds(_timeToAction);
            }
        }

        float Damage
        {
            get { return _baseDamage * _dmgModifier.Value; }
        }

        bool IsTargetAttackable()
        {
            Unit targetUnit = _target.GetComponent<Unit>();
            if (targetUnit.PlayerOwner != _unit.PlayerOwner) return true;
            return false;
        }

        private void OnTriggerStay(Collider other)
        {
            
        }
    } 
}
