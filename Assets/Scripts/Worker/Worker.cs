using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    public TableManager TableManager;
    public DishMachineManager MachineManager;
    [HideInInspector] public Table Table;
    public DishMachine Machine;
    public WorkerMove Move;
    [HideInInspector] public Mission Mission;
    [HideInInspector] public DishVariety Hand;

    private void Start()
    {
        StartCoroutine(FindWork());

    }
    public IEnumerator FindWork()
    {
        while (true)
        {
            foreach (Table table in TableManager.Tables)
            {
                if (table.State == TableState.InWaitingOrder)
                {
                    Table = table;
                    table.State = TableState.InWaitingTakeOrder;
                    Move.Positions.Add(Table.WorkerPosition);
                    StartCoroutine(Move.Move(TableState.InWaitingOrder));
                    yield return new WaitForSeconds(1f);
                    yield break;
                }
                else if (table.State == TableState.WaitDelivery && table.OrderMissions.Count > 0)
                {
                    Table = table;
                    Mission = Table.OrderMissions[0];
                    if (MachineManager.HaveFreeMachines(Mission.Variety))
                    {
                        Table.OrderMissions.Remove(Mission);
                        Table.WorkerDoMissions.Add(Mission);
                        Machine = MachineManager.Machines[MachineManager.GetFreeMachines(Table.Order.Data.DishVariety)];
                        Machine.State = DishMachineState.InWaiting;
                        Move.Positions.Add(Machine.TakePosition);
                        StartCoroutine(Move.Move(TableState.WaitDelivery));
                        yield return new WaitForSeconds(1f);
                        yield break;
                    }

                    break;
                }
            }
            yield return new WaitForSeconds(2f);
        }
    }
    public void TakeOrder()
    {
        Table.TakeOrder(this);
    }
    public void SendDishReq()
    {
        Machine.Worker = this;
        Machine.TakeDish();
    }
    public void DeliveryDish()
    {
        Table.DeliveryOrder();
        StartCoroutine(FindWork());
    }

    public void TakeDish(DishVariety variety)
    {
        Hand = variety;
        Move.Positions.Add(Table.WorkerPosition);
        StartCoroutine(Move.Move(TableState.TakeDish));
    }
}
