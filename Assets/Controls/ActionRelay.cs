using UnityEngine;

namespace RTS
{
    public class ActionRelay : MonoBehaviour
    {
        private Layer _currentLayer;
        private RaycastHit _layerHit;
        private GameObject _hitGo;

        private UnitRaycaster _raycaster;

        public delegate void UnitAction(RaycastHit targetHit);
        public event UnitAction assignAction;
        
        private void Awake()
        {
            _raycaster = GetComponent<UnitRaycaster>();
            _raycaster.UpdateActiveLayer += UpdateActiveLayer;
        }

        public void AssignAction()
        {
            if (assignAction == null) { return; }
            assignAction(_layerHit);
        }
        
        private void UpdateActiveLayer(Layer newLayer, RaycastHit layerHit)
        {
            _currentLayer = newLayer;
            _layerHit = layerHit;
            _hitGo = _layerHit.collider.gameObject;
        }

    } 
}
