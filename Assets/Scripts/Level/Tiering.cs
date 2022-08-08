using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tiering : MonoBehaviour
{
    public void NextLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void PreviousLevel()
    {
        SceneManager.LoadScene(0);
    }
}
