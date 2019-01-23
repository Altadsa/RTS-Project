using UnityEngine.AI;
using UnityEngine;

namespace RTS
{
    public class PlayerUnit : Unit
    {
        UnitController controller;

        [HideInInspector]
        public bool isSelected = false;

        private void Start()
        {
            controller = FindObjectOfType<UnitController>();
            agent = GetComponent<NavMeshAgent>();
            if (controller)
            {
                controller.selectableUnits.Add(gameObject);
            }
        }

        private void Update()
        {
            if (isSelected)
            {
                GetComponentInChildren<Projector>().enabled = true;
            }
            else
            {
                GetComponentInChildren<Projector>().enabled = false;
            }
        }

        private void OnDestroy()
        {
            if (controller)
            {
                controller.onRightMbClicked -= OnRightMbClicked;
                controller.onAttackTargetFound -= Target;
            }
        }

        public void Target(GameObject _target)
        {
            GetComponent<UnitCombat>().Target(_target);
        }

        public void OnRightMbClicked(Vector3 dest)
        {
            agent.SetDestination(dest);
        }
    } 
}
