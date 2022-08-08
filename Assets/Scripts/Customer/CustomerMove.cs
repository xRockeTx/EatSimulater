using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMove : MonoBehaviour
{
    public float MoveSpeed = 3;
    [HideInInspector] public Customer Customer;

    public IEnumerator Move(List<Transform> pos,CustomerState state)
    {
        int id = 0;
        while (id < pos.Count)
        {
                transform.Translate((pos[id].position - transform.position).normalized * MoveSpeed * Time.deltaTime, Space.World);
                if (Vector3.Distance(transform.position, pos[id].position) < 0.1)
                {
                    id++;
                }
            yield return new WaitForSeconds(Time.deltaTime/2);
        }

        switch (state)
        {
            case CustomerState.TakeOrder:  
                Customer.TakeOrder();
                break;
            case CustomerState.GoOut:
                Customer.Spawner.SpawnCust(Customer.Spawner.TableManager.Tables.IndexOf(Customer.Table), Customer);
                break;
        }
        
    }
}
