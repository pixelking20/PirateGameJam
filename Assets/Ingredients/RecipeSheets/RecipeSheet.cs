using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ingredients
{
    [CreateAssetMenu(fileName = "RecipeSheet", menuName = "Ingredients/RecipeSheet", order = 1)]
    public class RecipeSheet : ScriptableObject
    {
        [SerializeField]
        public PotionEntry[] Potions;
    }
}
