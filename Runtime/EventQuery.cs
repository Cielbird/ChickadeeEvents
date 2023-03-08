using System.Collections.Generic;
using UnityEngine;

namespace ChickadeeEvents
{
    public class EventQuery
    {
        public string EventName;
        public List<Fact> Facts;


        public EventQuery(string eventName, List<Fact> blackboard, List<Fact> eventFacts)
        {
            this.EventName = eventName;
            Facts = new List<Fact>(blackboard);
            Facts.AddRange(eventFacts);
        }

        public bool MatchesRule(Rule rule)
        {
            if (rule.EventName != EventName)
                return false;

            foreach(Fact criterion in rule.Criteria)
            {
                Fact queryFact = Facts.GetFact(criterion.Key);
                if (queryFact == null ||
                    Deref(criterion.Value) != queryFact.Value)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// if value is a reference (starts with &), then this
        /// returns the value of the referenced fact.
        /// </summary>
        public string Deref(string value)
        {
            if (value == null || value.Length <= 0 || value[0] != '*')
                return value;
            return Facts.GetValue(value.Substring(1));
        }
    }
}
