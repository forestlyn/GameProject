using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public Text dialogueText;
    public GameObject Panel;
    private void OnEnable()
    {
        EventHandler.ShowDialogueEvent += ShowDialogue;
    }
    private void OnDisable()
    {
        EventHandler.ShowDialogueEvent -= ShowDialogue;
    }
    private void ShowDialogue(bool open, string dialogue)
    {
        dialogueText.text = dialogue;
        Panel.SetActive(open);
    }
}