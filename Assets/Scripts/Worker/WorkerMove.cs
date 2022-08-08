using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerMove : MonoBehaviour
{
    public float MoveSpeed = 3;
    [HideInInspector] public List<Transform> Positions = new List<Transform>();
    [HideInInspector] public Worker Worker;

    public IEnumerator Move(List<Transform> positions,WorkerState state)
    {
        int id = 0;
        while (id < positions.Count)
        {
            transform.Translate((positions[id].position - transform.position).normalized * MoveSpeed * Time.deltaTime, Space.World);
            if (Vector3.Distance(transform.position, positions[id].position) < 0.25)
            {
                id++;
            }
            yield return new WaitForSeconds(Time.deltaTime / 2);
        }

        switch (state)
        {
            case WorkerState.None:
                break;
            case WorkerState.TakeOrder:
                Worker.TakeTableOrder();
                break;
            case WorkerState.WentToMachine:
                Worker.TakeMachine();
                break;
            case WorkerState.DeliveryDish:
                Worker.DeliveryDish();
                break;
            case WorkerState.WentToStorage:
                Worker.TakeStorage();
                break;
        }
    }
}

public enum WorkerState
{
    None,
    TakeOrder,
    WentToMachine,
    DeliveryDish,
    WentToStorage,
    DeliveryStorageDish
}
public enum WorkerPositions
{
    None,
    DeliveryOrder,
    DeliveryStorage
}