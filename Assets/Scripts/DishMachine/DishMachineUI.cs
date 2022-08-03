using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DishMachineUI : MonoBehaviour
{
    public Image DishImage;
    public Text TakeTime;
    public GameObject TakeOrderPanel;
    public Image TakeProgres;

    public DishMachine DishMachine;
    public DishData Data;

    public void Start()
    {
        DishMachine.Variety = Data.DishVariety;
        TakeTime.text = DishMachine.TakeTime.ToString();
        DishImage.color = Data.DishColor;

        TakeOrderPanel.SetActive(false);
    }
    public IEnumerator TakeDish(float time,Worker worker)
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

        DishMachine.State = DishMachineState.Free;
        worker.TakeDish(Data.DishVariety);

        TakeOrderPanel.SetActive(false);
        DishImage.gameObject.SetActive(true);
    }
}
