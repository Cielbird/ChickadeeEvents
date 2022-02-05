using System.Collections;
using System.Collections.Generic;
using ChickadeeEvents;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Speaker speaker;

    public GameObject responseUI;
    public string dialogueTarget;


    private void Start()
    {
        EventManager.Current.CallEvent(new EventCall("on_see", "bob", "player"));
    }

    public void Talk(EventQuery query)
    {
        speaker.Say(query.facts.GetValue("message"));
    }

    public void GetResponse(EventQuery query)
    {
        dialogueTarget = query.facts.GetValue("target");
    }

    public void SendResponse(string response)
    {
        List<Fact> respFacts = new List<Fact>();
        respFacts.SetValue("sender", "player");
        respFacts.SetValue("target", dialogueTarget);
        respFacts.SetValue("message", response);
        EventManager.Current.CallEvent(new EventCall("on_talk", respFacts));
    }
}
