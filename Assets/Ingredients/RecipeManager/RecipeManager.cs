using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ingredients;
using TMPro;
using System;

public class RecipeManager : MonoBehaviour
{
    public RecipeSheet recipeSheet;

    public GameObject ingredientPanel;
    private Dictionary<IngredientsEnum, GameObject> ingredientDict = new Dictionary<IngredientsEnum, GameObject>();
    public GameObject textPrefab;

    private IngredientCollectionReturnTypes lastCollectionResult;

    private List<IngredientEntry> neededIngredients = new List<IngredientEntry>();
    private GameObject[] sourceList;

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
        sourceList = GameObject.FindGameObjectsWithTag("IngredientSource");
        ingredientPanel = GameObject.FindGameObjectWithTag("IngredientList");
        IngredientManager.Instance.SetRecipeList(recipeSheet);
        loadNeededIngredients();
    }

    public void loadNeededIngredients() {
        foreach(PotionEntry potion in recipeSheet.Potions) {
            foreach(IngredientEntry ingredient in potion.IngredientList) {
                if(!neededIngredients.Contains(ingredient)) {
                    neededIngredients.Add(ingredient);
                    GameObject tempIngredientPrefab = Instantiate(textPrefab, ingredientPanel.transform);
                    tempIngredientPrefab.GetComponent<TextMeshProUGUI>().text = (" • " + ingredient.Name + " - " + ingredient.Description);
                    ingredientDict.Add(ingredient.Type, tempIngredientPrefab);
                }
                foreach(GameObject source in sourceList) {
                    if(source.GetComponent<IngredientSource>().ingredient == ingredient.Type) {
                        source.GetComponent<IngredientSource>().highlightParticles.Play();
                    }
                }
            }
        }
    }

    public void displayNeededIngredients() {
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

    public void updateUI(IngredientsEnum name) {
        if(ingredientDict.ContainsKey(name)){
            ingredientDict[name].GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
        }
    }
}
