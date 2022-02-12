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
        public Blackboard blackboard;
        public List<RuleList> ruleCollections;

        private void Awake()
        {
            blackboard = new Blackboard();
            ruleCollections = new List<RuleList>();
            ruleCollections.Add(new RuleList("Main rules"));
        }
    }
}