
using UnityEngine;

namespace ChickadeeEvents.Debug
{
    public class EventCallInfo
    {
        public EventCall call;
        public UnityEngine.Object sender;
        public float time;

        public EventCallInfo(EventCall call, Object sender, float time)
        {
            this.call = call;
            this.sender = sender;
            this.time = time;
        }
    }
}
