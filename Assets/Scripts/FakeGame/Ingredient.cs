using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] string ingredientName;
    [SerializeField] GameObject outline;

    private void OnMouseEnter()
    {
        outline.SetActive(true);
    }
    private void OnMouseExit()
    {
        outline.SetActive(false);
    }
    private void OnMouseDown()
    {
        AddIngredientToDrink();
    }
    void AddIngredientToDrink()
    {
        DrinkManager.Instance.AddIngredient(ingredientName);
    }
}