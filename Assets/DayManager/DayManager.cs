using Ingredients;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SceneBlocks;

namespace DayProgress
{
    public class DayManager : MonoBehaviour
    {
        public int DayNumber
        {
            get;

            private set;
        }

        public static DayManager Instance
        {
            get;

            private set;
        }

        public RecipeSheet[] RecipeList;


        // Start is called before the first frame update
        void Awake()
        {
            if (Instance != null)
            {
                Debug.Log("There is already a copy of DayManager");
                Destroy(this);
            }
            Instance = this;

            DayNumber = 1;
        }

        public void ForceSetDay(int  day)
        {
            DayNumber = day;
        }

        public void NextDay()
        {
            DayNumber++;
            StartCoroutine(SceneBlockManager.Instance.ChangeScene((SceneBlockEnum)DayNumber));
        }

        public void SetDayRecipe()
        {
            RecipeManager.instance.recipeSheet = RecipeList[DayNumber];
            RecipeManager.instance.loadNeededIngredients();
            IngredientManager.Instance.SetRecipeList(RecipeList[DayNumber]);
        }

    }
}
