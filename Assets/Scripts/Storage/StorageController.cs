using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageController : MonoBehaviour
{
    public DishData Data;
    public List<Storage> Storages;
    public List<StorageBox> StorageBoxes;
    public GameObject StorageBox;
    public List<int> Cost;

    private void Start()
    {
        foreach (Storage storage in Storages)
        {
            storage.StorageUI.Data = Data;
            storage.gameObject.SetActive(false);
        }
        for (int i = 0; i < Storages.Count; i++)
        {
            GameObject box = Instantiate(StorageBox, transform);
            box.transform.position = Storages[i].transform.position;
            box.GetComponent<StorageBox>().TakeData(Storages[i], Data, Cost[i]);
            StorageBoxes.Add(box.GetComponent<StorageBox>());
        }
    }
}
