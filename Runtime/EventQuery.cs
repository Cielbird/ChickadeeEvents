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
                Fact queryFact = GetFact(criterion.Key);
                if (queryFact == null ||
                    Deref(criterion.Value) != queryFact.Value)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Gets the string value of a fact. returns null if not found
        /// </summary>
        public string GetVal(string key)
        {
            Fact fact = GetFact(key);
            if (fact == null)
            {
                UnityEngine.Debug.LogWarning($"Fact with key \"{key}\" not found!");
                return null;
            }
            return fact.Value;
        }

        /// <summary>
        /// if value is a reference (starts with *), then this
        /// returns the value of the fact referenced.
        /// </summary>
        public string Deref(string value)
        {
            if (value == null || value.Length <= 0 || value[0] != '*')
                return value;

            return GetVal(value.Substring(1));
        }

        public Fact GetFact(string key)
        {
            foreach (Fact fact in Facts)
            {
                if (fact.Key == key)
                    return fact;
            }
            return null;
        }
    }
}
