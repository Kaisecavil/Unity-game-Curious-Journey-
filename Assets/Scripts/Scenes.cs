using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public void LoadScenes(int ind)
    {
        SceneManager.LoadScene(ind);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
