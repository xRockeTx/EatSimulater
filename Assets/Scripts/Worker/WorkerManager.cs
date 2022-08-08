using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerManager : MonoBehaviour
{
    public List<Worker> Workers=new List<Worker>();
    public TableManager TableManager;
    public DishMachineManager DishMachineManager;
    public StorageManager StorageManager;
    public Transform Pos1, Pos2;
    public GameObject Worker;
    public int WorkerCount;

    private void Start()
    {
        for(int i =0; i < WorkerCount; i++)
        {
            InstantiateNewWorker();
        }      
    }
    public void InstantiateNewWorker()
    {
        GameObject g = Instantiate(Worker,transform);
        Worker worker = g.GetComponent<Worker>();
        worker.TakeData(TableManager, DishMachineManager,StorageManager,Pos1,Pos2);
        Workers.Add(worker);
    }
}
