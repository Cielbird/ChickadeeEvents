using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;
using System;

namespace ChickadeeEvents
{
    /// <summary>
    /// An editor made to replace the older EventEditor window
    /// </summary>
    public class ChickadeeEditor : EditorWindow
    {
        [SerializeField]
        VisualTreeAsset m_EditorAsset;

        [SerializeField]
        VisualTreeAsset m_FactAsset;
        [SerializeField]
        VisualTreeAsset m_EventAsset;
        [SerializeField]
        VisualTreeAsset m_RuleAsset;

        [MenuItem("Chickadee/Chickadee Editor")]
        public static void ShowWindow()
        {
            ChickadeeEditor wnd = GetWindow<ChickadeeEditor>();
            wnd.titleContent = new GUIContent("ChickadeeEditor");
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            VisualElement editor = m_EditorAsset.Instantiate();

            EventManagerData data = EventManager.Current.data;
            SerializedObject so = new SerializedObject(data);

            ListView factListView = editor.Q<ListView>("FactList");
            factListView.Bind(so);
            factListView.makeItem = MakeFact;

            ListView eventListView = editor.Q<ListView>("EventList");
            eventListView.Bind(so);

            root.Add(editor);
        }

        private VisualElement MakeFact()
        {
            VisualElement item = m_FactAsset.CloneTree();
            item.Q<Label>().bindingPath = "key";
            return item;
        }



        [MenuItem("GameObject/Chickadee Events/Event Manager")]
        public static GameObject GetEventManagerGameObject()
        {
            // respect the singleton pattern
            if(EventManager.Current != null)
            {
                EditorUtility.DisplayDialog("Event Manager already present",
                    "There is already an Event Manager in the scene", "Ok");
                return null;
            }

            GameObject em = new GameObject("Event Manager");
            EventManager eventManager = em.AddComponent<EventManager>();
            string[] dataFiles = AssetDatabase.FindAssets("t:EventManagerData");
            if (dataFiles.Length != 0)
            {
                //plug in the first data file found
                string guid = AssetDatabase.GUIDToAssetPath(dataFiles[0]);
                var data = AssetDatabase.LoadAssetAtPath<EventManagerData>(guid);
                eventManager.data = data;
            }
            ShowWindow();
            return em;
        }
    }
}