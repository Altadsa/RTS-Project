using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace RTS
{
    public class GatherAction : UnitAction
    {
        Resource _resource;
        Transform _resourceParent;
        ResourceType _resourceToWork;
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
             _resourceParent = resource.transform.parent;
            _agent.isStopped = false;
            _agent.SetDestination(_gatherPoint);
            while (DistanceToTarget(_gatherPoint) > actionRange)
            {
                yield return new WaitForSeconds(0.5f);
            }
            _timeToAction = _resource.WorkTime;
            while (_currentCarryLoad < _maxCarryLoad)
            {
                yield return new WaitForSeconds(_timeToAction);
                if (!_resource) { FindNewResource(); yield break; }
                Gather(resource);
                _actionCooldown = 0;
                _agent.isStopped = true;
            }
            StartCoroutine(DropOffResources());
        }

        private void FindNewResource()
        {
            Debug.Log(_resourceParent);
            _resource = FindClosestResource();
            _gatherPoint = _resource.GatherPoint;
            StopAllCoroutines();
            StartCoroutine(GatherResource(_resource));
        }

        private Resource FindClosestResource()
        {
            var closest = _resourceParent.GetChild(0);
            foreach (Transform resource in _resourceParent)
            {
                float currentDistance = Vector3.Distance(transform.position, closest.position);
                float checkdistance = Vector3.Distance(transform.position, resource.position);
                if (checkdistance < currentDistance) { closest = resource; }
            }
            Resource newResource = closest.GetComponent<Resource>();
            return newResource;
        }

        IEnumerator DropOffResources()
        {
            _agent.isStopped = false;
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
