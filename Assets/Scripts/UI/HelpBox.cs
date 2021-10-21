using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public  class HelpBoxAttribute: PropertyAttribute
{
    public string text;
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(HelpBoxAttribute))]
public class HelpBoxAttributePropertyDrawer: PropertyDrawer
{
    private float HelpBoxHeght
    {
        get
        {
            var width = EditorGUIUtility.currentViewWidth;
            var helpBoxAttribute = attribute as HelpBoxAttribute;
            var content = new GUIContent(helpBoxAttribute.text);
            float helpBoxHeight = EditorStyles.helpBox.CalcHeight(content, width);

            return helpBoxHeight + EditorGUIUtility.singleLineHeight;
        }
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var helpBoxPosition = position;
        helpBoxPosition.height = HelpBoxHeght;
        var propertyPosition = position;
        propertyPosition.y += EditorGUIUtility.standardVerticalSpacing + helpBoxPosition.height;
        propertyPosition.height = EditorGUI.GetPropertyHeight(property, includeChildren: true);

        HelpBoxAttribute helpBox = (attribute as HelpBoxAttribute);
        string text = helpBox.text;
        EditorGUI.HelpBox(helpBoxPosition, text, MessageType.Info);
        EditorGUI.PropertyField(propertyPosition, property, true);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float lineSpacing = EditorGUIUtility.standardVerticalSpacing;
        float propertyHeight = EditorGUI.GetPropertyHeight(property, includeChildren: true);

        return HelpBoxHeght + lineSpacing + propertyHeight;
    }
}
#endif
