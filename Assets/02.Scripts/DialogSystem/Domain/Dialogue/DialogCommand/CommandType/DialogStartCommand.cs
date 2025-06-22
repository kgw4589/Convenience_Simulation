/// <summary>
/// 다이얼로그 시작
/// </summary>
public class DialogStartCommand : ICommandable
{
    public void PlayCommand(DialogCommandData dialogCommandData)
    {
        StoryScene.StoryType storyType = DialogueManager.Instance.currentScene.storyType;
        
        if (storyType is StoryScene.StoryType.Movable)
        {
            SetMovable();
        }
    }

    private void SetMovable()
    {
        DialogCanvas canvas = DialogueManager.Instance.dialogCanvas;
        canvas.gameObject.SetActive(true);
    }
}
