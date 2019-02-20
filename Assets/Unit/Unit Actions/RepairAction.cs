using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class RepairAction : UnitAction
    {
        Building _buildingToRepair;
        BuildingHealth _buildingCurrentHealth;

        public override bool IsTargetValid(GameObject target)
        {
            Building building = _target.GetComponent<Building>();
            if (!building || !IsBuildingValid()) return false;
            StartCoroutine(RepairBuilding());
            return true;
        }


        IEnumerator RepairBuilding()
        {
            Vector3 buildingPos = _buildingToRepair.transform.position;
            _agent.SetDestination(buildingPos);
            while(DistanceToTarget(buildingPos) > actionRange)
            {
                yield return new WaitForSeconds(1f);
            }
             _buildingCurrentHealth = _buildingToRepair.GetComponent<BuildingHealth>();
            while (_buildingCurrentHealth.NeedsRepaired)
            {
                _buildingCurrentHealth.Repair();
                yield return new WaitForSeconds(_timeToAction);
            }
            _target = null;
        }

        private bool IsBuildingValid()
        {
            if (_buildingToRepair.Player == _unit.PlayerOwner) return true;
            return false;
        }

    } 
}
