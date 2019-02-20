using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class Tower : MonoBehaviour
    {
        List<GameObject> targets = new List<GameObject>(10);

        [SerializeField] float _attackSpeed = 0.8f;
        [SerializeField] float _damage;
        Building _building;
        float timeSinceAttack = 0;

        private void Start()
        {
            _building = GetComponent<Building>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (targets.Count >= targets.Capacity) return;
            if (targets.Contains(other.gameObject)) return;
            if (other.GetComponent<Unit>().PlayerOwner == _building.Player)
            { 
                targets.Add(other.gameObject);
            }

        }

        private void OnTriggerExit(Collider other)
        {
            Unit iUnit = other.GetComponent<Unit>();
            if (targets.Contains(iUnit.gameObject))
            {
                targets.Remove(other.gameObject);
            }
        }

        private void Update()
        {
            timeSinceAttack += Time.deltaTime;
            if (targets.Count <= 0) return;
            bool canAttack = (timeSinceAttack > _attackSpeed);
            if (canAttack)
            {
                List<GameObject> deadTargets = new List<GameObject>();
                foreach (GameObject target in targets)
                {
                    if (!target)
                    {
                        deadTargets.Add(target);
                    }
                    else
                    {
                        UnitHealth unitHealth = target.GetComponent<UnitHealth>();
                        unitHealth.TakeDamage(_damage);
                    }

                }
                if (deadTargets.Count > 0)
                {
                    foreach (GameObject deadTarget in deadTargets)
                    {
                        targets.Remove(deadTarget);
                    }
                }
                timeSinceAttack = 0;
            }
        }

        private void RemoveDeadTargets()
        {
            List<GameObject> deadTargets = new List<GameObject>();
            foreach (GameObject target in targets)
            {
                if (!target)
                {
                    deadTargets.Add(target);
                }
            }
        }
    } 
}
