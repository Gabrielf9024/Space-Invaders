using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool isPlay;
    public bool isQuit;

    public void ButtonAction()
    {
        if (isPlay)
        {
            SceneManager.LoadScene("GameStart");
        }
        if (isQuit)
        {
            Application.Quit();
        }
    }
}
