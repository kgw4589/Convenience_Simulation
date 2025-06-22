using UnityEditor;
using UnityEngine;

/// <summary>
/// 인스펙터에서 조건에 따라 필요한 변수만 보일 수 있도록 해주는 애트리뷰트 
/// </summary>
[CustomPropertyDrawer(typeof(DrawIfAttribute))]
public class DrawIfPropertyDrawer : PropertyDrawer
{
    DrawIfAttribute drawIf;
    SerializedProperty comparedField;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (!ShowMe(property) && drawIf.disablingType == DrawIfAttribute.DisablingType.DontDraw)
            return 0f;
        return base.GetPropertyHeight(property, label);
    }

    private bool ShowMe(SerializedProperty property)
    {
        drawIf = attribute as DrawIfAttribute;
        
        // 현재 경로에서 마지막 프로퍼티 이름을 비교할 프로퍼티 이름으로 교체
        string path = property.propertyPath;
        int lastDotIndex = path.LastIndexOf('.');
        if (lastDotIndex != -1)
        {
            path = path.Substring(0, lastDotIndex + 1) + drawIf.comparedPropertyName;
        }
        else
        {
            path = drawIf.comparedPropertyName;
        }

        comparedField = property.serializedObject.FindProperty(path);

        if (comparedField == null)
        {
            Debug.LogError($"프로퍼티를 찾을 수 없습니다:{drawIf.comparedPropertyName} {path}");
            return true;
        }

        switch (comparedField.type)
        {
            case "bool":
                return CompareBooleanValues();
            case "Enum":
                return CompareEnumValues();
            default:
                Debug.LogError($"지원하지 않는 타입입니다: {comparedField.type}");
                return true;
        }
    }

    private bool CompareBooleanValues()
    {
        foreach (object value in drawIf.comparedValues)
        {
            if (comparedField.boolValue.Equals(value))
                return true;
        }
        return false;
    }

    private bool CompareEnumValues()
    {
        foreach (object value in drawIf.comparedValues)
        {
            if (comparedField.enumValueIndex.Equals((int)value))
                return true;
        }
        return false;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (ShowMe(property))
        {
            EditorGUI.PropertyField(position, property);
        }
        else if (drawIf.disablingType == DrawIfAttribute.DisablingType.ReadOnly)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property);
            GUI.enabled = true;
        }
    }
}