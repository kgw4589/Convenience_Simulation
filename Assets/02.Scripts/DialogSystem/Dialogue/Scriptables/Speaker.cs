using UnityEngine;

/// <summary>
/// 다이얼로그에서 쓰일 화자 정보 스크립터블
/// </summary>
[CreateAssetMenu(fileName = "NewSpeaker", menuName = "Story/New Speaker")]
[System.Serializable]
public class Speaker : ScriptableObject
{
    public string speakerName; // 화면에 표시될 이름.
    public Color nameColor; // 이름 텍스트 색상.
}

