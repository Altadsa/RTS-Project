using UnityEngine;

namespace RTS
{
    public interface IUnitAction
    {
        bool IsTargetValid(GameObject target);
    }

}
