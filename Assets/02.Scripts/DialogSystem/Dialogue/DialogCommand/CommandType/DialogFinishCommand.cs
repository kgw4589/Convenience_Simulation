using UnityEngine;

/// <summary>
/// 다이얼로그 끝
/// </summary>
public class DialogFinishCommand : ICommandable
{
    public void PlayCommand(DialogCommandData dialogCommandData)
    {
        DialogueManager dialogueManager = DialogueManager.Instance;
        
        dialogueManager.TypingState = DialogueManager.State.Completed;
        
        // 모든 코루틴 정지.
        GameManager.Instance.StopAllCoroutines();
        
        DialogCanvas canvas = dialogueManager.dialogCanvas;
        canvas.gameObject.SetActive(false);
        
        Time.timeScale = 1;
        
        EventManager.Instance.PlayEventByData(dialogueManager.currentScene.eventElement);
    }
}
