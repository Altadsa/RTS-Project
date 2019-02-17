using UnityEngine;
using UnityEngine.AI;

namespace RTS
{
    public abstract class UnitAction : MonoBehaviour, IUnitAction
    {
        protected NavMeshAgent _agent;
        protected GameObject _target;
        protected float _actionCooldown = 0;
        protected float _timeToAction = 2;
        protected float actionRange = 5;

        //private int _walkLayer = (int)Layer.Walkable;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        public abstract bool IsTargetValid(GameObject target);

        protected float DistanceToTarget(Vector3 targetPos)
        {
            Vector3 myPos = transform.position;
            return Vector3.Distance(myPos, targetPos);
        }

        protected void MoveToTarget()
        {
            Vector3 targetPos = _target.transform.position;
            _agent.SetDestination(targetPos);
        }

    }
}
