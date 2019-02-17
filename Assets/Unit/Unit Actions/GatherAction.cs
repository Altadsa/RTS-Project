using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RTS
{
    public class GatherAction : UnitAction
    {
        Resource _resource;
        ResourceType _resourceToWork;
        bool _isDroppingResources = false;
        Vector3 _gatherPoint;
        int _resourceAmount = 0;
        float _currentCarryLoad = 0, _maxCarryLoad = 5;
        IDropoff _dropoff;

        public override bool IsTargetValid(GameObject target)
        {
            StopAllCoroutines();
            _target = target;
            _resource = _target.GetComponent<Resource>();
            if (!_resource) return false;
            AssignResourceToWork();
            return true;
        }

        private void AssignResourceToWork()
        {
            _gatherPoint = _resource.GatherPoint;
            _resourceToWork = _resource.ResourceType();
            _dropoff = FindDropOffForResource(_resourceToWork);
            StartCoroutine(GatherResource(_resource));
        }

        private IDropoff FindDropOffForResource(ResourceType resourceToWork)
        {
            IDropoff dropoff;
            switch (resourceToWork)
            {
                case ResourceType.Timber:
                    dropoff = ClosestBuilding<LumberMill>();
                    if (dropoff != null) return dropoff;                  
                    break;
            }
            return ClosestBuilding<Headquarters>();
        }

        private IDropoff ClosestBuilding<T>() where T : Component
        {
            var buildings = FindObjectsOfType<T>().OfType<IDropoff>();
            if (buildings.Count() <= 0) return _dropoff;
            var closest = buildings.First();
            foreach (var building in buildings)
            {
                float currentDistance = Vector3.Distance(transform.position, closest.DropPoint);
                float checkdistance = Vector3.Distance(transform.position, building.DropPoint);
                if (checkdistance < currentDistance) { closest = building; }
            }
            return closest;
        }

        IEnumerator GatherResource(Resource resource)
        {
            _agent.SetDestination(_gatherPoint);
            while (DistanceToTarget(_gatherPoint) > actionRange)
            {
                yield return new WaitForSeconds(0.5f);
            }
            while (_currentCarryLoad < _maxCarryLoad)
            {
                yield return new WaitForSeconds(_timeToAction);
                if (!_resource) yield break;
                Gather(resource);
                _actionCooldown = 0; 
            }
            StartCoroutine(DropOffResources());
        }

        IEnumerator DropOffResources()
        {
            _agent.SetDestination(_dropoff.DropPoint);
            while (DistanceToTarget(_dropoff.DropPoint) > actionRange)
            {
                yield return new WaitForSeconds(0.5f);
            }
            DropResourcesAndReturnToWork();
        }

        private void DropResourcesAndReturnToWork()
        {
            _dropoff.DropResources(_resourceToWork, _resourceAmount);
            ReturnToWork();
        }

        private void ReturnToWork()
        {
            _resourceAmount = 0;
            _currentCarryLoad = 0;
            _isDroppingResources = false;
            StartCoroutine(GatherResource(_resource));
        }

        private void Gather(Resource resource)
        {
            _currentCarryLoad += resource.Gather();
            _resourceAmount++;
            return;
        }




    } 
}
