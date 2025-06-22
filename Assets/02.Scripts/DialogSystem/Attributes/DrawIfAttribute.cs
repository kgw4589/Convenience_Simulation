using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class DrawIfAttribute : PropertyAttribute
{
    public string comparedPropertyName { get; private set; }
    public object[] comparedValues { get; private set; }
    public DisablingType disablingType { get; private set; }

    public enum DisablingType
    {
        ReadOnly = 2,
        DontDraw = 3
    }

    // 기존 생성자 (하나의 값 비교용)
    public DrawIfAttribute(string comparedPropertyName, object comparedValue, DisablingType disablingType = DisablingType.DontDraw)
    {
        this.comparedPropertyName = comparedPropertyName;
        this.comparedValues = new object[] { comparedValue };
        this.disablingType = disablingType;
    }

    // 새로운 생성자 (두 개의 값을 OR 조건으로 비교)
    public DrawIfAttribute(string comparedPropertyName, DisablingType disablingType = DisablingType.DontDraw, params object[] values)
    {
        this.comparedPropertyName = comparedPropertyName;
        this.disablingType = disablingType;
        this.comparedValues = values;
    }
}