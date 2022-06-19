using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingplat : MonoBehaviour
{   
    [SerializeField] float speedX = 3f, speedY = 3f, rangeX = 4f, rangeY = 4f;
    float dirX;
    bool movingRight ;
    bool movingUp;
    // Start is called before the first frame update
    void Start()
    {
        movingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (speedX != 0f)
        {
            if (transform.position.x > rangeX)
            {
                movingRight = false;
            }
            else if (transform.position.x < -rangeX)
            {
                movingRight = true;
            }

            if (movingRight)
            {
                transform.position = new Vector2(transform.position.x + speedX * Time.deltaTime, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(transform.position.x - speedX * Time.deltaTime, transform.position.y);
            }
        }

        if (speedY != 0f) {
            if (transform.position.y > rangeY)
            {
                movingUp = false;
            }
            else if (transform.position.y < -rangeY)
            {
                movingUp = true;
            }

            if (movingUp)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + speedY * Time.deltaTime);
            }
            else
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - speedY * Time.deltaTime);
            }
        }


        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Hero") && speedX == 0)
            {
                speedX = 3f;
            }
        }
    }
}
