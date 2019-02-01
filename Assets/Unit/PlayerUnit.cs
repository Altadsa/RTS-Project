using UnityEngine.AI;
using UnityEngine;

namespace RTS
{
    [RequireComponent(typeof(UnitCombat))]
    public class PlayerUnit : Unit
    {
        SelectionController selectionController;
        private UnitActionController _actionController;
        UnitCombat unitCombat;

        [HideInInspector]
        public bool isSelected = false;

        private void Start()
        {
            SetupUnit();
        }

        private void SetupUnit()
        {
            selectionController = FindObjectOfType<SelectionController>();
            _actionController = FindObjectOfType<UnitActionController>();
            unitCombat = GetComponent<UnitCombat>();
            agent = GetComponent<NavMeshAgent>();
            if (selectionController)
            {
                selectionController.selectableUnits.Add(gameObject);
            }
        }

        private void OnDestroy()
        {
            selectionController.selectableUnits.Remove(gameObject);
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
            if (!hit.collider.GetComponent<EnemyUnit>() && !hit.collider.GetComponent<Resource>())
            {
                Vector3 location = hit.point;
                unitCombat.Target(null);
                agent.SetDestination(location);
            }
            else if (GetComponent<UnitWork>() && hit.collider.GetComponent<Resource>())
            {
                GetComponent<UnitWork>().MineResource(hit.collider.gameObject);
            }
            else
            {
                unitCombat.Target(hit.collider.gameObject);
            }
        }

        public void Select()
        {
            isSelected = true;
            _actionController.assignAction += OnActionAssigned;
        }

        public void Deselect()
        {
            isSelected = false;
            _actionController.assignAction -= OnActionAssigned;
        }
    } 
}
