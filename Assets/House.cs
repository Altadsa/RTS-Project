using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class House : MonoBehaviour
    {

        [SerializeField] int _foodValue;

        void Start()
        {
            ResourceData.AmendMaxFood(_foodValue);
        }

        private void OnDestroy()
        {
            ResourceData.AmendMaxFood(-_foodValue);
        }
    } 
}
