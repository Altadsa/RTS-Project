﻿using UnityEngine;
using UnityEngine.AI;
using System.Linq;

namespace RTS
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(UnitHealth))]
    public class Unit : MonoBehaviour, IDamageable
    {
        #region Fields
        SelectionController _selectionController;
        ActionRelay _actionController;
        UnitActionController _unitActionController;
        Projector _selectionProjector;

        [HideInInspector]
        public bool _isSelected { get; private set; } = false;
        #endregion

        #region Unity Lifecycle
        private void Start()
        {
            //SetupUnit();
            //Deselect();
        }

        private void OnDestroy()
        {
            if (!_selectionController || !_actionController) return;
            _selectionController._selectableUnits.Remove(gameObject);
            _isSelected = false;
            ToggleActionAssignment();
        }

        #endregion

        #region Public Functions
        public void TakeDamage(float damage)
        {
            GetComponentInChildren<UnitHealth>().TakeDamage(damage);
        }

        public void SetPlayerOwner(PlayerInformation player)
        {
            gameObject.GetComponent<Player>()._player = player;
            SetupUnit();
        }

        public PlayerInformation PlayerOwner
        {
            get
            {
                if (!GetComponent<Player>())
                    gameObject.AddComponent<Player>()._player = GameManager.Default;
                return GetComponent<Player>()._player;
            }
        }

        public void Select()
        {
            _isSelected = true;
            ToggleActionAssignment();
            ToggleProjector();
        }

        public void Deselect()
        {
            _isSelected = false;
            ToggleActionAssignment();
            ToggleProjector();
        }
        #endregion

        #region Private Functions
        private void SetupUnit()
        {
            _selectionController = FindObjectsOfType<SelectionController>()
                .Where(s => s._player == PlayerOwner)
                .FirstOrDefault();
            if (!_selectionController) return;
            _selectionController._selectableUnits.Add(gameObject);
            _actionController = _selectionController.GetComponent<ActionRelay>();
            _unitActionController = GetComponent<UnitActionController>();
            _selectionProjector = GetComponentInChildren<Projector>();
        }

        private void ToggleProjector()
        {
            _selectionProjector.enabled = _isSelected;
        }

        private void OnActionAssigned(RaycastHit hit)
        {
            _unitActionController.Target(hit);
        }

        private void ToggleActionAssignment()
        {
            if (_isSelected)
                _actionController.assignAction += OnActionAssigned;
            else
                _actionController.assignAction -= OnActionAssigned;
        }
        #endregion

        private void OnDrawGizmos()
        {
            if (PlayerOwner == null) return;
            Gizmos.color = PlayerOwner._color;
            Gizmos.DrawWireSphere(transform.position, 1f);
        }

    }
}
