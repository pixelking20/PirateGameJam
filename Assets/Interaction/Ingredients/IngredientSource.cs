using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ingredients;

public class IngredientSource : Interactable
{
    public IngredientsEnum ingredient;

    public override void Interact() {
        // print("Wowee, an ingredient!");
        IngredientManager.Instance.CollectIngredient(ingredient);
        RecipeManager.instance.updateUI(ingredient);
    }
}
