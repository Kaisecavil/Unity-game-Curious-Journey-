using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class warp : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int nextLevelIndex;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextLevelIndex);
        }
    }
}
