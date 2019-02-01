using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public static class ResourceData
    {
        public static int Gold { get; private set; }
        public static int Timber { get; private set; }
        public static int Food { get; private set; }

        public static void AmendGold(int amount)
        {
            Gold += amount;
        }

        public static void AmendTimber(int amount)
        {
            Timber += amount;
        }

        public static void AmendFood(int amount)
        {
            Food += amount;
        }
    }
}