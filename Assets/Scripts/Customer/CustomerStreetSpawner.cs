using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerStreetSpawner : MonoBehaviour
{
    public LoadDishData Data;
    public List<DishData> CanOrder;
    public DishData StarterDish;
    public GameObject Customer;
    public TableManager TableManager;
    private void Start()
    {
        CanOrder.Add(StarterDish);
        StartCoroutine(SpawnCustomer());
    }
    private IEnumerator SpawnCustomer()
    {
        while (true)
        {
            foreach (Table table in TableManager.Tables)
            {
                if (table.State == TableState.Free && table.gameObject.activeSelf)
                {
                    table.State = TableState.InWaiting;
                    GameObject customer = Instantiate(Customer, transform);
                    customer.GetComponent<Customer>().TakeData(new Order(CanOrder), table, this);
                    yield return new WaitForSeconds(1f);
                }
            }
            yield return new WaitForSeconds(2f);
        }
    }
    public void SpawnCust(int id,Customer customer)
    {
        TableManager.Tables[id].State = TableState.InWaiting;
        customer.transform.position = this.transform.position;
        customer.GetComponent<Customer>().TakeData(new Order(CanOrder), TableManager.Tables[id],this);
    }
}
