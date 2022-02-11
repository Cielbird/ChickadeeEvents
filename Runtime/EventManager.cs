using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChickadeeEvents
{
    public class EventManager : MonoBehaviour
    {
        static EventManager _current;
        public static EventManager Current
        {
            get
            {
                if (_current == null)
                {
                    _current = FindObjectOfType<EventManager>();
                    if (_current == null)
                        UnityEngine.Debug.LogWarning("EventManager not found in scene!");
                }
                return _current;
            }
        }

        public event Action<EventQuery> OnCallEvent;

        [HideInInspector]
        public Blackboard blackboard = new Blackboard();
        [HideInInspector]
        public List<Rule> rules = new List<Rule>();

        public float debugEventDelay;
        public List<EventCall> debugLog = new List<EventCall>();

        private void Start()
        {
            if (_current == null)
                _current = this;
            else if (_current != this)
                Destroy(gameObject);
        }

        public void CallEvent(EventCall call)
        {
            EventQuery query = new EventQuery(call.EventName, blackboard, call.eventFacts);

            debugLog.Add(call);

            OnCallEvent?.Invoke(query);

            foreach (Rule rule in rules)
            {
                if (query.MatchesRule(rule))
                {
                    // responses
                    foreach (var response in rule.responses)
                    {
                        List<Fact> derefedFacts = new List<Fact>();
                        foreach (Fact f in response.eventFacts)
                        {
                            derefedFacts.Add(new Fact(f.key, query.Deref(f.value)));
                        }
                        EventCall derefedCall = new EventCall(
                                        query.Deref(response.EventName),
                                        derefedFacts, this);

                        StartCoroutine(CallEventCoroutine(derefedCall));
                    }
                }
            }
        }

        IEnumerator CallEventCoroutine(EventCall call)
        {
            yield return new WaitForSeconds(debugEventDelay);
            CallEvent(call);
        }

        /// <summary>
        /// Gets the string value of a fact. returns null if not found
        /// </summary>
        public string GetFactVal(string key)
        {
            return blackboard.facts.GetValue(key);
        }

        /// <summary>
        /// Sets the string value of a fact. adds it if nonexistant
        /// </summary>
        public void SetFactVal(string key, string value)
        {
            blackboard.facts.SetValue(key, value);
        }
    }

}