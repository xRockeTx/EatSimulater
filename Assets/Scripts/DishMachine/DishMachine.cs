using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishMachine : MonoBehaviour
{
    [HideInInspector] public DishVariety Variety;
    public float TakeTime;
    [HideInInspector] public DishMachineState State;
    public Transform TakePosition;
    public Transform StepPosition;
    public Transform StepGoPosition;
    public DishMachineUI DishMachineUI;
    [HideInInspector] public Worker Worker;

    public void TakeDish(Worker worker)
    {
        Worker = worker;
        State = DishMachineState.InTake;
        DishMachineUI.StartCoroutine(DishMachineUI.TakeDish(TakeTime,worker));
    }

}

public enum DishMachineState
{
    Free,
    InWaiting,
    InTake
}