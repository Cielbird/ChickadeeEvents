using System;

namespace ChickadeeEvents
{
    [Serializable]
    public class Fact
    {
        public string Key;
        public string Value;

        public Fact()
        {
            Key = "";
            Value = "";
        }

        /// <summary>
        /// Creates a deep copy of the other fact
        /// </summary>
        public Fact(Fact otherFact)
        {
            Key = otherFact.Key;
            Value = otherFact.Value;
        }

        public Fact(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        public override string ToString()
        {
            return $"{Key} : {Value}";
        }

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Fact f = (Fact)obj;
                return Key == f.Key && Value == f.Value;
            }
        }

        public override int GetHashCode()
        {
            return (Key+":"+Value).GetHashCode();
        }
    }
}
