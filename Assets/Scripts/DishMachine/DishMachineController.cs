using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishMachineController : MonoBehaviour
{
    public DishData Data;
    public List<DishMachine> Machines;
    public List<DishMachineBox> DishMachineBoxes;
    public GameObject DishMachineBox;
    public List<int> Cost;
    public CustomerStreetSpawner CustomerStreetSpawner;

    private void Awake()
    {
        foreach(DishMachine machine in Machines)
        {
            machine.DishMachineUI.Data = Data;
            machine.gameObject.SetActive(false);
        }
        for (int i = 0; i < Machines.Count; i++)
        {
            GameObject box = Instantiate(DishMachineBox, transform);
            box.transform.position = Machines[i].transform.position;
            box.GetComponent<DishMachineBox>().TakeData(Machines[i],Data, CustomerStreetSpawner,Cost[i]);
            DishMachineBoxes.Add(box.GetComponent<DishMachineBox>());
        }
    }
}
