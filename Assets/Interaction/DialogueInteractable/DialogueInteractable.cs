using InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteractable : Interactable
{
    [SerializeField]
    DialogueChain dialogueChain;
    
    public override void Interact()
    {
        if (!DialogueManager.CheckIsDialogueRunning())
        {
            StartCoroutine(DialogueExampleCoroutine(dialogueChain));
        }
    }

    void OnDialogueFinish()
    {
        print("Dialogue Chain Finished!");
    }
    IEnumerator DialogueExampleCoroutine(DialogueChain chain)
    {
        yield return DialogueManager.HandleDialogueChain(chain);
        OnDialogueFinish();
    }
}
