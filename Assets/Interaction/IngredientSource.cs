using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSource : Interactable
{
    public GameObject ingredient;

    public override void Interact() {
        print("I gotta give you an ingredient!");
    }
}
