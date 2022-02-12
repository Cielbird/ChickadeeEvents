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
        [System.NonSerialized]
        public Blackboard blackboard = new Blackboard();
        [System.NonSerialized]
        public List<IRuleCollection> ruleCollections = new();
        [System.NonSerialized]
        public ChickadeeTest test = new ChickadeeTest();

        public List<EventCallInfo> debugLog = new List<EventCallInfo>();
    }
}