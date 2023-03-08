using System.Collections.Generic;
using UnityEngine;

namespace ChickadeeEvents
{
    /// <summary>
    /// Class that contains an event call in the form of a fact list.
    /// </summary>
    [System.Serializable]
    public class EventCall
    {
        public List<Fact> EventFacts;

        /// <summary>
        /// Gets the value of the "event_name" fact.
        /// </summary>
        public string EventName
        {
            get
            {
                return EventFacts.GetValue("event_name");
            }
            set
            {
                EventFacts.SetValue("event_name", value);
            }
        }

        /// <summary>
        /// Gets the value of the "sender" fact. The sender is usually a
        /// unity Object's name.
        /// </summary>
        public string Sender
        {
            get
            {
                return EventFacts.GetValue("sender");
            }
            set
            {
                EventFacts.SetValue("sender", value);
            }
        }

        /// <summary>
        /// Gets the value of the "target" fact. The target is usually a
        /// unity Object's name.
        /// </summary>
        public string Target
        {
            get
            {
                return EventFacts.GetValue("target");
            }
            set
            {
                EventFacts.SetValue("target", value);
            }
        }

        /// <summary>
        /// Creates a new event call with event name "untitled_event"
        /// </summary>
        public EventCall()
        {
            EventFacts = new List<Fact>();
            EventName = "untitled_event";
        }

        /// <summary>
        /// Deep copies the other event call
        /// </summary>
        public EventCall(EventCall call)
        {
            EventFacts = new List<Fact>();
            foreach(Fact otherFact in call.EventFacts)
            {
                EventFacts.Add(new Fact(otherFact));
            }
        }

        public EventCall(string eventName, List<Fact> otherEventFacts = null)
        {
            EventFacts = new List<Fact>();
            EventName = eventName;
            if(otherEventFacts != null)
            {
                foreach (Fact f in otherEventFacts)
                    EventFacts.Add(f);
            }
        }

        public EventCall(string eventName, List<Fact> otherEventFacts = null,
                         string sender = null, string target = null)
                            : this(eventName, otherEventFacts)
        {
            Sender = sender;
            Target = target;
        }

        public EventCall(string eventName, string sender = null,
                         string target = null)
                            : this(eventName, null, sender, target) { }

        /// <summary>
        /// Displays the event call's name, sender, and target.
        /// </summary>
        public override string ToString()
        {
            return $"{EventName} ({Sender} -> {Target})";
        }
    }
}