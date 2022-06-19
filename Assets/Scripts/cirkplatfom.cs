using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cirkplatfom : MonoBehaviour
{
    [SerializeField] Transform center;
    [SerializeField] float radius = 2f, angularspeed = 2f;
    float positionX, positionY, angle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        positionX = center.position.x * Mathf.Cos(angle) * radius;
        positionY = center.position.y * Mathf.Sin(angle) * radius;
        transform.position = new Vector2(positionX, positionY);
        angle += Time.deltaTime * angularspeed;

        if (angle >= 360f) {
            angle = 0f;
        }
    }
}
