using UnityEngine.Events;

namespace ChickadeeEvents
{
    [System.Serializable]
    public class EventTrigger
    {
        public string eventName;
        public UnityEvent<EventQuery> triggers;
    }
}
