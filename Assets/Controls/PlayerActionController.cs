using UnityEngine;

namespace RTS
{
    public class PlayerActionController : ActionRelay
    {
        private Layer _currentLayer;

        private GameObject _hitGo;

        private UnitRaycaster _raycaster;

        private void Start()
        {
            _raycaster = GetComponent<UnitRaycaster>();
            _raycaster.UpdateActiveLayer += UpdateActiveLayer;
        }


        private void UpdateActiveLayer(Layer newLayer, RaycastHit layerHit)
        {
            _currentLayer = newLayer;
            _layerHit = layerHit;
            _hitGo = _layerHit.collider.gameObject;
        }
    }
}
