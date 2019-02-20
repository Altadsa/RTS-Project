using UnityEngine;

namespace RTS
{
    public abstract class ActionRelay : MonoBehaviour
    {
        protected RaycastHit _layerHit;
        public delegate void UnitAction(RaycastHit targetHit);
        public event UnitAction AssignAction;

        public void SetAction()
        {
            if (AssignAction == null) { return; }
            AssignAction(_layerHit);
        }

        protected bool Foo()
        {
            return AssignAction == null;
        }

    } 
}
