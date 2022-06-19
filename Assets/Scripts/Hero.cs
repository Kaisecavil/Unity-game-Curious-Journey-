using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    public GameOverScreen gameOverScreen;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float lives = 5;
    public float maxlives;
    [SerializeField] private float jumpForce = 7f;
    public bool isGrounded = false;
    private bool isInvincible = false;
    private bool isAttacking = false;
    private bool isRecharged = true;
    private bool isDiying = false;
    private bool isSliding = false;
    public int heroDamage = 30;
    private Vector2 vel;
    //private int Njmp = 0;
    private Animator anim;
    public Scrollbar scrollbarhp;
    private int lifetime = 0;


    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    public Transform AttackPoint;
    public float attackrange = 0.5f;
    public LayerMask enemyLayers;


    public static Hero Instance { get; set; }
    private States State
    {
        get { return (States)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();//Childer
        anim = GetComponent<Animator>();
        scrollbarhp.size = lives;
        maxlives = lives;

    }

    private void Run()
    {
        if (isGrounded) State = States.run;
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
        
        
    }

    private void Jump(float jmpmul) {
        rb.AddForce(transform.up * jumpForce * jmpmul , ForceMode2D.Impulse);
        isAttacking = false;
        isRecharged = true;
    }


    private void Attack()
    {
        
        
        if (isRecharged)
        {
            Debug.Log("attack");
            
            State = States.attack;
            isAttacking = true;
            isRecharged = false;
        }
        else Debug.Log("not recharged");

    }

    private void Attackhit()
    {
        Collider2D[] hitenemyes = Physics2D.OverlapCircleAll(AttackPoint.position, attackrange, enemyLayers);

        foreach (Collider2D enemy in hitenemyes)
        {
            Debug.Log("we hit " + enemy.name);
            enemy.GetComponent<emeny>().TakeDamage(heroDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;
        Gizmos.DrawWireSphere(AttackPoint.position, attackrange);

    }

    public void EndAttack()
    {
        isAttacking = false;
        isRecharged = true;
        State = States.idle;
    }


    private void CheckGruond() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        Collider2D[] collidersfar = Physics2D.OverlapCircleAll(transform.position, 2.0f);
        isGrounded = colliders.Length > 1;
        isSliding = colliders.Length < collidersfar.Length;
        vel = rb.velocity;
        //if (!isGrounded) State = States.jump;

        if (!isGrounded && !isDiying)
        {
            if (vel.y > 0) {
                State = States.jump;
                
          
            }
            if (vel.y < 0) {
                State = States.prefall;
              
            }
            
            if (vel.y < -89) State = States.fall;


        }


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!isDiying && this)
        {
            if (isGrounded && !isAttacking) State = States.idle;
            if (Input.GetButton("Horizontal") && !isAttacking)
            {
                Run();
            }
            if ((Input.GetButtonDown("Jump") || Input.GetKeyDown("w")) && isGrounded)
            {
                Jump(1f);
            }
            if (Input.GetMouseButtonDown(0) && isGrounded)
            {
                Attack();
            }
            if (Input.GetKeyDown("s") && isGrounded)
            {
                Debug.Log("crouch");
            }
        }

    }

    private void FixedUpdate()
    {
        if (!isDiying && this) 
        {
            CheckGruond();
            lifetime++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("moving_platform"))
        {
            this.transform.parent = collision.transform;
        }

        if (collision.gameObject.CompareTag("inter"))
        {
            collision.gameObject.GetComponent<bridge>().Interact();
            Debug.Log("interact");
        }



    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("spikes") && !isDiying)
        {
            getDamage(1);
            
        }

        if (collision.gameObject.CompareTag("Enemy") && !isDiying)
        {
            getDamage(1);

        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isDiying)
        {
            getDamage(1);
            
        }
        
    }

    



    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("moving_platform"))
        {
            this.transform.parent = null;
        }
    }

    public void getDamage(int damage)
    {
        if (!isInvincible)
        {
            lives -= damage;
            scrollbarhp.size = lives/maxlives;
            Debug.Log("lives=" + lives);
            State = States.idle;
            if (lives <= 0)
            {
                Diying();
            }
            if (!isDiying)
            {
                 StartCoroutine("Nongetingdamage");
                 Jump(0.7f);
            } 
            
        }
    }


    private void Diying()
    {
        isDiying = true;
        speed = 0;
        jumpForce = 0;
        State = States.die;
    }
    public void Die()
    {
        GameOver();
        Destroy(this.gameObject);
    }

    public void GameOver()
    {
        gameOverScreen.Setup(lifetime);
    }
    

    IEnumerator Nongetingdamage()
    {
        float timer = 0;
        setisInvincible(true);
        while (timer < 2f)
        {
            sprite.enabled = !sprite.enabled;
            yield return new WaitForSeconds(0.15f);
            timer += 0.15f;
        }

        setisInvincible(false);
    }

    public bool getisInvincible()
    {
        return isInvincible;
    }

    public void setisInvincible(bool s)
    {
        isInvincible = s;
    }

    

}

public enum States
{
    idle,
    run,
    jump,
    prefall,
    fall,
    attack,
    die

}


