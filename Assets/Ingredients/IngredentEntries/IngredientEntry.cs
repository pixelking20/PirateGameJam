using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ingredients
{
    [CreateAssetMenu(fileName = "IngredientEntry", menuName = "Ingredients/IngredientEntry", order = 3)]
    public class IngredientEntry : ScriptableObject
    {
        [SerializeField]
        public IngredientsEnum Type;

        public bool AlwaysCollected;

        [SerializeField]
        public string Name;

        [SerializeField]
        public string Description;

        //TODO, set this to be an enum
        [SerializeField]
        public string Room;

        public bool collected;

        [SerializeField]
        GameObject prefab;

        public string FormatIngredientEntry()
        {
            return $"{Name} {Description} - {Room}";
        }
        public GameObject GetPrefab()
        {
            return prefab;
        }
    }
}
