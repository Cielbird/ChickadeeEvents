using System;
using System.Collections.Generic;

namespace ChickadeeEvents
{
    [Serializable]
    public class Rule
    {
        /// <summary>
        /// The event that will trigger the reactions
        /// </summary>
        public string EventName;

        /// <summary>
        /// What criteria have to be met
        /// </summary>
        public List<Fact> Criteria;

        /// <summary>
        /// What events will be called if criteria are met
        /// </summary>
        public List<EventCall> Responses;


        public override string ToString()
        {
            return $"{EventName} ({Criteria.Count})";
        }


        public Rule()
        {
            EventName = "untitled_event";
            Criteria = new List<Fact>();
            Responses = new List<EventCall>();
        }

        /// <summary>
        /// Creates a deep copy of otherRule
        /// </summary>
        public Rule(Rule otherRule)
        {
            EventName = otherRule.EventName;
            Criteria = new List<Fact>();
            foreach(Fact c in otherRule.Criteria)
            {
                Criteria.Add(new Fact(c));
            }
            Responses = new List<EventCall>();
            foreach (EventCall c in otherRule.Responses)
            {
                EventCall newCall = new EventCall(c);
                Responses.Add(newCall);
            }
        }
    }

}
