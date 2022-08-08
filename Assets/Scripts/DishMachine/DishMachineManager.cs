using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishMachineManager : MonoBehaviour
{
    public List<DishMachine> Machines;

    public bool HaveFreeMachines(DishVariety variety)
    {
        foreach(DishMachine machine in Machines)
        {
            if (machine.Variety == variety&&machine.State==DishMachineState.Free&&machine.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }
    public DishMachine GetFreeMachines(DishVariety variety)
    {
        foreach (DishMachine machine in Machines)
        {
            if (machine.Variety == variety && machine.State == DishMachineState.Free && machine.gameObject.activeSelf)
            {
                machine.State = DishMachineState.InWaiting;
                return machine;
            }
        }
        return new DishMachine();
    }
}
