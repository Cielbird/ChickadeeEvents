using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ChickadeeEvents
{
    /// <summary>
    /// Editor for event manager data. Allows for in-editor access to facts,
    /// events, rules, and an event log.
    /// </summary>
    public class ChickadeeEditor : EditorWindow
    {
        [MenuItem("Chickadee/Chickadee Editor")]
        public static void ShowWindow()
        {
            ChickadeeEditor wnd = GetWindow<ChickadeeEditor>();
            wnd.titleContent = new GUIContent("ChickadeeEditor");
            wnd.CreateGUI();
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            rootVisualElement.Clear();
            VisualElement root = rootVisualElement;
            var path = "Packages/com.cielbird.chickadee_events/Editor/ChickadeeEditor.uxml";
            var editorAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            VisualElement editor = editorAsset.CloneTree();

            EventManagerData data = EventManager.Current.Data;
            SerializedObject so = new SerializedObject(data);

            editor.Q<Button>("EventLogButton").clicked +=
                EventLogWindow.ShowWindow;

            ListView factListView = editor.Q<ListView>("FactList");
            factListView.Bind(so);

            ListView eventListView = editor.Q<ListView>("EventList");
            eventListView.Bind(so);

            root.Add(editor);
        }

        /// <summary>
        /// Creates a GameObject with an EventManager added.
        /// </summary>
        /// <returns>The GameObject</returns>
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
                eventManager.Data = data;
            }
            ShowWindow();
            return em;
        }
    }
}