using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace RTS
{
    public class UiRaycast : MonoBehaviour
    {

        public static bool RaycastToUi { get; private set; }

        static EventSystem _eventSystem;
        static PointerEventData _eventData;
        static GraphicRaycaster _Graycaster;

        // Start is called before the first frame update
        void Start()
        {
            _Graycaster = GetComponent<GraphicRaycaster>();
            _eventSystem = GetComponent<EventSystem>();
        }

        public static bool IsRaycastingToUi()
        {
            _eventData = new PointerEventData(_eventSystem);
            _eventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();

            _Graycaster.Raycast(_eventData, results);
            int layermask = 1 << (int)Layer.Ui;
            foreach (RaycastResult result in results)
            {
                int resultLayer = 1 << (int)result.gameObject.layer;
                if (resultLayer== layermask)
                {
                    return true;
                }
            }
            return false;
        }
    } 
}
