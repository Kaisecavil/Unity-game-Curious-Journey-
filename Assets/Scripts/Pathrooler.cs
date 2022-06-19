using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathrooler : MonoBehaviour
{
    //public int maxhealth = 200;
    // private int currenthealth;
    // public GameObject rana;
    public float attackrange;
    public int damage = 5;
    public float speed = 3f;
    public float attackdist = 4f;
    public float rangeOfPatrool = 40f;
    public Transform pointOfPatrool;
    public float stopdist = 30f;
    private bool isMovingRight = false;
    private Rigidbody2D rb;
    public LayerMask herolayer;
    public BoxCollider2D collider2D;
    private SpriteRenderer sprite;
    private Animator anim;
    public Transform AttackPoint;
    bool isAttacking = false;
    bool isRecharged = true;
    bool notstay = false;
    bool chill = false;
    bool angry = false;
    bool goback = false;
    bool isedge = false;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        //currenthealth = maxhealth;
        rb = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
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

            if (chill == true && !isAttacking)
            {
                Chill();
            }
            else if (goback == true && !isAttacking)
            {
                GoBack();
            }
            if (angry == true && !isAttacking)
            {
                Angry();
            }


            transform.position = new Vector3(transform.position.x, transform.position.y, -786f);
        }

    }


    private void Attack()
    {
        if (isRecharged)
        {
            Debug.Log("attack");

            anim.SetBool("isAttacking", true);
            isAttacking = true;
            isRecharged = false;
        }
        else Debug.Log("not recharged");
    }

    private void Attackhit()
    {
        Collider2D[] hitHero = Physics2D.OverlapCircleAll(AttackPoint.position, attackrange, herolayer);

        foreach (Collider2D hero in hitHero)
        {
            Debug.Log("we hit " + hero.name);
            hero.GetComponent<Hero>().getDamage(damage);
        }
    }

    public void EndAttack()
    {
        isAttacking = false;
        isRecharged = true;
        angry = true;
        
    }

    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;
        Gizmos.DrawWireSphere(AttackPoint.position, attackrange);

    }

    void Chill()
    {
        if(transform.position.x > pointOfPatrool.position.x + rangeOfPatrool)
        {
            isMovingRight = false;
        } 
        else if(transform.position.x < pointOfPatrool.position.x - rangeOfPatrool)
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
        anim.SetBool("isAttacking", false);
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
        anim.SetBool("isAttacking", false);
    }

    /*
    public void TakeDamage(int damage)
    {
        currenthealth -= damage;
        if (currenthealth <= 0)
        {
            Diying();
        }
        StartCoroutine("Rana");


    }

    public void Diying()
    {
        anim.SetBool("isDiying", true);
    }



    private void Die()
    {
        Debug.Log("enemy died");
        Destroy(this.gameObject);
    }

    IEnumerator Rana()
    {
        rana.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        rana.SetActive(false);
        sprite.enabled = false;
        yield return new WaitForSeconds(0.8f);
        sprite.enabled = true;
    }*/

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
}
