using UnityEngine.AI;
using UnityEngine;

namespace RTS
{
    [RequireComponent(typeof(UnitAction))]
    public class PlayerUnit : Unit
    {
        SelectionController _selectionController;
        private ActionRelay _actionController;
        UnitAction _unitAction;

        [HideInInspector]
        public bool isSelected = false;

        private void Start()
        {
            SetupUnit();
        }

        private void SetupUnit()
        {
            _selectionController = FindObjectOfType<SelectionController>();
            _actionController = FindObjectOfType<ActionRelay>();
            _unitAction = GetComponent<UnitAction>();
            agent = GetComponent<NavMeshAgent>();
            if (_selectionController)
            {
                _selectionController._selectableUnits.Add(gameObject);
            }
        }

        private void OnDestroy()
        {
            _selectionController._selectableUnits.Remove(gameObject);
            Deselect();
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

        public void ActionMenu(UnitAction uAction)
        {

        }
    } 
}
