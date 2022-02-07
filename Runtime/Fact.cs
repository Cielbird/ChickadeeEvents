using System;

namespace ChickadeeEvents
{
    [Serializable]
    public class Fact
    {
        public string key;
        public string value;

        public Fact()
        {
            key = "";
            value = "";
        }

        /// <summary>
        /// Creates a deep copy of the other fact
        /// </summary>
        public Fact(Fact otherFact)
        {
            key = otherFact.key;
            value = otherFact.value;
        }

        public Fact(string key, string value)
        {
            this.key = key;
            this.value = value;
        }

        public override string ToString()
        {
            return $"{key} : {value}";
        }
    }
}
