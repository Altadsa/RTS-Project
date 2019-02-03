using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class PlayerManager : MonoBehaviour
    {

        void Start()
        {
            ResourceData.AmendFood(500); ResourceData.AmendGold(500); ResourceData.AmendTimber(500);
        }

    } 
}
