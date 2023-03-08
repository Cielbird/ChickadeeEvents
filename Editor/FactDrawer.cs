using System.Collections;
using System.Collections.Generic;
using Codice.CM.Common.Serialization.Replication;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ChickadeeEvents
{
    [CustomPropertyDrawer(typeof(Fact))]
    public class FactDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var keyProperty = property.FindPropertyRelative("Key");
            PropertyField key = new PropertyField(keyProperty, "");
            var valueProperty = property.FindPropertyRelative("Value");
            PropertyField value = new PropertyField(valueProperty, "");

            VisualElement container = new VisualElement();
            container.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
            container.Add(key);
            container.Add(new Label(" = "));
            container.Add(value);

            return container;
        }
    }
}
