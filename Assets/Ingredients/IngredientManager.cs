using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ingredients
{
    //used as a return type for the collection method
    public enum IngredientCollectionReturnTypes
    {
        Collected = 0,
        AlreadyCollected = 1,
        NotNeeded = 2
    }

    public class IngredientManager : MonoBehaviour
    {

        private RecipeSheet currentRecipeSheet;

        private Dictionary<IngredientsEnum, bool> QuickIngredientValidation;

        public static IngredientManager Instance
        {
            get;

            private set;
        }

        public bool FullyCollected
        {
            get;

            private set;
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.Log("An Instance of this manager already exists");
                Destroy(this);
            }

            Instance = this;

            QuickIngredientValidation = new Dictionary<IngredientsEnum, bool>();
        }

        public void SetRecipeList(RecipeSheet sheet)
        {
            currentRecipeSheet = sheet;

            for (int i = 0;i < sheet.Potions.Length;i++)
            {
                var currentPotion = sheet.Potions[i];

                for (int j = 0; j < currentPotion.IngredientList.Length; j++)
                {
                    var currentIngredient = currentPotion.IngredientList[j];

                    //clears out collection, just in case
                    currentIngredient.collected = false;

                    if (QuickIngredientValidation.ContainsKey(currentIngredient.Type))
                    {
                        continue;
                    }
                    else
                    {
                        QuickIngredientValidation.Add(currentIngredient.Type, false);
                    }
                }
            }
        }

        /// <summary>
        /// Call this to trigger the collection of an ingredient
        /// </summary>
        /// <param name="ingredientType"></param>
        /// <returns>true if the </returns>
        public IngredientCollectionReturnTypes CollectIngredient(IngredientsEnum ingredientType)
        {
            if (QuickIngredientValidation.ContainsKey(ingredientType))
            {
                if (QuickIngredientValidation[ingredientType])
                {
                    //ingredient is already true
                    return IngredientCollectionReturnTypes.AlreadyCollected;
                }
                else
                {
                    //Set validation to true for next time the ingredient is called
                    QuickIngredientValidation[ingredientType] = true;

                    //check if all ingredients have been collected
                    FullyCollected = checkTotalCollection();

                    //loop through the RecipeSheet and set each ingredient of the type to true
                    //this allows multiple potions to use the same ingredient on the same day
                    for (int i = 0; i < currentRecipeSheet.Potions.Length; i++)
                    {
                        var currentPotion = currentRecipeSheet.Potions[i];

                        for (int j = 0; j < currentPotion.IngredientList.Length; j++)
                        {
                            var currentIngredient = currentPotion.IngredientList[j];

                            if (currentIngredient.Type == ingredientType)
                            {
                                currentIngredient.collected = true;
                            }
                        }
                    }

                    //returns after collection logic is complete
                    return IngredientCollectionReturnTypes.Collected;
                }
                
            }
            else
            {
                return IngredientCollectionReturnTypes.NotNeeded;
            }
        }

        

        private bool checkTotalCollection()
        {
            foreach (bool collectedValues in QuickIngredientValidation.Values)
            {
                //explicit false comparrision to make it more readable
                if (collectedValues == false) {
                    return false;
                }
            }

            //if none were false to exit early, than all are collected
            return true;
        }
    }
}
