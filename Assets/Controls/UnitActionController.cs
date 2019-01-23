using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class UnitActionController : MonoBehaviour
    {
        Layer currentLayer;
        GameObject layerObject;



        private void Awake()
        {
            
        }

        public void SelectAction()
        {
            //if (selectedUnits.Count <= 0) return;
            //if (Input.GetMouseButtonDown(1))
            //{
            //    RaycastForSelection(actionLayers);
            //}
        }

        private void MoveUnits()
        {

            //Call the Assign Action with the gameobject hit by the ray.
        }


    } 
}
