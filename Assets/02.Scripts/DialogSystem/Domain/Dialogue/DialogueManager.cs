using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DialogueManager : Singleton<DialogueManager>
{
    public readonly Dictionary<CommandType, Func<ICommandable>> CommandLogicDic = new()
    {
        { CommandType.StartDialog , () => new DialogStartCommand() },
        { CommandType.SoundControl , () => new SoundPlayCommand() },
        { CommandType.TypeSentence , () => new SentenceTypeCommand() },
        { CommandType.EndDialog , () => new DialogFinishCommand() },
    };

    public StoryScene currentScene;

    public DialogCanvas dialogCanvas;
    
    private int _commandIndex = -1;
    private int _commandListIndex = 0;

    private bool _isDelayFinish = true;

    // 타이핑 상태 열거.
    public enum State
    {
        // 타이핑 중, 타이핑 끝.
        Playing,
        Completed
    }
    private State _state = State.Completed;
    public State TypingState
    {
        get => _state;
        set => _state = value;
    }

    /// <summary>
    /// 다음 문장으로 넘어갈 준비가 된 상태 여부를 반환하는 함수 
    /// </summary>
    public bool IsCompleted()
    {
        return _isDelayFinish && _state == State.Completed;
    }
    
    /// <summary>
    /// 현재 문장이 해당 스토리의 마지막 문장인지 확인하는 함수.
    /// </summary>
    public bool IsLastSentence()
    {
        if (_commandIndex + 1 == currentScene.dialogCommands.Count)
        {
            return true;
        }

        if (currentScene.dialogCommands[_commandIndex].commandList[^1].commandType is CommandType.EndDialog
            && _commandListIndex >= currentScene.dialogCommands[_commandIndex].commandList.Count)
        {
            return true;
        }
        
        return false;
    }

    /// <summary>
    /// 한 스토리를 시작하는 함수.
    /// </summary>
    public void PlayScene(StoryScene scene)
    {
        currentScene = scene;
        
        // 문장 인덱스 초기화.
        _commandIndex = -1;
        
        NextCommand();
    }
    
    /// <summary>
    /// 비주얼 노벨 방식의 다음 문장을 실행시키는 함수.
    /// </summary>
    public void NextCommand()
    {
        _isDelayFinish = false;
        
        if (++_commandIndex >= currentScene.dialogCommands.Count)
        {
            return;
        }
        
        List<DialogCommandData> currentCommands = currentScene.dialogCommands[_commandIndex].commandList;
        
        GameManager.Instance.StartCoroutine(PlayCommand(currentCommands));
    }

    // 같은 ID의 커멘드들을 실행시키는 코루틴. 
    private IEnumerator PlayCommand(List<DialogCommandData> commands)
    {
        float time = 0;
        _commandListIndex = 0;
        
        while (_commandListIndex < commands.Count)
        {
            time += Time.deltaTime;

            yield return new WaitUntil(() => _state == State.Completed);
            
            if (_commandListIndex < commands.Count && time >= commands[_commandListIndex].commandDelay)
            {
                ICommandable commandLogic = CommandLogicDic[commands[_commandListIndex].commandType].Invoke();
                commandLogic.PlayCommand(commands[_commandListIndex]);
                ++_commandListIndex;
                
                time = 0;
            }
        }

        // 모든 커멘드가 끝났을 때 다음 ID로 넘어갈 수 있도록.
        _isDelayFinish = true;
        
        if (currentScene.storyType == StoryScene.StoryType.Movable)
        {
            NextCommand();
        }
    }
}