using System;
using System.Collections;
using System.Collections.Generic;
using ChickadeeEvents;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class EventLogWindow : EditorWindow
{
    [MenuItem("Chickadee/Event Log")]
    public static void ShowWindow()
    {
        EventLogWindow wnd = GetWindow<EventLogWindow>();
        wnd.titleContent = new GUIContent("Event Log");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        IMGUIContainer imgui = new IMGUIContainer(imguiHandler);
        root.Add(imgui);
    }

    /// <summary>
    /// Creates an IMGUI based event log
    /// </summary>
    private void imguiHandler()
    {
        string text = "";
        foreach(string entry in EventManager.Current.log)
        {
            text += entry + '\n';
        }
        GUILayout.Label(text);
    }
}
