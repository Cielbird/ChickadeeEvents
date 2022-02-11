using System.Collections.Generic;
using UnityEngine;

namespace ChickadeeEvents
{
    public class EventQuery
    {
        public string eventName;
        public List<Fact> facts;


        public EventQuery(string eventName, Blackboard blackboard, List<Fact> eventFacts)
        {
            this.eventName = eventName;
            facts = new List<Fact>(blackboard.facts);
            facts.AddRange(eventFacts);
        }

        public bool MatchesRule(Rule rule)
        {
            if (rule.eventName != eventName)
                return false;

            foreach(Fact criterion in rule.criteria)
            {
                Fact queryFact = GetFact(criterion.key);
                if (queryFact == null ||
                    Deref(criterion.value) != queryFact.value)
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
            return fact.value;
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
            foreach (Fact fact in facts)
            {
                if (fact.key == key)
                    return fact;
            }
            return null;
        }
    }
}
