using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;

public class DialogueManager : MonoBehaviour
{
    static DialogueManager instance;

    static bool currentlyRunning = false;
    private void Awake()
    {
        if (instance)
        {
            Destroy(this.gameObject);
        }
        instance = this;
    }
    public static bool CheckIsDialogueRunning()
    {
        return currentlyRunning;
    }
    //public static void LoadNewDialogueChain(DialogueChain chain, System.Action callback = null)
    //{

    //    if (!currentlyRunning)
    //    {
    //        instance.StartCoroutine(instance.HandleDialogueChain(chain,callback));
    //    }
    //    else
    //    {
            
    //    }
    //}
    public static IEnumerator HandleDialogueChain(DialogueChain chain,System.Action callback = null)
    {
        if (currentlyRunning)
        {
            Debug.LogError("Cannot start new dialogue chain while running another! >.<");
            yield break;
        }
        currentlyRunning = true;

        int index = 0;

        Dialogue[] dialogues = chain.GetDialogueChain();
        Dialogue currentDialogue = dialogues[index];
        DialogueBox.instance.LoadDialogueInfo(currentDialogue);

        string previousTitle = currentDialogue.GetTitle();

        yield return DialogueBox.instance.PopUpMotion();

        while (index < dialogues.Length)
        {
            currentDialogue = dialogues[index];

            if(currentDialogue.GetTitle() != previousTitle)
            {
                yield return DialogueBox.instance.CollapseMotion();
                DialogueBox.instance.LoadDialogueInfo(currentDialogue);
                yield return DialogueBox.instance.PopUpMotion();
                previousTitle = currentDialogue.GetTitle();
            }

            yield return DialogueBox.instance.InitializeNewDialogue(currentDialogue);

            while (!InputHandler.GetInput(Inputs.Interact, ButtonInfo.Press))
            {
                yield return new WaitForEndOfFrame();
            }
            index++;
        }

        yield return DialogueBox.instance.CollapseMotion();

        currentlyRunning = false;
        callback?.Invoke();
    }
}
