using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RTS
{
    public class WorkerActions : UnitAction
    {

        ResourceType _resourceToWork;
        int _resourceAmount = 0;
        float _maxCarryLoad = 5;
        float _currentCarryLoad = 0;
        Headquarters _dropPoint;

        bool _isDroppingResources;

        public override bool IsTargetValid(GameObject target)
        {
            return true;
        }

        private void Update()
        {
            if (!_target) return;
            _actionCooldown += Time.deltaTime;
            if (!_isDroppingResources)
            {
                MoveToWork();
                WorkIfWithinRange();
                return;
            }
        }

        private void MoveToWork()
        {
            _agent.SetDestination(_target.transform.position);
        }

        private void WorkIfWithinRange()
        {
            if (!IsInRange()) return;
            RepairBuilding();
        }

        private bool IsInRange()
        {
            float distanceToTarget = Vector3.Distance(transform.position, _target.transform.position);
            return distanceToTarget <= actionRange;
        }


        private void RepairBuilding()
        {
            Building building = _target.GetComponent<Building>();
            if (!building) return;
            if (_actionCooldown >= _timeToAction)
            {
                Repair(building);
            }
        }

        private void Repair(Building building)
        {
            var buildingHealth = building.GetComponent<BuildingHealth>();
            if (buildingHealth
                 && buildingHealth.NeedsRepaired)
            {
                buildingHealth.Repair();
                _actionCooldown = 0;
                return;
            }
            _target = null;
        }

    }
}