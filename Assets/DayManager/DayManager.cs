using Ingredients;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            DayNumber = 0;
        }

        public void ForceSetDay(int  day)
        {
            DayNumber = day;
        }

        public void NextDay()
        {
            DayNumber++;

            //call scene load
        }

        public void SetDayRecipe()
        {
            RecipeManager.instance.recipeSheet = RecipeList[DayNumber];
            RecipeManager.instance.loadNeededIngredients();
            IngredientManager.Instance.SetRecipeList(RecipeList[DayNumber]);
        }

    }
}
