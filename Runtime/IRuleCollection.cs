using System.Collections.Generic;
using ChickadeeEvents;

namespace ChickadeeEvents
{
    public interface IRuleCollection
    {
        public string Name { get; set; }
        public List<Rule> GetRules();
    }
}