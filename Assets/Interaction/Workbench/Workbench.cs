using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workbench : Interactable
{
    public override void Interact()
    {
        RecipeManager.instance.checkFinish();
        //When finished load minigames //TODO: Keegan this where you step in :)
    }
}
