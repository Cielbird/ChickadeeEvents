using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChickadeeEvents
{
    /// <summary>
    /// Singleton monobehaviour that contains the EventManagerData for the
    /// scene. Provides the interface for other scripts to access the event
    /// system.
    /// </summary>
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
                }
                return _current;
            }
        }

        /// <summary>
        /// Called whenever a Chickadee Event is called.
        /// </summary>
        public event System.Action<EventQuery> OnCallEvent;

        public EventManagerData Data;

        public float EventDelay;

        public List<string> Log = new List<string>();

        private void Start()
        {
            if (_current == null)
                _current = this;
            else if (_current != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Calls an event
        /// </summary>
        /// <param name="call">The event to call</param>
        /// <param name="caller">The object that is calling this event</param>
        public void CallEvent(EventCall call, Object caller)
        {
            EventQuery query = new EventQuery(call.EventName, Data.Facts, call.EventFacts);

            // log
            Log.Add($"[{Time.time}] {call.EventName} ({call.Sender} -> {call.Target})");
            if (Log.Count > 30)
                Log.RemoveAt(0);

            OnCallEvent?.Invoke(query);

            // call subsequent events according to rules
            foreach (Rule rule in Data.Rules)
            {
                if (query.MatchesRule(rule))
                {
                    foreach (var response in rule.Responses)
                    {
                        List<Fact> derefedFacts = new List<Fact>();
                        foreach (Fact f in response.EventFacts)
                        {
                            derefedFacts.Add(new Fact(f.Key, query.Deref(f.Value)));
                        }
                        EventCall derefedCall = new EventCall(
                                        query.Deref(response.EventName),
                                        derefedFacts);

                        StartCoroutine(CallEventCoroutine(derefedCall, caller));
                    }
                }
            }
        }

        IEnumerator CallEventCoroutine(EventCall call, Object caller)
        {
            yield return new WaitForSeconds(EventDelay);
            CallEvent(call, caller);
        }

        /// <summary>
        /// Gets the string value of a fact. returns null if not found
        /// </summary>
        public string GetFactVal(string key)
        {
            return Data.Facts.GetValue(key);
        }

        /// <summary>
        /// Sets the string value of a fact. adds it if nonexistant
        /// </summary>
        public void SetFactVal(string key, string value)
        {
            Data.Facts.SetValue(key, value);
        }

        public List<string> GetFactNames()
        {
            return Data.Facts.ConvertAll(e => e.Key);
        }
    }

}