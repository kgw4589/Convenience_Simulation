using System;
using UnityEngine;
using UnityEngine.Serialization;

public enum CommandType
{
    StartDialog,
    SoundControl,
    TypeSentence,
    EndDialog,
}

/// <summary>
/// 다이얼로그 커멘드에 필요한 세부 수치 데이터 정의
/// </summary>
[Serializable]
public class DialogCommandData
{
    public float commandDelay;
    public CommandType commandType;
    
    [DrawIf("commandType", DrawIfAttribute.DisablingType.DontDraw, CommandType.TypeSentence)]
    public Speaker speaker;
    

    #region Data-SoundControl

    public enum SoundControlType
    {
        PlaySfx, PlayBgm, StopBgm
    }

    [DrawIf("commandType", CommandType.SoundControl)]
    public SoundControlType soundType;

    [DrawIf("commandType", CommandType.SoundControl)]
    [DrawIf("soundType", DrawIfAttribute.DisablingType.DontDraw, SoundControlType.PlaySfx, SoundControlType.PlayBgm)]
    public AudioClip audioClip;

    [DrawIf("commandType", CommandType.SoundControl)]
    [DrawIf("soundType", DrawIfAttribute.DisablingType.DontDraw, SoundControlType.PlaySfx, SoundControlType.PlayBgm)]
    [Range(0, 1)]
    public float soundVolume = 1;

    [FormerlySerializedAs("isLoop")]
    [DrawIf("commandType", CommandType.SoundControl)]
    [DrawIf("soundType", DrawIfAttribute.DisablingType.DontDraw, SoundControlType.PlayBgm)]
    public bool isLoopSound = true;

    #endregion

    #region Data-TypeSentence

    [DrawIf("commandType", CommandType.TypeSentence)]
    public string sentence;

    [DrawIf("commandType", CommandType.TypeSentence)]
    public bool useTyping = true;
    
    [DrawIf("commandType", CommandType.TypeSentence)]
    [DrawIf("useTyping", true)]
    public bool useDefaultTypeDelay = true;

    [DrawIf("commandType", CommandType.TypeSentence)]
    [DrawIf("useTyping", true)]
    [DrawIf("useDefaultTypeDelay", false)]
    public float typeDelay;

    #endregion
}