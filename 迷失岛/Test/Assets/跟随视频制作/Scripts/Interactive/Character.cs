using UnityEngine;

[RequireComponent(typeof(DialogueController))]
public class Character : Interactive
{
    private DialogueController dialogueController;

    private void Awake()
    {
        dialogueController = GetComponent<DialogueController>();
    }
    public override void OnEmptyClickedAction()
    {
        if (isDone) dialogueController.ShowDialogWhen();
        else
            dialogueController.ShowDialogBefore();
    }

    protected override void OnClickedAction()
    {
        dialogueController.ShowDialogWhen();
    }
}