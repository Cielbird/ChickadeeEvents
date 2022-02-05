using System.Collections;
using System.Collections.Generic;
using ChickadeeEvents;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Speaker speaker;


    public void Talk(EventQuery query)
    {
        speaker.Say(query.facts.GetValue("message"));
    }
}
