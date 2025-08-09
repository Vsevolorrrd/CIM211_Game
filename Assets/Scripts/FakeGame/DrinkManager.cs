using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrinkManager : Singleton<DrinkManager>
{
    private List<string> currentDrink = new List<string>();

    public void AddIngredient(string name)
    {
        currentDrink.Add(name);
        Debug.Log("Added: " + name);
    }

    public void FinishDrink()
    {
        var visit = ShiftManager.Instance?.GetCustomerVisit();

        if (visit == null)
        {
            Debug.LogWarning("No current customer visit found.");
            return;
        }

        var ingredientsForDrink = new List<string>(visit.requestedDrink.ingredients);

        bool isCorrect = AreDrinksEqual(currentDrink, ingredientsForDrink);

        if (isCorrect)
        {
            ShiftManager.Instance.Reward(5);
            Debug.Log("Drink is correct!");
        }
        else
        {
            ShiftManager.Instance.Penalize(10);
            Debug.Log("Drink is wrong!");
        }

        currentDrink.Clear();
    }

    private bool AreDrinksEqual(List<string> input, List<string> ingredientsForDrink)
    {
        if (input.Count != ingredientsForDrink.Count)
        return false;

        var inputSorted = new List<string>(input);
        var ingredientsForDrinkSorted = new List<string>(ingredientsForDrink);

        inputSorted.Sort();
        ingredientsForDrinkSorted.Sort();

        return inputSorted.SequenceEqual(ingredientsForDrinkSorted);
    }
}