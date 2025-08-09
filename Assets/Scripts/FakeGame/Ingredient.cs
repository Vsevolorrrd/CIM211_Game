using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] string ingredientName;
    void AddIngredientToDrink()
    {
        DrinkManager.Instance.AddIngredient(ingredientName);
    }
}