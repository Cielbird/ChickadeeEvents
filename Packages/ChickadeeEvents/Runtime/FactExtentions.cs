using System.Collections.Generic;

namespace ChickadeeEvents
{
    public static class FactExtentions
    {
        public static string GetValue(this List<Fact> facts, string key)
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

        public static void SetValue(this List<Fact> facts, string key, string value)
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

        public static Fact GetFact(this List<Fact> facts, string key)
        {
            foreach (Fact fact in facts)
            {
                if (fact.key == key)
                    return fact;
            }
            return null;
        }
    }
}
