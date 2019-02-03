using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RTS
{
    public class UnitWork : UnitAction
    {

        ResourceType _resourceToWork;
        int _resourceAmount = 0;
        int _maxCarryLoad = 5;
        int _currentCarryLoad = 0;
        Headquarters _dropOffPoint;

        bool isDroppingResources;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _dropOffPoint = FindObjectOfType<Headquarters>();
        }

        private void Update()
        {

            if (!_target) return;
            _actionCooldown += Time.deltaTime;
            if (!isDroppingResources)
            {
                if (_agent.destination != _target.transform.position) _agent.SetDestination(_target.transform.position);
                WorkIfWithinRange();
                return;
            }
            DropResourcesAndReturnToWork();
        }

        private void WorkIfWithinRange()
        {
            if (Vector3.Distance(transform.position, _target.transform.position) <= actionRange)
            {
                Resource resource = _target.GetComponent<Resource>();
                if (!resource) { _target = null; return; }
                _resourceToWork = resource.ResourceType();
                if (_actionCooldown >= _timeToAction)
                {
                    MineResource(resource);
                    _actionCooldown = 0;
                }
            }
        }

        private void DropResourcesAndReturnToWork()
        {
            Vector3 pos = transform.position;
            Vector3 hqPos = _dropOffPoint.transform.position;
            float distToHq = Vector3.Distance(pos, hqPos);
            if (distToHq <= actionRange)
            {
                _dropOffPoint.DropOffResources(_resourceToWork, _resourceAmount);
                _resourceAmount = 0;
                _currentCarryLoad = 0;
                Vector3 tarPos = _target.transform.position;
                isDroppingResources = false;
                _agent.SetDestination(tarPos);
            }
        }

        private void MineResource(Resource resource)
        {
            if (_currentCarryLoad < _maxCarryLoad)
            {
                _currentCarryLoad += resource.Mine();
                _resourceAmount++;
                return;
            }
            ReturnToHeadquarters();
        }

        private void ReturnToHeadquarters()
        {
            Vector3 hDest = _dropOffPoint.transform.position;
            _agent.SetDestination(hDest);
            isDroppingResources = true;
        }
    }
}