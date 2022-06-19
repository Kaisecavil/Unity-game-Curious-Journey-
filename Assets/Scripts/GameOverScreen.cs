using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text pointsText;
    public void Setup(int score) {
        gameObject.SetActive(true);
        pointsText.text = score.ToString() + "POINTS";
    }

    public void RestartButton()
    {
        
        SceneManager.LoadScene("Level 1");
        //gameObject.SetActive(false);
    }

    public void BackButton()
    {
        SceneManager.LoadScene(0);
    }
}
