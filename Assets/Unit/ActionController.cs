using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class ActionController : MonoBehaviour
    {
        UnitController controller;
        Ray mousePos;
        RaycastHit hit;

        private void Awake()
        {
            controller = GetComponent<UnitController>();
        }


    } 
}
