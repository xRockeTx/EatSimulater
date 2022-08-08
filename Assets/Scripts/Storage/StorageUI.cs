using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageUI : MonoBehaviour
{
    public Image DishImage;
    public Text TakeTime;
    public Text Amount;
    public GameObject TakeOrderPanel;
    public Image TakeProgres;

    public Storage Storage;
    [HideInInspector] public DishData Data;

    public void Start()
    {
        Storage.Variety = Data.DishVariety;
        TakeTime.text = Storage.TakeTime.ToString();
        DishImage.color = Data.DishColor;
        Amount.text = Storage.Amount.ToString();

        TakeOrderPanel.SetActive(false);
    }
    public IEnumerator TakeDish(float time, Worker worker)
    {
        DishImage.gameObject.SetActive(false);
        TakeOrderPanel.SetActive(true);

        TakeProgres.fillAmount = 1;
        float t = time;

        while (t > 0)
        {
            t -= time / 20;
            TakeProgres.fillAmount = t / time;
            yield return new WaitForSeconds(time / 20);
        }

        Storage.Amount--;
        Storage.OrderMissions.Add(new Mission(Storage.Variety));
        Amount.text = Storage.Amount.ToString();
        Storage.State = StorageState.Free;
        worker.TakeStorageDish(Data.DishVariety);

        TakeOrderPanel.SetActive(false);
        DishImage.gameObject.SetActive(true);
    }
    public void PickUp()
    {
        Storage.Amount++;
        Storage.OrderMissions.Remove(new Mission(Storage.Variety));
        Amount.text = Storage.Amount.ToString();
    }
}
