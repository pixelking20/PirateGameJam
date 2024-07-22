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
            DialogueManager.LoadNewDialogueChain(dialogueChain,OnDialogueFinish);//Just give this function the DialogueChain, and a function to run once the dialogue chain is closed!
        }
    }
    void OnDialogueFinish()
    {
        print("Dialogue Chain Finished!");
    }
}
