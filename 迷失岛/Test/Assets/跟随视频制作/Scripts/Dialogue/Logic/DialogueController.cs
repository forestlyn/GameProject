using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public DialogueData beforeGetTicket;
    public DialogueData whenGetTicket;

    private bool isTalking;
    private Stack<string> beforeGetTicketStack;
    private Stack<string> whenGetTicketStack;

    private Coroutine coroutine;

    private void Awake()
    {
        FillDialogueStack();
    }

    private void OnDisable()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        isTalking = false;
        EventHandler.CallShowDialogueEvent(false, "");
    }

    private void FillDialogueStack()
    {
        beforeGetTicketStack = new Stack<string>();
        whenGetTicketStack = new Stack<string>();
        for (int i = beforeGetTicket.dialogueList.Count - 1; i >= 0; i--)
        {
            beforeGetTicketStack.Push(beforeGetTicket.dialogueList[i]);
        }
        for (int i = whenGetTicket.dialogueList.Count - 1; i >= 0; i--)
        {
            whenGetTicketStack.Push(whenGetTicket.dialogueList[i]);
        }
    }

    public void ShowDialogBefore()
    {
        if (!isTalking)
        {
            coroutine = StartCoroutine(DialogueRoutine(beforeGetTicketStack));
        }
    }

    public void ShowDialogWhen()
    {
        if (!isTalking)
        {
            StartCoroutine(DialogueRoutine(whenGetTicketStack));
        }
    }

    private IEnumerator DialogueRoutine(Stack<string> data)
    {
        isTalking = true;
        if (data.Count != 0)
        {
            EventHandler.CallShowDialogueEvent(true, data.Pop());
            yield return null;
            isTalking = false;
        }
        else
        {
            EventHandler.CallShowDialogueEvent(false, string.Empty);
            FillDialogueStack();
            isTalking = false;
        }
    }
}