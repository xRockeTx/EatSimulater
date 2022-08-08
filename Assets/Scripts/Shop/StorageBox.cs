using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageBox : MonoBehaviour
{
    public DishData Data;
    public Storage Storage;
    public int Cost;
    public StorageBoxUI BoxUI;

    public void TakeData(Storage storage, DishData data, int cost)
    {
        Storage = storage;
        Data = data;
        Cost = cost;
        GetComponent<MeshRenderer>().material.color = data.DishColor;
    }
    public void OnMouseUp()
    {
        StartCoroutine(BoxUI.OpenPanel());
    }
    public void Buy()
    {
        Storage.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
