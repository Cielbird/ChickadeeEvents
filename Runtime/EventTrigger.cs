using UnityEngine.Events;

namespace ChickadeeEvents
{
    [System.Serializable]
    public class EventTrigger
    {
        public string EventName;
        public UnityEvent<EventQuery> Triggers;
    }
}
