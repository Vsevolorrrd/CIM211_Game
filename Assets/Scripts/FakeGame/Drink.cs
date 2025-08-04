using UnityEngine;

[CreateAssetMenu(fileName = "NewDrink", menuName = "NightShift System/Drink")]
public class Drink : ScriptableObject
{
    public string drinkName;
    public Sprite icon;
    public string[] ingredients;
}