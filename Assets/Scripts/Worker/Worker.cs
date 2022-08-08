using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    [HideInInspector] public TableManager TableManager;
    [HideInInspector] public DishMachineManager MachineManager;  
    [HideInInspector] public StorageManager StorageManager;
    [HideInInspector] public Table Table;
    [HideInInspector] public DishMachine Machine;
    [HideInInspector] public Storage Storage;
    [HideInInspector] public Transform StorageStepPosition1;
    [HideInInspector] public Transform StorageStepPosition2;
    public WorkerMove Move;
    [HideInInspector] public Mission Mission;
    [HideInInspector] public DishVariety Hand;
    public WorkerState State=WorkerState.None;
    public WorkerPositions Position = WorkerPositions.DeliveryOrder;

    public void TakeData(TableManager tableManager, DishMachineManager machineManager, StorageManager storageManager,Transform storageStepPosition1, Transform storageStepPosition2)
    {
        TableManager = tableManager;
        MachineManager = machineManager;
        StorageManager = storageManager;
        StorageStepPosition1 = storageStepPosition1;
        StorageStepPosition2 = storageStepPosition2;
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
                    FindOrderTableWork(table);
                    yield break;
                }
                else if(table.State == TableState.WaitDelivery && table.OrderMissions.Count > 0)
                {
                    if (FindFreeDish(table))
                    {
                        yield break;
                    }
                }
            }
            foreach (Storage storage in StorageManager.Storages)
            {
                if (storage.gameObject.activeSelf &&storage.OrderMissions.Count > 0&&storage.Amount<storage.MaxAmount)
                {
                    if (FindFreeStorage(storage))
                    {
                        break;
                    }
                }
            }
            
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void FindOrderTableWork(Table table)
    {
        Table = table;
        table.State = TableState.InWaitingTakeOrder;
        List<Transform> pos = new List<Transform>();
        if (Position == WorkerPositions.None || Position == WorkerPositions.DeliveryOrder)
        {
            pos.Add(Table.WorkerPosition);
        }
        else
        {
            pos.Add(StorageStepPosition2);
            pos.Add(StorageStepPosition1);
            pos.Add(Table.WorkerPosition);
        }
        State = WorkerState.TakeOrder;
        Move.StartCoroutine(Move.NewMove(State, pos));
    }
    public bool FindFreeDish(Table table)
    {
        bool res = false;
        Table = table;
        Mission = Table.OrderMissions[0];
        List<Transform> pos = new List<Transform>();
        if (StorageManager.HaveFreeStorage(Mission.Variety))
        {
            res = true;
            Table.OrderMissions.Remove(Mission);
            Table.WorkerDoMissions.Add(Mission);

            Storage = StorageManager.Storages[StorageManager.GetFreeMachines(Table.Order.Data.DishVariety)];
            Storage.State = StorageState.InWaiting;

            Move.Positions = new List<Transform>();
            if (State == WorkerState.None || State == WorkerState.TakeOrder)
            {
                pos.Add(Storage.TakePosition);
            }
            else if (State == WorkerState.TakeStorageOrder)
            {
                pos.Add(StorageStepPosition2);
                pos.Add(StorageStepPosition1);
                pos.Add(Storage.TakePosition);
            }
            State = WorkerState.TakeStorageOrder;
            Move.StartCoroutine(Move.NewMove(State,pos));
        }
        else if (MachineManager.HaveFreeMachines(Mission.Variety))
        {
            res = true;
            Table.OrderMissions.Remove(Mission);
            Table.WorkerDoMissions.Add(Mission);

            Machine = MachineManager.Machines[MachineManager.GetFreeMachines(Table.Order.Data.DishVariety)];
            Machine.State = DishMachineState.InWaiting;

            Move.Positions = new List<Transform>();
            if (Position == WorkerPositions.None || Position == WorkerPositions.DeliveryOrder)
            {
                pos.Add(StorageStepPosition1);
                pos.Add(StorageStepPosition2);
                pos.Add(Machine.StepGoPosition);
                pos.Add(Machine.StepPosition);
                pos.Add(Machine.TakePosition);
            }
            else if (Position == WorkerPositions.DeliveryStorageDish)
            {
                pos.Add(Machine.StepGoPosition);
                pos.Add(Machine.StepPosition);
                pos.Add(Machine.TakePosition);
            }
            State = WorkerState.SendDishReq;
            Move.StartCoroutine(Move.NewMove(State,pos));
        }
        return res;
    }
    public bool FindFreeStorage(Storage storage)
    {
        bool res = false;
        Storage = storage;
        Mission = storage.OrderMissions[0];
        if (MachineManager.HaveFreeMachines(Mission.Variety))
        {
            res = true;
            Storage.OrderMissions.Remove(Mission);
            Storage.WorkerDoMissions.Add(Mission);

            Machine = MachineManager.Machines[MachineManager.GetFreeMachines(Mission.Variety)];
            Machine.State = DishMachineState.InWaiting;

            List < Transform > pos = new List<Transform>();
            if (Position == WorkerPositions.None || Position == WorkerPositions.DeliveryOrder)
            {
                pos.Add(StorageStepPosition1);
                pos.Add(StorageStepPosition2);
                pos.Add(Machine.StepGoPosition);
                pos.Add(Machine.StepPosition);
                pos.Add(Machine.TakePosition);
            }
            else if (Position == WorkerPositions.DeliveryStorageDish)
            {
                pos.Add(Machine.StepGoPosition);
                pos.Add(Machine.StepPosition);
                pos.Add(Machine.TakePosition);
            }
            State = WorkerState.TakeStorageOrder;
            Move.StartCoroutine(Move.NewMove(State,pos));
        }
        return res;
    }

    public void SendDishReq()
    {
        Machine.Worker = this;
        Machine.TakeDish();
    }
    public void SendStorageDishReq()
    {
        Storage.Worker = this;
        Storage.PickUpDish();
    }
    public void DeliveryStorageDish()
    {
        Storage.TakeDish();
        Storage = null;
        State = WorkerState.None;
        Position = WorkerPositions.DeliveryStorageDish;
        StartCoroutine(FindWork());
    }

    public void TakeOrder()
    {
        Table.TakeOrder(this);
    }     // +
    public void DeliveryDish()
    {
        Table.DeliveryOrder();
        State = WorkerState.TakeOrder;
        Position = WorkerPositions.DeliveryOrder;
        StartCoroutine(FindWork());
    }  // +
    public void TakeStorageDish(DishVariety variety)
    {
        Hand = variety;
        List<Transform> pos = new List<Transform>();
        pos.Add(Table.WorkerPosition);
        StartCoroutine(Move.NewMove(WorkerState.DeliveryDish,pos));
    }
    public void TakeDish(DishVariety variety)
    {
        Hand = variety;
        if (State==WorkerState.TakeStorageOrder)
        {
            List<Transform> pos = new List<Transform>();
            pos.Add(Machine.StepPosition);
            pos.Add(Machine.StepGoPosition);
            pos.Add(Storage.PickUpPosition);
            Position = WorkerPositions.DeliveryStorageDish;
            StartCoroutine(Move.NewMove(WorkerState.DeliveryStorageDish, pos));
        }
        else
        {
            List<Transform> pos = new List<Transform>();
            pos.Add(Machine.StepPosition);
            pos.Add(Machine.StepGoPosition);
            pos.Add(StorageStepPosition2);
            pos.Add(StorageStepPosition1);
            pos.Add(Table.WorkerPosition);
            Position = WorkerPositions.DeliveryOrder;
            StartCoroutine(Move.NewMove(WorkerState.DeliveryDish, pos));
        }
    }
}
