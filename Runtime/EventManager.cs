using System.Collections;
using System.Collections.Generic;
using ChickadeeEvents.Debug;
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

        public event System.Action<EventQuery> OnCallEvent;

        public EventManagerData data;

        public float debugEventDelay;
        public List<EventCallInfo> debugLog = new List<EventCallInfo>();

        private void Start()
        {
            if (_current == null)
                _current = this;
            else if (_current != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        public void CallEvent(EventCall call, Object caller)
        {
            EventQuery query = new EventQuery(call.EventName, data.blackboard, call.eventFacts);

            debugLog.Add(new EventCallInfo(call, caller, Time.time));

            OnCallEvent?.Invoke(query);

            foreach(RuleList ruleCollection in data.ruleCollections)
            {
                foreach (Rule rule in ruleCollection.rules)
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
                                            derefedFacts);

                            StartCoroutine(CallEventCoroutine(derefedCall, caller));
                        }
                    }
                }
            }
        }

        IEnumerator CallEventCoroutine(EventCall call, Object caller)
        {
            yield return new WaitForSeconds(debugEventDelay);
            CallEvent(call, caller);
        }

        /// <summary>
        /// Gets the string value of a fact. returns null if not found
        /// </summary>
        public string GetFactVal(string key)
        {
            return data.blackboard.facts.GetValue(key);
        }

        /// <summary>
        /// Sets the string value of a fact. adds it if nonexistant
        /// </summary>
        public void SetFactVal(string key, string value)
        {
            data.blackboard.facts.SetValue(key, value);
        }
    }

}