using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using ChickadeeEvents;
using System;
using UnityEditorInternal;

class EventEditor : EditorWindow
{

    EventManager eventManager;

    Rule selectedRule = null;
    EventCall selectedResponse = null;
    bool criteriaExpanded = true;
    bool responsesExpanded = true;
    ReorderableList ruleList;
    ReorderableList criteriaList;
    ReorderableList responseList;
    ReorderableList responseFactList;

    [MenuItem("Chickadee/Event Editor")]
    public static void ShowWindow()
    {
        EventEditor editor = (EventEditor) GetWindow(typeof(EventEditor));
        editor.eventManager = FindObjectOfType<EventManager>();
        if (editor.eventManager == null)
            return;

        List<Rule> rules = editor.eventManager.rules;

        editor.ruleList = new ReorderableList(rules,
            typeof(Rule), true, false, true, true);
        editor.ruleList.drawElementCallback =
            (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                Rule rule = rules[index];
                EditorGUI.LabelField(rect, rule.eventName);
            };
        editor.ruleList.onChangedCallback = editor.UpdateSelectedRule;
        editor.ruleList.onSelectCallback = editor.UpdateSelectedRule;


        editor.criteriaList = new ReorderableList(null,
            typeof(Fact), true, false, true, true);
        editor.criteriaList.drawElementCallback =
            (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                editor.DrawFact(rect, (Fact) editor.criteriaList.list[index], "criteria"+index);
            };


        editor.responseList = new ReorderableList(null,
            typeof(EventCall), true, false, true, true);
        editor.responseList.drawElementCallback =
            (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                EventCall response = (EventCall) editor.responseList.list[index];
                editor.DrawResponse(rect, response);
            };
        editor.responseList.onChangedCallback = editor.UpdateSelectedResponse;
        editor.responseList.onSelectCallback = editor.UpdateSelectedResponse;

        editor.responseFactList = new ReorderableList(null,
            typeof(Fact), true, false, true, true);
        editor.responseFactList.drawElementCallback =
            (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                editor.DrawFact(rect, (Fact) editor.responseFactList.list[index], "response"+index);
            };
    }


    void OnGUI()
    {
        if(eventManager == null)
        {
            GUILayout.Label("EventManager not found in the scene!");
            if(GUILayout.Button("Spawn new"))
            {
                Instantiate(new GameObject("EventManager", typeof(EventManager)));
                ShowWindow();
            }
            return;
        }

        List<Rule> rules = eventManager.rules;

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical(GUILayout.Width(300));
        EditorGUILayout.BeginScrollView(Vector2.zero);
        
        ruleList.DoLayoutList();

        DrawBlackboard(eventManager);

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(GUILayout.Width(300));
        EditorGUILayout.BeginScrollView(Vector2.zero);


        DrawRule(selectedRule, selectedResponse);

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();

    }

    void UpdateSelectedRule(ReorderableList list)
    {
        if (list.selectedIndices.Count == 0)
            return;

        int firstSelected = list.selectedIndices[0];
        if (list.list.Count > firstSelected)
            selectedRule = (Rule)list.list[firstSelected];
        else
            selectedRule = null;
    }

    void UpdateSelectedResponse(ReorderableList list)
    {
        if (list.selectedIndices.Count == 0)
            return;

        int firstSelected = list.selectedIndices[0];
        if (list.list.Count > firstSelected)
            selectedResponse = (EventCall)list.list[firstSelected];
        else
            selectedResponse = null;
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
            GUILayout.BeginHorizontal();
            GUILayout.Label("event to trigger: ");
            selectedResponse.eventName = GUILayout.TextField(selectedResponse.eventName);
            GUILayout.EndHorizontal();
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
        if(fact == null)
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