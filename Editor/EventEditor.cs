using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using UnityEditorInternal;
using ChickadeeEvents;

namespace ChickadeeEvents
{
    class EventEditor : EditorWindow
    {
        EventManager eventManager;

        Rule selectedRule = null;
        EventCall selectedResponse = null;
        EventCall selectedCall = null;
        bool debugExpanded = false;
        ReorderableList ruleList;
        ReorderableList criteriaList;
        ReorderableList responseList;
        ReorderableList responseFactList;
        ReorderableList debugLogList;

        bool setup = false;

        [MenuItem("Chickadee/Event Editor")]
        public static void SetUpWindow()
        {
            EventEditor editor = (EventEditor)GetWindow(typeof(EventEditor));
            editor.SetUp();
        }

        private void Awake()
        {
            SetUp();
        }

        private void SetUp()
        {
            eventManager = FindObjectOfType<EventManager>();
            if (eventManager == null)
                return;

            List<Rule> rules = eventManager.rules;

            ruleList = new ReorderableList(rules,
                typeof(Rule), true, false, true, true);
            ruleList.drawElementCallback =
                (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    Rule rule = rules[index];
                    EditorGUI.LabelField(rect, rule.eventName);
                };


            criteriaList = new ReorderableList(null,
                typeof(Fact), true, false, true, true);
            criteriaList.drawElementCallback =
                (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    DrawFact(rect, (Fact)criteriaList.list[index], "criteria" + index);
                };


            responseList = new ReorderableList(null,
                typeof(EventCall), true, false, true, true);
            responseList.drawElementCallback =
                (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    EventCall response = (EventCall)responseList.list[index];
                    DrawResponse(rect, response);
                };

            responseFactList = new ReorderableList(null,
                typeof(Fact), true, false, true, true);
            responseFactList.drawElementCallback =
                (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    DrawFact(rect, (Fact)responseFactList.list[index], "response" + index);
                };


            debugLogList = new ReorderableList(eventManager.debugLog,
                                typeof(EventCall), false, false, false, false);

            setup = true;
        }

        void OnGUI()
        {
            if (eventManager == null)
            {
                // idk why but this works
                SetUp();
                if (eventManager == null)
                {
                    GUILayout.Label("EventManager not found in the scene!");
                    if (GUILayout.Button("Spawn new"))
                    {
                        Instantiate(new GameObject("EventManager", typeof(EventManager)));
                        SetUpWindow();
                    }
                    return;
                }
            }

            //update selected
            UpdateSelected(ruleList, out selectedRule);
            UpdateSelected(responseList, out selectedResponse);
            UpdateSelected(debugLogList, out selectedCall);

            List<Rule> rules = eventManager.rules;

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical(GUILayout.Width(300));
            EditorGUILayout.BeginScrollView(Vector2.zero);

            ruleList.DoLayoutList();

            debugExpanded = EditorGUILayout.Foldout(debugExpanded, "Debug:");
            if(debugExpanded)
            {
                GUILayout.Label("Event log");

                debugLogList.DoLayoutList();

                if(selectedCall != null)
                {
                    GUILayout.Label(selectedCall.EventName);
                    GUILayout.Label($"from:\n\t{selectedCall.Sender}\n" +
                                    $"to:\n\t{selectedCall.Target}\n" +
                                    $"called by:\n\t{selectedCall.caller}\n");
                }

                DrawBlackboard(eventManager);
            }


            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(GUILayout.Width(300));
            EditorGUILayout.BeginScrollView(Vector2.zero);


            DrawRule(selectedRule, selectedResponse);

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();

        }

        void UpdateSelected<T>(ReorderableList list, out T selectedVar)
        {
            selectedVar = default(T);
            if (list.selectedIndices.Count == 0)
                return;

            int firstSelected = list.selectedIndices[0];
            if (list.list.Count > firstSelected)
                selectedVar = (T)list.list[firstSelected];
            else
                selectedVar = default(T);
        }

        public void DrawRule(Rule rule, EventCall selectedResponse)
        {
            if (rule == null)
                return;

            EditorGUILayout.LabelField("event name");
            rule.eventName = GUILayout.TextField(rule.eventName);

            EditorGUILayout.Separator();

            GUILayout.Label("criteria");
            if (criteriaList.list != rule.criteria)
                criteriaList.list = rule.criteria;
            criteriaList.DoLayoutList();

            GUILayout.Label("responses");
            if (responseList.list != rule.responses)
                responseList.list = rule.responses;
            responseList.DoLayoutList();

            GUILayout.Space(50);
            if (selectedResponse == null)
            {
                GUILayout.Label("no response selected");
            }
            else
            {
                if (responseFactList.list != selectedResponse.eventFacts)
                    responseFactList.list = selectedResponse.eventFacts;

                if (selectedResponse.eventFacts.GetValue("event_name") == null)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("event_name not found!");
                    if(GUILayout.Button("Add event_name"))
                    {
                        Fact fact = new Fact("event_name", "untitled_event");
                        selectedResponse.eventFacts.Add(fact);
                    }
                    GUILayout.EndHorizontal();
                }
                

                responseFactList.DoLayoutList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="fact"></param>
        /// <param name="name">used for controls</param>
        private void DrawFact(Rect rect, Fact fact, string name)
        {
            if (fact == null)
            {
                GUI.Label(rect, "null criterion", EditorStyles.miniLabel);
                return;
            }
            float thirdWidth = rect.width / 3f;
            fact.key = GUI.TextField(new Rect(rect.x, rect.y, thirdWidth, rect.height), fact.key);

            GUI.Label(new Rect(rect.x + thirdWidth, rect.y, thirdWidth, rect.height), " = ");

            fact.value = GUI.TextField(new Rect(rect.x + thirdWidth * 2f, rect.y, thirdWidth, rect.height), fact.value);
        }

        private void DrawResponse(Rect rect, EventCall response)
        {
            if (response == null)
            {
                GUI.Label(rect, "null response");
                return;
            }

            GUI.Label(rect, response.ToString());
        }


        private void DrawBlackboard(EventManager eventManager)
        {
            EditorGUILayout.BeginScrollView(Vector2.zero);
            foreach (Fact fact in eventManager.blackboard.facts)
            {
                GUILayout.Label(fact.ToString());
            }

            foreach (Rule rule in eventManager.rules)
            {
                foreach (Fact fact in rule.criteria)
                {
                    GUILayout.Label(fact.key);
                }
            }
            EditorGUILayout.EndScrollView();
        }
    }
}
