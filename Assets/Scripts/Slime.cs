using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float rangeOfPatrool = 40f;
    public Transform pointOfPatrool;
    public float attackdist=10f;
    public float stopdist = 30f;
    public float speed = 5f;
    public float jumpForce = 15f;
    private bool isMovingRight = true;
    private Rigidbody2D rb;
    Transform player;
    public int damage = 1;
    //public LayerMask herolayer;
    public BoxCollider2D collider2D;
    private SpriteRenderer sprite;
    private Animator anim;
    Vector2 vel;
    public float coolDownTime=3f;
    bool isJumping = false;
    bool isRecharged = true;
    bool notstay = false;
    bool chill = false;
    bool angry = false;
    bool goback = false;
    bool isedge = false;
    bool isGrounded = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        vel = rb.velocity;
    }

    void Update()
    {
        if (!isedge)
        {
            if (Vector2.Distance(transform.position, pointOfPatrool.position) < rangeOfPatrool && angry == false)
            {
                chill = true;
                goback = false;

            }
            if (Vector2.Distance(transform.position, player.position) < stopdist)
            {
                angry = true;
                chill = false;
                goback = false;
            }
            if (Vector2.Distance(transform.position, player.position) > stopdist && !chill)
            {
                goback = true;
                angry = false;

            }
            if (Vector2.Distance(transform.position, player.position) < attackdist)
            {

                angry = true;
                chill = false;
                goback = false;
                Attack();

            }


            if ((Vector2.Distance(transform.position, pointOfPatrool.position) > rangeOfPatrool - 1f && chill && !notstay))
            {
                StartCoroutine("Stay");
            }

            if (chill == true)
            {
                Chill();
            }
            else if (goback == true)
            {
                GoBack();
            }
            if (angry == true)
            {
                Angry();
            }


            transform.position = new Vector3(transform.position.x, transform.position.y, -786f);
        }

    }


    private void Attack()
    {
        if (isRecharged && isGrounded)
        {
           // Debug.Log("attack");

            anim.SetBool("isJumping", true);
            isJumping = true;
            isRecharged = false;
            Jump();
            StartCoroutine("cooldown");
        }
        //else Debug.Log("not recharged");
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce);
        rb.AddForce(transform.right * speed);
    }
    /*
    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("we hit Hero");
            GetComponent<Hero>().getDamage(damage);
        }
    }*/


    private void CheckGruond()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = colliders.Length > 1;
        vel = rb.velocity;

        if (isGrounded)
        {
            isJumping = false;
            //isRecharged = true;
            //angry = true;
            anim.SetBool("isJumping", false);
        }
    }



    void Chill()
    {
        if (transform.position.x > pointOfPatrool.position.x + rangeOfPatrool)
        {
            isMovingRight = false;
        }
        else if (transform.position.x < pointOfPatrool.position.x - rangeOfPatrool)
        {
            isMovingRight = true;
        }

        if (isMovingRight)
        {
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, -786f);

        }
        else
        {
            transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, -786f);
        }

        sprite.flipX = isMovingRight;
        
    }



    void Angry()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        sprite.flipX = transform.position.x < player.position.x;

    }

    void GoBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, pointOfPatrool.position, speed * Time.deltaTime);
        sprite.flipX = transform.position.x < pointOfPatrool.position.x;
        
    }

    IEnumerator Stay()
    {
        isedge = true;
        Debug.Log("edge");
        anim.SetBool("isStaying", true);
        yield return new WaitForSeconds(1.3f);
        anim.SetBool("isStaying", false);
        isedge = false;
        notstay = true;
        yield return new WaitForSeconds(1.3f);
        notstay = false;
    }

    IEnumerator cooldown()
    {

        yield return new WaitForSeconds(coolDownTime);
        isRecharged = true;
    }

    private void FixedUpdate()
    {
        CheckGruond();
    }
}
