using UnityEngine.AI;
using UnityEngine;

namespace RTS
{
    [RequireComponent(typeof(UnitCombat))]
    public class PlayerUnit : Unit
    {
        UnitSelectionController selectionController;
        UnitCombat unitCombat;

        [HideInInspector]
        public bool isSelected = false;

        private void Start()
        {
            SetupUnit();
        }

        private void SetupUnit()
        {
            selectionController = FindObjectOfType<UnitSelectionController>();
            unitCombat = GetComponent<UnitCombat>();
            agent = GetComponent<NavMeshAgent>();
            if (selectionController)
            {
                selectionController.selectableUnits.Add(gameObject);
            }
        }

        private void Update()
        {
            ActivateProjector();
        }

        private void ActivateProjector()
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

        private void OnActionAssigned(RaycastHit hit)
        { 
            if (!hit.collider.GetComponent<EnemyUnit>())
            {
                Vector3 location = hit.point;
                agent.SetDestination(location);
            }
            else
            {
                unitCombat.Target(hit.collider.gameObject);
            }
        }

        private void SetValidTarget(GameObject _target)
        {
            if (_target.GetComponent<EnemyUnit>())
            {
                unitCombat.Target(_target);
            }
            else
            {

            }
        }

        public void Select()
        {
            isSelected = true;
            selectionController.assignAction += OnActionAssigned;
        }

        public void Deselect()
        {
            isSelected = false;
            selectionController.assignAction -= OnActionAssigned;
        }
    } 
}
