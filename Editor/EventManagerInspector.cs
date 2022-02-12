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
            VisualElement eventInspector = new VisualElement();

            var dataField = new PropertyField(serializedObject.FindProperty("data"));
            eventInspector.Add(dataField);

            // Add a simple label
            Button button = new Button(() =>
            {
                EventEditor.SetUpWindow((EventManagerData)serializedObject.FindProperty("data").objectReferenceValue);
            });
            button.text = "Open event editor";
            eventInspector.Add(button);

            // Return the finished inspector UI
            return eventInspector;
        }
    }
}
