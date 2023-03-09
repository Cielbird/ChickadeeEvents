using System.Collections;
using System.Collections.Generic;
using ChickadeeEvents;
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

        public List<Fact> Facts;

        public List<string> EventNames;

        public List<Rule> Rules;

        private void Awake()
        {
            Facts = new List<Fact>();
            Rules = new List<Rule>();
        }
        // TODO refactor fact-setting and fat-getting. There is code everywhere
        // that acomplishes this objective
        public void SetFact(string key, string value)
        {
            foreach (Fact fact in Facts)
            {
                if (fact.Key == key)
                {
                    fact.Value = value;
                }
            }
            Facts.Add(new Fact(key, value));
        }

        public string GetStringFact(string key)
        {
            foreach (Fact fact in Facts)
            {
                if (fact.Key == key)
                {
                    return fact.Value;
                }
            }
            return null;
        }
    }
}