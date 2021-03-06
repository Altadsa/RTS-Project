﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    [RequireComponent(typeof(SelectionController))]
    [RequireComponent(typeof(ActionRelay))]
    public class UnitInputController : MonoBehaviour
    {
        private static UnitInputController _instance;
        private static readonly object padlock = new object();
        public static UnitInputController Instance
        {
            get
            {
                lock(padlock)
                {
                    if (!_instance) _instance = FindObjectOfType<UnitInputController>();
                    return _instance;
                }
            }
        }

        private UnitRaycaster _raycaster;
        Vector3 _mousePos1, _mousePos2;

        private PlayerSelectionController _selectionController;
        private ActionRelay _actionController;

        private void Start()
        {
            _selectionController = GetComponent<PlayerSelectionController>();
            _actionController = GetComponent<ActionRelay>();
        }

        private void Update()
        {
            SelectUnits();
        }

        private void SelectUnits()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _mousePos1 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                _selectionController.SelectionState();  
            }

            if (Input.GetMouseButtonUp(0))
            {
                _mousePos2 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                if (_mousePos1 != _mousePos2)
                {
                    _selectionController.SelectUnitsInBox(DrawRect());
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                _actionController.SetAction();
            }
        }
        
        private Rect DrawRect()
        {
            float width = _mousePos2.x - _mousePos1.x;
            float height = _mousePos2.y - _mousePos1.y;
            return new Rect(_mousePos1.x, _mousePos1.y, width, height);
        }
        
    }
}