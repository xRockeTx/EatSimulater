using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public DishVariety Variety;
    public int Amount;
    public int MaxAmount;
    public float TakeTime;
    public StorageState State;
    public Transform TakePosition;
    public Transform PickUpPosition;
    public Transform StepPosition;
    public Transform StepGoPosition;
    public List<Mission> OrderMissions = new List<Mission>();
    public List<Mission> WorkerDoMissions = new List<Mission>();
    public StorageUI StorageUI;
     public Worker Worker;

    public void Start()
    {
        for(int i =Amount; i < MaxAmount; i++)
        {
            OrderMissions.Add(new Mission(Variety));
        }
    }
    public void PickUpDish(Worker worker)
    {
        State = StorageState.InTake;
        StorageUI.StartCoroutine(StorageUI.TakeDish(TakeTime,worker));
    }
    public void TakeDish()
    {
        StorageUI.PickUp();
    }

}

public enum StorageState
{
    Free,
    InWaiting,
    InTake
}