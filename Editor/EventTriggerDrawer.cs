using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ChickadeeEvents
{
    [CustomPropertyDrawer(typeof(EventTrigger))]
    public class EventTriggerDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement container = new VisualElement();
            // name
            var eventNames = EventManager.Current.Data.EventNames;
            var eventDropdown = new DropdownField("Event: ", eventNames, 0);
            var eventNameProp = property.FindPropertyRelative("EventName");
            eventDropdown.BindProperty(eventNameProp);
            container.Add(eventDropdown);
            // trigger
            var trigProp = property.FindPropertyRelative("Triggers");
            PropertyField trigField = new PropertyField(trigProp);
            container.Add(trigField);

            return container;
        }
    }
}
