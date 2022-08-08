using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableBox : MonoBehaviour
{
    public Table Table;
    public int Cost;
    public TableBoxUI BoxUI;

    public void TakeData(Table table, int cost)
    {
        Table = table;
        Cost = cost;
    }
    public void OnMouseUp()
    {
        BoxUI.OpenPanel();
    }
    public void Buy()
    {
        Table.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
