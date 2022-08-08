using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableBoxUI : MonoBehaviour
{
    public TableBox TableBox;

    public GameObject Panel;
    public Text Cost;


    private void Start()
    {
        Cost.text = TableBox.Cost.ToString();
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
        TableBox.Buy();
    }
}
