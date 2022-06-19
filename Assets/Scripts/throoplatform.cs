using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throoplatform : MonoBehaviour
{
    private PlatformEffector2D effector2D;
    // Start is called before the first frame update
    void Start()
    {
        effector2D = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            effector2D.rotationalOffset = 180;
        }
        if (Input.GetKeyUp("s"))
        {
            effector2D.rotationalOffset = 0;
        }
    }
}
