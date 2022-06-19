using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridge : MonoBehaviour
{
    public static Hero Instance { get; set; }
    public void Interact()
    {
        GetComponent<Animator>().SetTrigger("Open");
    }
}
