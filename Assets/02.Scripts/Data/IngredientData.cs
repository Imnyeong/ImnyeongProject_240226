using UnityEngine;
using UnityEngine.UI;

namespace Imnyeong
{
    [CreateAssetMenu]
    public class IngredientData : ScriptableObject
    {
        public Image thumbnail;
        public AbilityType abilityType;
        public int abilityValue;
        public string ingredientName;
        public string description;
    }
    public class Ingredient
    {
        public IngredientData ingredient;
        public int count;
    }
}
