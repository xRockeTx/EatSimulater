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

    public void TakeDish()
    {
        State = DishMachineState.InTake;
        DishMachineUI.StartCoroutine(DishMachineUI.TakeDish(TakeTime,Worker));
    }

}

public enum DishMachineState
{
    Free,
    InWaiting,
    InTake
}