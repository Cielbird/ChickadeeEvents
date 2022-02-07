using System;
using System.Collections.Generic;


namespace ChickadeeEvents
{
    [Serializable]
    public class Blackboard
    {
        public List<Fact> facts;

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