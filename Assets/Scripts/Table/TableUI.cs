using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableUI : MonoBehaviour
{
    public GameObject DishPanel;
    public Image DishImage;
    public Text DishAmount;
    public GameObject TakeOrderPanel;
    public Image TakeProgres;
    public Table Table;
    [HideInInspector] public Order Order;

    public void Start()
    {
        DishPanel.SetActive(false);
        TakeOrderPanel.SetActive(false);
    }
    public IEnumerator TakeOrder(Order order,float time)
    {
        Order = order;
        TakeOrderPanel.SetActive(true);
        TakeProgres.fillAmount = 1;
        time *= order.Amount;
        float t = time;

        Table.State = TableState.TakeOrder;

        while (t > 0)
        {
            t -= time / 20;
            TakeProgres.fillAmount = t / time;
            yield return new WaitForSeconds(time / 20);
        }
        TakeOrderPanel.SetActive(false);
        Table.Worker.StartCoroutine(Table.Worker.FindWork());
        WaitOrder(order);

    }
    public void WaitOrder(Order order)
    {
        DishPanel.SetActive(true);
        DishImage.color = order.Data.DishColor;
        DishAmount.text = order.Amount.ToString();

        Table.State = TableState.WaitDelivery;
    }
    public void DeliveryOrder()
    {
        Order.Amount--;
        DishAmount.text = Order.Amount.ToString();
        if (Order.Amount == 0)
        {
            DishPanel.SetActive(false);
            TakeOrderPanel.SetActive(false);

            List<Transform> pos = new List<Transform>();
            pos.Add(Table.OutStepPosition);
            pos.Add(Table.OutPosition);
            Table.Customer.Move.StartCoroutine(Table.Customer.Move.Move(pos,CustomerState.GoOut));
            return;
        }
    }
    public void NextMove()
    {
        
    }
}
