using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerStreetSpawner : MonoBehaviour
{
    public LoadDishData Data;
    public GameObject Customer;
    public TableManager TableManager;
    private void Start()
    {
        StartCoroutine(SpawnCustomer());

    }
    private IEnumerator SpawnCustomer()
    {
        while (true)
        {
            foreach (Table table in TableManager.Tables)
            {
                if (table.State == TableState.Free)
                {
                    table.State = TableState.InWaiting;
                    GameObject customer = Instantiate(Customer, transform);
                    customer.GetComponent<Customer>().TakeData(new Order(Data.Dishes), table, this);
                    yield return new WaitForSeconds(0.5f);
                }
            }
            yield return new WaitForSeconds(2f);
        }
    }
    public void SpawnCust(int id,Customer customer)
    {
        TableManager.Tables[id].State = TableState.InWaiting;
        customer.transform.position = this.transform.position;
        customer.GetComponent<Customer>().TakeData(new Order(Data.Dishes), TableManager.Tables[id],this);
    }
}
