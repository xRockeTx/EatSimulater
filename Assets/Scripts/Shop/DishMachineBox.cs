using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishMachineBox : MonoBehaviour
{
    public CustomerStreetSpawner CustomerSpawner;
    public DishData Data;
    public DishMachine DishMachine;
    public int Cost;
    public DishMachineBoxUI BoxUI;

    public void TakeData(DishMachine machine, DishData data, CustomerStreetSpawner customerStreetSpawner,int cost)
    {
        DishMachine = machine;
        Data = data;
        CustomerSpawner = customerStreetSpawner;
        Cost = cost;
        GetComponent<MeshRenderer>().material.color = data.DishColor;
    }
    public void OnMouseUp()
    {
        StartCoroutine(BoxUI.OpenPanel());    
    }
    public void Buy()
    {
        DishMachine.gameObject.SetActive(true);
        if (!CustomerSpawner.CanOrder.Contains(Data))
        {
            CustomerSpawner.CanOrder.Add(Data);
        }
        gameObject.SetActive(false);
    }

}
