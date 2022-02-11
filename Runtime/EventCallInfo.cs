
using UnityEngine;

namespace ChickadeeEvents.Debug
{
    public class EventCallInfo
    {
        public EventCall call;
        public UnityEngine.Object caller;
        public float time;

        public EventCallInfo(EventCall call, Object caller, float time)
        {
            this.call = call;
            this.caller = caller;
            this.time = time;
        }

        public override string ToString()
        {
            return $"[{time}] {call}";
        }
    }
}
