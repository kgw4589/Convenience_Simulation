using TMPro;
using UnityEngine;

public class DialogCanvas : MonoBehaviour
{
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI sentenceText;
    
    private void OnDisable()
    {
        InitUI();
    }

    private void InitUI()
    {
        sentenceText.text = "";
        speakerText.text = "";
    }

    public void SetUI(Speaker speaker, string sentence)
    {
        speakerText.text = speaker.speakerName;
        speakerText.color = speaker.nameColor;

        sentenceText.text = sentence;
    }
}
