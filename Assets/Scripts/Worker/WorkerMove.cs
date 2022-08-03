using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerMove : MonoBehaviour
{
    public float MoveSpeed = 3;
    [HideInInspector] public List<Transform> Positions = new List<Transform>();
    [HideInInspector] public Worker Worker;

    public IEnumerator Move(TableState state)
    {
        int id = 0;
        while (id < Positions.Count)
        {
            transform.Translate((Positions[id].position - transform.position).normalized * MoveSpeed * Time.deltaTime, Space.World);
            if (Vector3.Distance(transform.position, Positions[id].position) < 0.25)
            {
                id++;
            }
            yield return new WaitForSeconds(Time.deltaTime / 2);
        }
        Positions = new List<Transform>();
        switch (state)
        {
            case TableState.InWaitingOrder:
                Worker.TakeOrder();
                break;
            case TableState.WaitDelivery:
                Worker.SendDishReq();
                break;
            case TableState.TakeDish:
                Worker.DeliveryDish();
                break;

        }
    }
}
