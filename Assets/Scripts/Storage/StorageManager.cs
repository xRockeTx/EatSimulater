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
    public Storage GetFreeStorage(DishVariety variety)
    {
        foreach (Storage storage in Storages)
        {
            if (storage.Variety == variety && storage.State == StorageState.Free && storage.gameObject.activeSelf && storage.Amount > 0)
            {
                storage.State = StorageState.InWaiting;
                return storage;
            }
        }
        return new Storage();
    }
}
