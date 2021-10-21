using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class MultiValue
{
    [SerializeField]
    int _selectedIndex = 0;
    [SerializeField]
    string[] options;

    public int SelectedIndex
    {
        get
        {
            return _selectedIndex;
        }
        set
        {
            value = Mathf.Clamp(value, 0, options.Length);
        }
    }

    public MultiValue(params string[] values)
    {
        this.options = values;
    }

    public string SelectedValue
    {
        get
        {
            if(options.Length > 0)
            {
                return options[_selectedIndex];
            }
            else
            {
                return null;
            }
        }
    }
}

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(MultiValue))]
public class MultiValuePropertyDrawer: PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var indexProperty = property.FindPropertyRelative("_selectedIndex");
        var valuesProperty = property.FindPropertyRelative("options");

        var firstLinePosition = position;
        firstLinePosition.height = EditorGUI.GetPropertyHeight(indexProperty);
        var secondLinePosition = firstLinePosition;
        secondLinePosition.y += 2 + firstLinePosition.height;
        secondLinePosition.height = EditorGUI.GetPropertyHeight(valuesProperty);
        firstLinePosition = EditorGUI.PrefixLabel(firstLinePosition, new GUIContent(property.displayName));

        string[] labels = new string[valuesProperty.arraySize];
        for (int i = 0; i < labels.Length; i++)
        {
            labels[i] = valuesProperty.GetArrayElementAtIndex(i).stringValue;
        }

        EditorGUI.BeginChangeCheck();
        var index = indexProperty.intValue;
        var newValue = GUI.Toolbar(firstLinePosition, index, labels);
        if(EditorGUI.EndChangeCheck())
        {
            indexProperty.intValue = newValue;
        }

        EditorGUI.indentLevel++;
        EditorGUI.PropertyField(secondLinePosition, valuesProperty, true);
        EditorGUI.indentLevel--;
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float lineSpacing = EditorGUIUtility.standardVerticalSpacing;
        var indexProperty = property.FindPropertyRelative("_selectedIndex");
        var valuesProperty = property.FindPropertyRelative("options");

        float indexHeight = EditorGUI.GetPropertyHeight(indexProperty);
        float optionsHeight = EditorGUI.GetPropertyHeight(valuesProperty, true);

        return indexHeight + lineSpacing + optionsHeight;
    }
}
#endif
