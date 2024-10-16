using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ShowIfAttribute))]
public class ShowIfDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ShowIfAttribute showIf = (ShowIfAttribute)attribute;
        SerializedProperty conditionProperty = property.serializedObject.FindProperty(showIf.ConditionFieldName);

        if (conditionProperty != null && conditionProperty.boolValue)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ShowIfAttribute showIf = (ShowIfAttribute)attribute;
        SerializedProperty conditionProperty = property.serializedObject.FindProperty(showIf.ConditionFieldName);

        return (conditionProperty != null && conditionProperty.boolValue) ? EditorGUI.GetPropertyHeight(property, label, true) : 0;
    }
}
