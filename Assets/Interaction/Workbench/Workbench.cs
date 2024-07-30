using Ingredients;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workbench : Interactable
{
    public MinigameManager manager;

    private bool gamestarted = false;

    public override void Interact()
    {
        if (IngredientManager.Instance.FullyCollected && !gamestarted)
        {
            gamestarted = true;
            manager.StartMinigames();
        }
    }
}
