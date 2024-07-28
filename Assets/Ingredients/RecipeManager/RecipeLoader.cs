using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ingredients;
using System.Linq;
using Unity.VisualScripting;

public class RecipeLoader : MonoBehaviour
{

    [SerializeField]
    public RecipeSheet recipeSheet;

    private IngredientCollectionReturnTypes lastCollectionResult;

    private List<IngredientEntry> neededIngredients = new List<IngredientEntry>();

    // Start is called before the first frame update
    void Start()
    {
        IngredientManager.Instance.SetRecipeList(recipeSheet);
        loadNeededIngredients();
    }

    public void loadNeededIngredients() {
        foreach(PotionEntry potion in recipeSheet.Potions) {
            foreach(IngredientEntry ingredient in potion.IngredientList) {
                if(!neededIngredients.Contains(ingredient)) {
                    print("HERE");
                    neededIngredients.Add(ingredient);
                }
            }
        }
    }

    public void displayNeededIngredients() {
        print("INGREDIENTS NEEDED");
        if(IngredientManager.Instance.FullyCollected) {
            print("Fully collected!");
        } else {
            foreach(IngredientEntry entry in neededIngredients) {
                if(!entry.collected) {
                    print(entry.FormatIngredientEntry());
                }
            }
        }
    }

    public void collectWine() {
        IngredientManager.Instance.CollectIngredient(IngredientsEnum.ElderBerryWine);
    }

    public void collectClover() {
        IngredientManager.Instance.CollectIngredient(IngredientsEnum.TwoLeafedClover);
    }

    public void checkFinish() {
        print(IngredientManager.Instance.FullyCollected);
    }
}
