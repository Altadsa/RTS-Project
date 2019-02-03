using UnityEngine.AI;
using UnityEngine;

namespace RTS
{
    [RequireComponent(typeof(UnitAction))]
    public class PlayerUnit : Unit
    {
        SelectionController selectionController;
        private UnitActionController _actionController;
        UnitAction _unitAction;

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
            _unitAction = GetComponent<UnitAction>();
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
            _unitAction.Target(hit);
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
