using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class BuildAction : UnitAction
    {

        ConstructionBuilding _building;

        public override bool IsTargetValid(GameObject target)
        {
            StopAllCoroutines();
            _target = target;
            _building = _target.GetComponent<ConstructionBuilding>();
            if (!_building || !IsBuildingValid()) return false;
            StartCoroutine(ConstructBuilding());
            return true;
        }

        IEnumerator ConstructBuilding()
        {
            Vector3 construction = _building.transform.position;
            _agent.SetDestination(construction);
            while (DistanceToTarget(construction) > actionRange)
            {
                yield return new WaitForSeconds(1f);
            }
            while (_building)
            {
                Construct();
                yield return new WaitForSeconds(_timeToAction);
            }
            _target = null;
        }

        private void Construct()
        {
            _building.AddConstructionProgress();
        }

        private bool IsBuildingValid()
        {
            if (_building.Player == _unit.PlayerOwner) return true;
            return false;
        }
    } 
}
