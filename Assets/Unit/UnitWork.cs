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
        float _maxCarryLoad = 5;
        float _currentCarryLoad = 0;
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
            float distanceToTarget = Vector3.Distance(transform.position, _target.transform.position);
            Debug.Log(distanceToTarget);
            if (distanceToTarget <= actionRange)
            {
                GatherResource();
                ConstructBuilding();
                RepairBuilding();
            }
        }

        private void GatherResource()
        {
            Resource resource = _target.GetComponent<Resource>();
            if (!resource) { return; }
            _resourceToWork = resource.ResourceType();
            _timeToAction = resource.WorkTime();
            if (_actionCooldown >= _timeToAction)
            {
                Gather(resource);
                _actionCooldown = 0;
            }
        }

        private void ConstructBuilding()
        {
            ConstructionBuilding building = _target.GetComponent<ConstructionBuilding>();
            if (!building) return;
            if (_actionCooldown >= _timeToAction)
            {
                Construct(building);
            }
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

        private void Gather(Resource resource)
        {
            if (_currentCarryLoad < _maxCarryLoad)
            {
                _currentCarryLoad += resource.Gather();
                _resourceAmount++;
                return;
            }
            ReturnToHeadquarters();
        }

        private void Construct(ConstructionBuilding building)
        {
            if (building)
            {
                building.AddConstructionProgress();
                _actionCooldown = 0;
                return;
            }
            _target = null;
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

        private void ReturnToHeadquarters()
        {
            Vector3 hDest = _dropOffPoint.transform.position;
            _agent.SetDestination(hDest);
            isDroppingResources = true;
        }
    }
}