using System.Collections.Generic;
using System;

namespace ChickadeeEvents
{
    [Serializable]
    public class EventCall
    {
        public string eventName;
        public List<Fact> eventFacts;

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

        public EventCall()
        {
            eventName = "";
            eventFacts = new List<Fact>();
        }

        /// <summary>
        /// Deep copys the other event call
        /// </summary>
        public EventCall(EventCall call)
        {
            eventName = call.eventName;
            eventFacts = new List<Fact>();
            foreach(Fact otherFact in call.eventFacts)
            {
                eventFacts.Add(new Fact(otherFact));
            }
        }

        public EventCall(string eventName, List<Fact> eventFacts)
        {
            this.eventName = eventName;
            this.eventFacts = eventFacts;
        }

        public EventCall(string eventName, string sender = "", string target = "")
        {
            this.eventName = eventName;
            eventFacts = new List<Fact>();
            Sender = sender;
            Target = target;
        }

        public override string ToString()
        {
            return $"{eventName} ({Sender} -> {Target})";
        }
    }
}