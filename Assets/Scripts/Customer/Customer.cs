using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public CustomerMove Move;
    [HideInInspector] public CustomerStreetSpawner Spawner;
    [HideInInspector] public Table Table;
    [HideInInspector] public Order Order;

    public void StartGame()
    {                   
        List<Transform> pos = new List<Transform>();
        pos.Add(Table.StepPosition);
        pos.Add(Table.WaitPosition);
        StartCoroutine(Move.Move(pos,CustomerState.TakeOrder));    
    }
    public void TakeData(Order order,Table table,CustomerStreetSpawner spawner)
    {
        Order = order;
        Table = table;
        Spawner = spawner;
        StartGame();
    }
    public void TakeOrder()
    {
        Table.OpenOrder(Order,this);
    }
}

[Serializable]
public class Order 
{
    public DishData Data = new DishData();
    public int Amount;

    public Order(DishData[] Data)
    {
        System.Random r = new System.Random();
        Amount = r.Next(1, 4);
        this.Data = Data[r.Next(0, Data.Length)];
    }
}
public enum CustomerState
{
    TakeOrder,
    GoOut
}