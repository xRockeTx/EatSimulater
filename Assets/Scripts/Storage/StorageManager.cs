using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageManager : MonoBehaviour
{
    public List<Storage> Storages;

    public bool HaveFreeStorage(DishVariety variety)
    {
        foreach (Storage storage in Storages)
        {
            if (storage.Variety == variety && storage.State == StorageState.Free && storage.gameObject.activeSelf&& storage.Amount>0)
            {
                return true;
            }
        }
        return false;
    }
    public int GetFreeMachines(DishVariety variety)
    {
        foreach (Storage storage in Storages)
        {
            if (storage.Variety == variety && storage.State == StorageState.Free && storage.gameObject.activeSelf && storage.Amount > 0)
            {
                return Storages.IndexOf(storage);
            }
        }
        return 0;
    }
}
