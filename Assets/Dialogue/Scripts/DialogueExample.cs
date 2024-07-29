using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;

public class DialogueExample : MonoBehaviour
{
    [SerializeField]
    DialogueChain dialogueChain;

    void Update()
    {
        if (!DialogueManager.CheckIsDialogueRunning() && InputHandler.GetInput(Inputs.Interact, ButtonInfo.Press))
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
