using System;
using System.Collections.Generic;
using UnityEngine;

namespace ChickadeeEvents
{
    /// <summary>
    /// MonoBehaviour that allows for interaction between the event system and
    /// other Monobehaviours. When an event is called, if the
    /// EventListener's GameObject is the sender, the listener will
    /// trigger any linked Unity events.
    /// </summary>
    public class EventListener : MonoBehaviour
    {
        public List<EventTrigger> EventTriggers;

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
            if (query.Facts.GetValue("sender") != gameObject.name)
                return;

            foreach (EventTrigger trigger in EventTriggers)
            {
                if (trigger.EventName.Equals(query.EventName))
                {
                    trigger.Triggers.Invoke(query);
                    return;
                }
            }
        }
    }
}
