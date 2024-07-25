using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ingredients
{
    [CreateAssetMenu(fileName = "PotionEntry", menuName = "Ingredients/NewPotionEntry", order = 2)]
    public class PotionEntry : ScriptableObject
    {
        [SerializeField]
        public string PotionName;

        [SerializeField]
        public IngredientEntry[] IngredientList;
    }
}
