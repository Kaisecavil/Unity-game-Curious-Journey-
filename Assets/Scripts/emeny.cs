using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emeny : MonoBehaviour
{
    public int maxhealth = 100;
    private int currenthealth;
    bool isdiying = false;
    public GameObject rana;
    private Animator anim;
    private SpriteRenderer sprite;
    
    //private BoxCollider2D collider2D;
    //private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxhealth;
        //sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        if(sprite == null)
        {
            sprite = GetComponentInChildren<SpriteRenderer>();
        }
        //collider2D = GetComponent<BoxCollider2D>();
       
    }


    private void Update()
    {
        if (currenthealth <= 0 && !isdiying)
        {
            Diying();
        }
       
    }
    public void TakeDamage(int damage)
    {
        currenthealth -= damage;
        anim.SetBool("attacked", true);
        StartCoroutine("Rana");
        
       


    }

    public void Diying()
    {
        anim.SetBool("isDiying", true);
        
        StartCoroutine("timetodie");
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
        
        yield return new WaitForSeconds(0.8f);
        anim.SetBool("attacked", false);
    }

    IEnumerator timetodie()
    {
        yield return new WaitForSeconds(3f);
    }

}
