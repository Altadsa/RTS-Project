using UnityEngine.Events;

namespace GEV
{
    [System.Serializable]
    public class EventListener
    {

        public ScriptableEvent _Event;

        public UnityEvent response;

        public void Register()
        {
            _Event.RegisterListener(this);
        }

        public void Unregister()
        {
            _Event.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            response.Invoke();
        }
    }
}
