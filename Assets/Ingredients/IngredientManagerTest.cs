using Ingredients;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManagerTest : MonoBehaviour
{
    [SerializeField]
    public RecipeSheet sampleSheet;

    private IngredientCollectionReturnTypes lastCollectionResult;


    // Start is called before the first frame update
    void Start()
    {
        IngredientManager.Instance.SetRecipeList(sampleSheet);

        /*Debug.Log($"The value of FullyCollected: {IngredientManager.Instance.FullyCollected}");

        Debug.Log($"The cross Script collection value is : {sampleSheet.Potions[0].IngredientList[0].collected}");+

        lastCollectionResult = IngredientManager.Instance.CollectIngredient(IngredientsEnum.HeartyTomatoes);

        Debug.Log($"The last collection result is: {lastCollectionResult}");

        lastCollectionResult = IngredientManager.Instance.CollectIngredient(IngredientsEnum.ElderBerryWine);

        Debug.Log($"The last collection result is: {lastCollectionResult}");

        Debug.Log($"The cross Script collection value is : {sampleSheet.Potions[0].IngredientList[0].collected}");

        Debug.Log($"The value of FullyCollected: {IngredientManager.Instance.FullyCollected}");

        lastCollectionResult = IngredientManager.Instance.CollectIngredient(IngredientsEnum.ElderBerryWine);

        Debug.Log($"The last collection result is: {lastCollectionResult}");

        Debug.Log($"The cross Script collection value is : {sampleSheet.Potions[0].IngredientList[0].collected}");

        Debug.Log($"The value of FullyCollected: {IngredientManager.Instance.FullyCollected}");

        lastCollectionResult = IngredientManager.Instance.CollectIngredient(IngredientsEnum.TwoLeafedClover);

        Debug.Log($"The last collection result is: {lastCollectionResult}");

        Debug.Log($"The cross Script collection value is : {sampleSheet.Potions[0].IngredientList[0].collected}");

        Debug.Log($"The value of FullyCollected: {IngredientManager.Instance.FullyCollected}");
        */

        // foreach(PotionEntry potion in IngredientManager.Instance.GetRecipeSheet().Potions){
        //         foreach(IngredientEntry ingredient in potion.IngredientList) {
        //             print(ingredient.FormatIngredientEntry());
        //     }
        // }
    }

}
