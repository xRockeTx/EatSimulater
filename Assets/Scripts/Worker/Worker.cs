using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    public WorkerMove Move;

    [HideInInspector] public TableManager TableManager;
    public DishMachineManager MachineManager;
    public StorageManager StorageManager;
    [HideInInspector] public Table Table;
    [HideInInspector] public DishMachine Machine;
    public Storage Storage;

    public Transform Pos1,Pos2;

    [HideInInspector] public Mission Mission;

    [HideInInspector] public DishVariety Hand;
    public WorkerState State=WorkerState.None;
    public WorkerPositions Position = WorkerPositions.None;

    public void TakeData(TableManager tableManager, DishMachineManager machineManager, StorageManager storageManager, Transform pos1, Transform pos2)
    {
        TableManager = tableManager;
        MachineManager = machineManager;
        StorageManager = storageManager;
        Pos1 = pos1;
        Pos2 = pos2;
        StartCoroutine(FindWork());
    }
    public IEnumerator FindWork()
    {
        while (true)
        {
            if (FindWaitTable())
            {
                yield break;
            }
            else if (FindOrderMission())
            {
                yield break;
            }


            yield return new WaitForSeconds(.1f);
        }
    }
    public bool FindWaitTable()
    {
        bool res = false;
        foreach(Table table in TableManager.Tables)
        {
            if(table.State == TableState.InWaitingOrder&&table.gameObject.activeSelf)
            {
                Table = table;
                Table.State = TableState.InWaitingTakeOrder;

                List<Transform> pos = new List<Transform>();

                pos.Add(Table.WorkerPosition);

                State = WorkerState.TakeOrder;
                Move.StartCoroutine(Move.Move(pos,State));
                res = true;
                break;
            }
        }
        return res;
    }
    public bool FindOrderMission()
    {
        bool res = false;
        foreach (Table table in TableManager.Tables)
        {
            if (table.State == TableState.WaitDelivery&&table.OrderMissions.Count>0)
            {
                Mission = table.OrderMissions[0];
                table.OrderMissions.Remove(Mission);

                if (StorageManager.HaveFreeStorage(Mission.Variety))
                {
                    Storage = StorageManager.GetFreeStorage(Mission.Variety);

                    Table = table;
                    Table.State = TableState.WaitDelivery;

                    List<Transform> pos = new List<Transform>();

                    if (Position == WorkerPositions.None || Position == WorkerPositions.DeliveryOrder)
                    {
                        pos.Add(Storage.TakePosition);
                    }
                    else
                    {
                        pos.Add(Pos2);
                        pos.Add(Pos1);
                        pos.Add(Storage.TakePosition);
                    }

                    State = WorkerState.WentToStorage;
                    Move.StartCoroutine(Move.Move(pos, State));
                    res = true;
                    break;
                }
                if (MachineManager.HaveFreeMachines(Mission.Variety))
                {
                    Machine = MachineManager.GetFreeMachines(Mission.Variety);

                    Table = table;
                    Table.State = TableState.WaitDelivery;

                    List<Transform> pos = new List<Transform>();

                    if (Position == WorkerPositions.None|| Position == WorkerPositions.DeliveryOrder)
                    {
                        pos.Add(Pos1);
                        pos.Add(Pos2);
                        pos.Add(Machine.StepGoPosition);
                        pos.Add(Machine.StepPosition);
                        pos.Add(Machine.TakePosition);
                    }
                    else
                    {
                        pos.Add(Machine.StepGoPosition);
                        pos.Add(Machine.StepPosition);
                        pos.Add(Machine.TakePosition);
                    }

                    State = WorkerState.WentToMachine;
                    Move.StartCoroutine(Move.Move(pos, State));
                    res = true;
                    break;
                }
            }
        }
        return res;
    }
    public bool FindStorageMission()
    {
        bool res = false;
        if (MachineManager.HaveFreeMachines(Mission.Variety))
        {
            Machine = MachineManager.GetFreeMachines(Mission.Variety);
            Machine.State = DishMachineState.InWaiting;
            List<Transform> pos = new List<Transform>();

            pos.Add(Machine.StepGoPosition);
            pos.Add(Machine.StepPosition);
            pos.Add(Machine.TakePosition);

            State = WorkerState.WentToMachine;
            Move.StartCoroutine(Move.Move(pos, State));
            res = true;
        }
        return res;
    }

    public void TakeTableOrder()
    {
        Table.TakeOrder(this);
    }
    public void TakeMachine()
    {
        Machine.TakeDish(this);
    }
    public void TakeTableDish()
    {
        List<Transform> pos = new List<Transform>();

        pos.Add(Machine.StepPosition);
        pos.Add(Machine.StepGoPosition);
        pos.Add(Table.WorkerPosition);

        State = WorkerState.DeliveryDish;
        Move.StartCoroutine(Move.Move(pos, State));
    }
    public void DeliveryDish()
    {
        Table.DeliveryOrder();
        StartCoroutine(FindWork());
    }

    public void TakeStorage()
    {
        Storage.PickUpDish(this);
    }
    public void TakeStorageDish()
    {
        List<Transform> pos = new List<Transform>();

        pos.Add(Table.WorkerPosition);

        State = WorkerState.DeliveryDish;
        Move.StartCoroutine(Move.Move(pos, State));
    }
}
