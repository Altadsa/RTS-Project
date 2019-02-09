using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System;

namespace RTS
{
    public abstract class UnitAction : MonoBehaviour
    {
        protected NavMeshAgent _agent;
        protected GameObject _target;
        protected float _actionCooldown = 0;
        public float _timeToAction = 2;
        public float actionRange = 5;

        protected Dictionary<Type, ActionKeys> _actionDict = new Dictionary<Type, ActionKeys>
        {
            {typeof(Resource), ActionKeys.Building},
            {typeof(ConstructionBuilding), ActionKeys.Construction},
            {typeof(Building), ActionKeys.Resource}
        };

        protected enum ActionKeys
        {
            Resource,
            Construction,
            Building
        };

        //protected abstract void SetAction(GameObject tar);

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
                //SetAction(_target);
            }
        }

        public void MoveToPoint(RaycastHit hit)
        {
            _target = null;
            Vector3 location = hit.point;
            _agent.SetDestination(location);
        }

    }
}
