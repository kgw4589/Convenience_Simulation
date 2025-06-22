using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 다이얼로그 스토리 정의 스크립터블
/// </summary>
[CreateAssetMenu(fileName = "NewStoryScene", menuName = "Story/New Story Scene")]
[Serializable]
public class StoryScene : ScriptableObject
{
    public enum StoryType
    {
        Movable,
    }

    public StoryType storyType; // 스토리 진행 방식.
    public StoryScene nextScene; // 바로 이어져 실행할 다음 스토리.
    
    public List<EventElementData> eventElement;

    [Serializable]
    public class DialogCommandList
    {
        public List<DialogCommandData> commandList;
    }

    public List<DialogCommandList> dialogCommands;
}
