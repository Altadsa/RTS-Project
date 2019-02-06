using UnityEngine;
using UnityEngine.AI;

namespace RTS
{
    public abstract class UnitAction : MonoBehaviour
    {
        protected NavMeshAgent _agent;
        protected GameObject _target;
        protected float _actionCooldown = 0;
        public float _timeToAction = 2;
        public float actionRange = 5;

        public void Target(RaycastHit hit)
        {
            _agent.isStopped = false;
            if (!hit.collider.GetComponent<EnemyUnit>() && !hit.collider.GetComponent<Resource>() && !hit.collider.GetComponent<ConstructionBuilding>() && !hit.collider.GetComponent<Building>())
            {
                _target = null;
                Vector3 location = hit.point;
                _agent.SetDestination(location);
            }
            else
            {
                _target = hit.collider.gameObject;
            }
        }

    } 
}
