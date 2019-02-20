using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/*
 * A Class that issues an action to the Unit based on the Target Assigned.
 * 
 * */
namespace RTS
{

    public class UnitActionController : MonoBehaviour
    {
        NavMeshAgent _agent;
        GameObject _target;
        float _actionCooldown = 0;
        public float _timeToAction = 2;
        public float actionRange = 5;

        private int _walkLayer = (int)Layer.Walkable;

        List<UnitAction> _unitActions = new List<UnitAction>();

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _target = null;
            _unitActions = GetComponents<UnitAction>().ToList();
        }

        public void Target(RaycastHit hit)
        {
            StartCoroutine(LookForValidAction(hit));
        }

        IEnumerator LookForValidAction(RaycastHit hit)
        {
            GameObject hitGo = hit.collider.gameObject;
            foreach (var action in _unitActions)
            {
                if (action.IsTargetValid(hitGo))
                {
                    yield break;
                }
                yield return new WaitForEndOfFrame();
            }
            MoveToPoint(hit);
        }

        public void MoveToPoint(RaycastHit hit)
        {
            _target = null;
            Vector3 location = hit.point;
            _agent.SetDestination(location);
        }
    } 
}
