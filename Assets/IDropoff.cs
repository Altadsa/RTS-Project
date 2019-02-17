using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public interface IDropoff
    {
        Vector3 DropPoint { get; }
        void DropResources(ResourceType type, int amount);
    }
}
