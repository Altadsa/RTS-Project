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

        private int _walkLayer = (int)Layer.Walkable;

        public void Target(RaycastHit hit)
        {
            _agent.isStopped = false;
            GameObject hitGo = hit.collider.gameObject;
            if (hitGo.layer == _walkLayer)
            {
                MoveToPoint(hit);
            }
            else
            {
                _target = hitGo;
            }
        }

        public void MoveToPoint(RaycastHit hit)
        {
            _target = null;
            Vector3 location = hit.point;
            _agent.SetDestination(location);
        }


        //!hit.collider.GetComponent<EnemyUnit>() && !hit.collider.GetComponent<Resource>() && !hit.collider.GetComponent<ConstructionBuilding>() && !hit.collider.GetComponent<Building>()
    }
}
