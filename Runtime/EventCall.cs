using System.Collections.Generic;
using UnityEngine;

namespace ChickadeeEvents
{
    [System.Serializable]
    public class EventCall
    {
        public List<Fact> eventFacts;

        public string EventName
        {
            get
            {
                return eventFacts.GetValue("event_name");
            }
            set
            {
                eventFacts.SetValue("event_name", value);
            }
        }
        public string Sender
        {
            get
            {
                return eventFacts.GetValue("sender");
            }
            set
            {
                eventFacts.SetValue("sender", value);
            }
        }
        public string Target
        {
            get
            {
                return eventFacts.GetValue("target");
            }
            set
            {
                eventFacts.SetValue("target", value);
            }
        }

        public Object caller;

        public EventCall()
        {
            eventFacts = new List<Fact>();
            EventName = "untitled_event";
        }

        /// <summary>
        /// Deep copys the other event call
        /// </summary>
        public EventCall(EventCall call)
        {
            eventFacts = new List<Fact>();
            foreach(Fact otherFact in call.eventFacts)
            {
                eventFacts.Add(new Fact(otherFact));
            }
        }

        public EventCall(string eventName, List<Fact> otherEventFacts = null,
                         Object caller = null)
        {
            eventFacts = new List<Fact>();
            EventName = eventName;
            if(otherEventFacts != null)
            {
                foreach (Fact f in otherEventFacts)
                    eventFacts.Add(f);
            }
            this.caller = caller;
        }

        public EventCall(string eventName, List<Fact> otherEventFacts = null,
                         string sender = null, string target = null,
                         Object caller = null)
                            : this(eventName, otherEventFacts, caller)
        {
            Sender = sender;
            Target = target;
        }

        public EventCall(string eventName, string sender = null,
                         string target = null,
                         Object caller = null)
                            : this(eventName, null, sender, target, caller) { }


        public override string ToString()
        {
            return $"{EventName} ({Sender} -> {Target})";
        }
    }
}