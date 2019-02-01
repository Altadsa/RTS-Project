using System.Collections.Generic;
using UnityEngine;

namespace GEV
{
    [CreateAssetMenu(menuName = "GEV/Scriptable Event")]
    public class ScriptableEvent : ScriptableObject
    {
        List<EventListener> eventListeners = new List<EventListener>();

        public void RegisterListener(EventListener listener)
        {
            if (!eventListeners.Contains(listener))
            {
                eventListeners.Add(listener);
            }
        }

        public void UnregisterListener(EventListener listener)
        {
            if (eventListeners.Contains(listener))
            {
                eventListeners.Remove(listener);
            }
        }

        public void Raise()
        {
            foreach (EventListener listener in eventListeners)
            {
                listener.OnEventRaised();
            }
        }

    }
}
