using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ingredients;

public class RecipeManager : MonoBehaviour
{
    [SerializeField]
    public RecipeSheet recipeSheet;

    private IngredientCollectionReturnTypes lastCollectionResult;

    private List<IngredientEntry> neededIngredients = new List<IngredientEntry>();

    public static RecipeManager instance;
    private void Awake()
    {
        if (instance)
        {
            Destroy(this.gameObject);
        }
        instance = this;
    }

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

    public void checkFinish() {
        print(IngredientManager.Instance.FullyCollected);
    }

    public void updateUI() {
        print("UPDATE UI");
    }
}
