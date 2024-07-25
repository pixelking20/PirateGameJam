using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;

public class MinigameExample : MonoBehaviour
{
    [SerializeField]
    Minigame minigame;
    private void Update()
    {
        if (!Minigame.minigameRunning && InputHandler.GetInput(Inputs.Interact,ButtonInfo.Press))
        {
            minigame.InitializeMiniGame();
            minigame.onMinigameComplete += OnMinigameComplete;
        }
    }
    void OnMinigameComplete(bool success)
    {
        print("Minigame Success: " + success);
        minigame.onMinigameComplete -= OnMinigameComplete;
    }
}
