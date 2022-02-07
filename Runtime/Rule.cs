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
        public string eventName;

        /// <summary>
        /// What criteria have to be met
        /// </summary>
        public List<Fact> criteria;

        /// <summary>
        /// What events will be called if criteria are met
        /// </summary>
        public List<EventCall> responses;


        public override string ToString()
        {
            return $"{eventName} ({criteria.Count})";
        }


        public Rule()
        {
            eventName = "";
            criteria = new List<Fact>();
            responses = new List<EventCall>();
        }

        /// <summary>
        /// Creates a deep copy of otherRule
        /// </summary>
        public Rule(Rule otherRule)
        {
            eventName = otherRule.eventName;
            criteria = new List<Fact>();
            foreach(Fact c in otherRule.criteria)
            {
                criteria.Add(new Fact(c));
            }
            responses = new List<EventCall>();
            foreach (EventCall c in otherRule.responses)
            {
                EventCall newCall = new EventCall(c);
                responses.Add(newCall);
            }
        }
    }

}
