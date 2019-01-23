using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class Tower : MonoBehaviour
    {
        List<GameObject> targets = new List<GameObject>(10);

        public float attackSpeed = 0.8f;
        public float damage;
        float timeSinceAttack = 0;

        private void OnTriggerEnter(Collider other)
        {
            if (targets.Count >= targets.Capacity) return;
            if (targets.Contains(other.gameObject)) return;
            if (other.GetComponent<PlayerUnit>())
            { 
                targets.Add(other.gameObject);
            }

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<PlayerUnit>())
            {
                targets.Remove(other.gameObject);
            }
        }

        private void Update()
        {
            timeSinceAttack += Time.deltaTime;
            if (targets.Count <= 0) return;
            bool canAttack = (timeSinceAttack > attackSpeed);
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
                        unitHealth.TakeDamage(damage);
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
