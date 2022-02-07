using System.Collections.Generic;
using UnityEngine;

namespace ChickadeeEvents
{
    public class EventListener : MonoBehaviour
    {
        public string listenerName;
        public List<EventTrigger> eventTriggers;

        private void OnEnable()
        {
            if (EventManager.Current == null)
                return;

            EventManager.Current.OnCallEvent += OnCallEvent;
        }

        private void OnDisable()
        {
            if (EventManager.Current == null)
                return;

            EventManager.Current.OnCallEvent -= OnCallEvent;
        }

        void OnCallEvent(EventQuery query)
        {
            if (query.GetVal("sender") != listenerName)
                return;

            foreach (EventTrigger trigger in eventTriggers)
            {
                if (trigger.eventName.Equals(query.eventName))
                {
                    trigger.triggers.Invoke(query);
                    return;
                }
            }
        }
    }
}
