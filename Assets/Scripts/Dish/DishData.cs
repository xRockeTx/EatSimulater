using UnityEngine;

[CreateAssetMenu(fileName = "Dish", menuName = "ScriptableObject/EatVenture/New Dish")]
public class DishData : ScriptableObject
{
    public DishVariety DishVariety;
    public Color DishColor;
}

public enum DishVariety
{
    None,
    Milshake,
    ChocolateShake,
    Donate,
    Maffin,
    Fry,
    Burger,
    Coffee,
    Lemonade
}
