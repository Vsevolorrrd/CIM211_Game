using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] string ingredientName;
    [SerializeField] GameObject outline;
    private SpriteRenderer outlineRenderer;
    private bool selected = false;

    private void Awake()
    {
        outlineRenderer = outline.GetComponent<SpriteRenderer>();
    }
    private void OnMouseEnter()
    {
        if (selected) return;
        outline.SetActive(true);
    }
    private void OnMouseExit()
    {
        if (selected) return;
        outline.SetActive(false);
    }
    private void OnMouseDown()
    {
        if (selected)
        {
            RemoveIngredientFromDrink();
            return;
        }

        AddIngredientToDrink();
    }
    void AddIngredientToDrink()
    {
        DrinkManager.Instance.AddIngredient(ingredientName);
        outlineRenderer.color = Color.yellow;
        outline.SetActive(true);
        selected = true;
    }
    void RemoveIngredientFromDrink()
    {
        DrinkManager.Instance.RemoveIngredient(ingredientName);
        outlineRenderer.color = Color.white;
        outline.SetActive(false);
        selected = false;
    }
    public void ResetIngredient()
    {
        outlineRenderer.color = Color.white;
        outline.SetActive(false);
        selected = false;
    }
}