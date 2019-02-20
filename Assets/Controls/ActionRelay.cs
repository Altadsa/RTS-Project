using UnityEngine;

namespace RTS
{
    public abstract class ActionRelay : MonoBehaviour
    {
        protected RaycastHit _layerHit;
        public delegate void UnitAction(RaycastHit targetHit);
        public event UnitAction assignAction;

        public void AssignAction()
        {
            if (assignAction == null) { return; }
            assignAction(_layerHit);
        }

        protected bool Foo()
        {
            return assignAction == null;
        }

    } 
}
