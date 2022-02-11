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

        /// <summary>
        /// Sets the value of the fact in the list with the matching key
        ///
        /// If the key doesn't exist yet, it will be created.
        /// 
        /// If value is null, the fact will be removed or simply not added
        /// in the first place.
        /// </summary>
        public static void SetValue(this List<Fact> facts, string key,
                                    string value)
        {
            Fact match = facts.Find(e => e.key == key);
            if(match == null)
            {
                if(value != null)
                    facts.Add(new Fact(key, value));
                return;
            }
            if(value == null)
            {
                facts.Remove(match);
                return;
            }
            match.value = value;
        }

        /// <summary>
        /// Gets the value of the fact with the matching key in the list
        ///
        /// If the key isn't found, null is returned
        /// </summary>
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
