using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject OptionPanel;

    public void PlayScene(int i)
    {
        SceneManager.LoadScene(sceneBuildIndex:i);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
