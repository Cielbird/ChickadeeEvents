using System.Collections;
using System.Collections.Generic;
using ChickadeeEvents;
using ChickadeeEvents.Debug;
using UnityEngine;

/// <summary>
/// A scriptable object that holds all the info for rules and facts that will
/// be managed by the EventManager
/// </summary>
namespace ChickadeeEvents
{
    [CreateAssetMenu(fileName = "EventManagerData", menuName = "ChickadeeEvents/EventManagerData", order = 1)]
    public class EventManagerData : ScriptableObject
    {

        public List<Fact> facts;

        [System.Serializable]
        public class EventName
        {
            public string name;
        }
        public List<EventName> eventNames;

        public List<Rule> rules;

        private void Awake()
        {
            facts = new List<Fact>();
            rules = new List<Rule>();
        }

        public void SetFact(string key, string value)
        {
            foreach (Fact fact in facts)
            {
                if (fact.key == key)
                {
                    fact.value = value;
                }
            }
            facts.Add(new Fact(key, value));
        }

        public string GetStringFact(string key)
        {
            foreach (Fact fact in facts)
            {
                if (fact.key == key)
                {
                    return fact.value;
                }
            }
            return null;
        }
    }
}