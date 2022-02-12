using System.Collections;
using System.Collections.Generic;
using ChickadeeEvents;
using UnityEngine;

namespace ChickadeeEvents
{
    [System.Serializable]
    public class RuleList
    {
        public string name;
        public List<Rule> rules;

        public RuleList(string name)
        {
            rules = new List<Rule>();
            this.name = name;
        }
    }
}
