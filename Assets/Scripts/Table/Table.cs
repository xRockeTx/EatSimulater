using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [HideInInspector] public TableState State = TableState.Free;
    [Header("Позиции")]
    public Transform WaitPosition;
    public Transform StepPosition;
    public Transform OutStepPosition;
    public Transform OutPosition;
    public Transform WorkerPosition;
    public TableUI TableUI;
    public Order Order = null;
    [HideInInspector] public List<Mission> OrderMissions = new List<Mission>();
    [HideInInspector] public List<Mission> WorkerDoMissions = new List<Mission>();
    [HideInInspector] public List<Mission> CompleteMissions = new List<Mission>();
    [HideInInspector] public Worker Worker;
    [HideInInspector] public Customer Customer;
    public float OrderTakeSpeed;

    public void TakeOrder(Worker worker)
    {
        Worker = worker;
        State = TableState.InWaitingTakeOrder;
        StartCoroutine(TableUI.TakeOrder(Order, OrderTakeSpeed));
    }
    public void DeliveryOrder()
    {
        TableUI.DeliveryOrder();
    }

    public void OpenOrder(Order order,Customer customer)
    {
        State = TableState.InWaitingOrder;
        Order = order;
        Customer = customer;

        for (int i = 0; i < order.Amount; i++)
        {
            OrderMissions.Add(new Mission(order.Data.DishVariety));
        }
    }

    public void TakeOrderResult()
    {
        Worker.StartCoroutine(Worker.FindWork());
    }
}

    [Serializable]
public class Mission
{
    public DishVariety Variety;

    public Mission(DishVariety variety)
    {
        Variety = variety;
    }
}

public enum TableState
{
    Free,
    InWaiting,
    InWaitingOrder,
    InWaitingTakeOrder,
    TakeOrder,
    WaitDelivery,
    TakeDish
}