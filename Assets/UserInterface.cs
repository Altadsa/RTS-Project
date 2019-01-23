﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public static class UserInterface
    {
        public static void ClearBuildMenu()
        {
            GameObject buildMenu = GameObject.FindGameObjectWithTag("Build Panel");
            if (buildMenu)
            {
                foreach (Transform child in buildMenu.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
            }
        }
    } 
}