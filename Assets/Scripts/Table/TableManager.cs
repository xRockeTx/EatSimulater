using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public List<Table> Tables;
    public List<int> Cost;
    public List<TableBox> TableBoxes;
    public GameObject TableBox;
    private void Awake()
    {
        for (int i = 1; i < Tables.Count; i++)
        {
            GameObject box = Instantiate(TableBox, transform);
            box.transform.position = Tables[i].transform.position;
            Tables[i].gameObject.SetActive(false);
            box.GetComponent<TableBox>().TakeData(Tables[i],Cost[i]);
            TableBoxes.Add(box.GetComponent<TableBox>());
        }
    }
}
