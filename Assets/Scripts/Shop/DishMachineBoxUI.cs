using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DishMachineBoxUI : MonoBehaviour
{
    public DishMachineBox Box;

    public GameObject Panel;
    public Text Cost;


    private void Start()
    {
        Panel.SetActive(false);
        Cost.text = Box.Cost.ToString();    
    }
    public IEnumerator OpenPanel()
    {
        Panel.SetActive(true);
        yield return new WaitForSeconds(6);
        Panel.SetActive(false);
    }
    public void Buy()
    {
        Panel.SetActive(false);
        Box.Buy();
    }
}
