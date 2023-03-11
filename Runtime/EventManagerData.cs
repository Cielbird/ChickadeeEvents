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
    }
}