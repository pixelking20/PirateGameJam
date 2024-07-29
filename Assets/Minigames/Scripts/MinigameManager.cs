using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;

public class MinigameManager : MonoBehaviour
{
    [SerializeField]
    Minigame[] minigames;
    [SerializeField]
    DialogueChain[] dialogueChainsSuccess, dialogueChainsFailures;

    bool minigameChainRunning = false;

    private void Update()
    {
        if (InputHandler.GetInput(Inputs.Interact, ButtonInfo.Press) && !minigameChainRunning)    
        {
            StartCoroutine(InitializeMinigameChain());
        }
    }
    IEnumerator InitializeMinigameChain()
    {
        minigameChainRunning = true;
        int minigameIndex = 0;
        bool miniGameSucceeded = false;

        minigames[minigameIndex].onMinigameComplete += WhenMinigameComplete;

        while (minigameIndex < minigames.Length)
        {
            yield return minigames[minigameIndex].StartMiniGame();
            yield return miniGameSucceeded ? OnMinigameSucceed(minigameIndex) : OnMinigameFail(minigameIndex);
            if (miniGameSucceeded)
            {
                minigames[minigameIndex].onMinigameComplete -= WhenMinigameComplete;
                minigameIndex++;
                if(minigameIndex < minigames.Length)
                {
                    minigames[minigameIndex].onMinigameComplete += WhenMinigameComplete;
                }
            }
        }

        print("Minigame chain completed!");
        minigameChainRunning = false;

        void WhenMinigameComplete(bool success)
        {
            miniGameSucceeded = success;
        }
    }
    IEnumerator OnMinigameSucceed(int miniGameIndex)
    {
        DialogueChain chain = dialogueChainsSuccess[miniGameIndex];
        if(chain)
            yield return DialogueManager.HandleDialogueChain(chain);
    }
    IEnumerator OnMinigameFail(int minigameIndex)
    {
        DialogueChain chain = dialogueChainsFailures[minigameIndex];
        if (chain)
            yield return DialogueManager.HandleDialogueChain(chain);
    }
}
