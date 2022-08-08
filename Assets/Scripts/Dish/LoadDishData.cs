using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDishData : MonoBehaviour
{
    public DishData[] Dishes;
    private void Awake()
    {
        Dishes = Resources.LoadAll<DishData>("Dishes");
    }
}
