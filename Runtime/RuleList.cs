using System.Collections;
using System.Collections.Generic;
using ChickadeeEvents;
using UnityEngine;

namespace ChickadeeEvents
{
    public class RuleList : IRuleCollection
    {
        public List<Rule> rules;

        public string Name { get; set; }

        public RuleList(string name)
        {
            rules = new List<Rule>();
            Name = name;
        }

        List<Rule> IRuleCollection.GetRules()
        {
            return rules;
        }
    }
}
