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
    public int GetFreeMachines(DishVariety variety)
    {
        foreach (DishMachine machine in Machines)
        {
            if (machine.Variety == variety && machine.State == DishMachineState.Free && machine.gameObject.activeSelf)
            {
                return Machines.IndexOf(machine);
            }
        }
        return 0;
    }
}
