using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;


namespace ChickadeeEvents
{
    [CustomEditor(typeof(EventManager))]
    public class EventManagerInspector : Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            // Create a new VisualElement to be the root of our inspector UI
            VisualElement myInspector = new VisualElement();

            // Add a simple label
            Button button = new Button(() =>
            {
                EventEditor.ShowWindow();
            });
            button.text = "Open event editor";
            myInspector.Add(button);

            // Return the finished inspector UI
            return myInspector;
        }
    }
}
