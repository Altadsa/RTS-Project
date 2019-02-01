using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RTS
{
    public class UnitWork : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private GameObject _target;
        private float _timeSinceAction;
        public float _timeToAction = 2;
        public float actionRange = 5;
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        public void MineResource(GameObject target)
        {
            _target = target;
            Vector3 dest = _target.transform.position;
            _agent.SetDestination(dest);
        }

        private void Update()
        {
            if (!_target) return;
            _timeSinceAction += Time.deltaTime;
            if (Vector3.Distance(transform.position, _target.transform.position) <= actionRange)
            {
                Resource resource = _target.GetComponent<Resource>();
                if (!resource) return;
                if (_timeSinceAction >= _timeToAction)
                {
                    resource.Mine();
                    _timeSinceAction = 0;
                }
            }
        }
    }
}