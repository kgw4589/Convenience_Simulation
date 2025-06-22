using System.Collections;
using System.Text;
using UnityEngine;

/// <summary>
/// 대사 타이핑
/// </summary>
public class SentenceTypeCommand : ICommandable
{
    private const float TypeDelay = 0.07f;

    public void PlayCommand(DialogCommandData dialogCommandData)
    {
        StoryScene.StoryType storyType = DialogueManager.Instance.currentScene.storyType;

        if (storyType is StoryScene.StoryType.Movable)
        {
            if (dialogCommandData.useTyping)
            {
                DialogueManager.Instance.StartCoroutine(TypeSentence(dialogCommandData));
            }
            else
            {
                ShowSentence(dialogCommandData);
            }
        }
    }
    
    /// <summary>
    /// 대사를 한 글자씩 입력하는 함수.
    /// </summary>
    private IEnumerator TypeSentence(DialogCommandData dialogCommandData)
    {
        Speaker speaker = dialogCommandData.speaker;
        string sentence = dialogCommandData.sentence;
        float typeDelay = dialogCommandData.useDefaultTypeDelay ? TypeDelay : dialogCommandData.typeDelay;
        
        DialogCanvas canvas = DialogueManager.Instance.dialogCanvas;

        canvas.speakerText.text = speaker.speakerName;
        canvas.speakerText.color = speaker.nameColor;
        canvas.sentenceText.text = "";
        
        // 현재 상태를 타이핑 중으로 변경.
        DialogueManager.Instance.TypingState = DialogueManager.State.Playing;
        
        // 현재 글자 인덱스 초기화.
        int wordIndex = 0;
        bool isColoring = false;
        
        StringBuilder stringBuilder = new StringBuilder();
        
        // 모든 글자를 입력하기 전까지 타이핑.
        while (DialogueManager.Instance.TypingState != DialogueManager.State.Completed)
        {
            yield return null;
            
            // 모든 글자를 입력했으면 상태를 완료로 변경.
            if (wordIndex >= sentence.Length)
            {
                DialogueManager.Instance.TypingState = DialogueManager.State.Completed;
                break;
            }
            
            if (sentence[wordIndex].ToString().Equals("<"))
            {
                isColoring = true;
            }
            
            // 텍스트에 글자 하나씩 추가.
            if (isColoring)
            {
                stringBuilder.Append(sentence[wordIndex]);
                if (sentence[wordIndex++].ToString().Equals(">"))
                {
                    isColoring = false;
                    canvas.sentenceText.text += stringBuilder.ToString();
                    stringBuilder.Clear();
                }
                continue;
            }
            canvas.sentenceText.text += sentence[wordIndex++];
            
            // 타이핑 딜레이만큼 대기.
            yield return new WaitForSeconds(typeDelay);
        }
    }

    private void ShowSentence(DialogCommandData dialogCommandData)
    {
        DialogCanvas canvas = DialogueManager.Instance.dialogCanvas;

        Speaker speaker = dialogCommandData.speaker;
        string sentence = dialogCommandData.sentence;
        
        canvas.SetUI(speaker, sentence);
    }
}
