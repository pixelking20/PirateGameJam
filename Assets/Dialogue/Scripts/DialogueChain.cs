using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChain : ScriptableObject
{
    [SerializeField]
    Dialogue[] dialogues;
    public Dialogue[] GetDialogueChain()
    {
        return dialogues;
    }
}
