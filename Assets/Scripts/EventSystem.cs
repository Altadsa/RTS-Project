using System.Collections.Generic;
using UnityEngine;

namespace GEV
{
    public class EventSystem : MonoBehaviour
    {
        [HideInInspector]
        public List<EventListener> listeners = new List<EventListener>();

        private void OnEnable()
        {
            foreach (var listener in listeners)
            {
                listener.Register();
            }
        }

        private void OnDisable()
        {
            foreach (var listener in listeners)
            {
                listener.Unregister();
            }
        }


    }
}
