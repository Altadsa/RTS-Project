using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace RTS
{
    public class UnitCombat : UnitAction
    {
        public float _baseDamage;
        [SerializeField] FloatVar _meleeDmgModifier;

        public override bool IsTargetValid(GameObject target)
        {
            throw new System.NotImplementedException();
        }

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
                if (_target.GetComponent<Player>()._player != GetComponent<Player>()._player)
                { 
                    _target.GetComponent<Health>().TakeDamage(_baseDamage * _meleeDmgModifier.Value);
                    _actionCooldown = 0; 
                }
            }
        }

        private void Retreat()
        {
            Debug.Log("Retreating.");
            Headquarters[] allHqs = FindObjectsOfType<Headquarters>();
            var closest = allHqs[0];
            foreach (var hq in allHqs)
            {
                float currentDistance = Vector3.Distance(transform.position, closest.transform.position);
                float checkDistance = Vector3.Distance(transform.position, hq.transform.position);
                if (checkDistance < currentDistance) closest = hq;
            }
            _target = null;
            _agent.SetDestination(closest.DropPoint);
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

        bool IsAttackable()
        {
            return true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (_target || _agent.remainingDistance > 1) return;
            PlayerInformation unitPlayer = other.GetComponent<Player>()._player;
            if (unitPlayer == null) return;
            if (unitPlayer != GetComponent<Player>()._player)
            {
                _target = other.gameObject;
            }
        }

    } 
}
