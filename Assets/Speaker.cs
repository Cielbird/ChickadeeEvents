using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Speaker : MonoBehaviour
{
    public string name;
    public TextMeshProUGUI text;

    public void Say(string message)
    {
        StartCoroutine(ShowMessage(name + ": "+ message));
    }

    IEnumerator ShowMessage(string message)
    {
        text.text = message;
        yield return new WaitForSeconds(2);
        text.text = name + ": ";
    }
}
