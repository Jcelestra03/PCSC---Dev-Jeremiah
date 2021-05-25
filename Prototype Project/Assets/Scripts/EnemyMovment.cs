using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovment : MonoBehaviour
{
    private Rigidbody2D MyRB;
    private Vector2 velocity;
    public bool isfollowing = false;
    public float movementspeed = 5;
    public GameObject playertarget;
    public float health = 3;
    public GameObject Mist;

    // Start is called before the first frame update
    void Start()
    {
        MyRB = GetComponent<Rigidbody2D>();

        playertarget = GameObject.Find("Player");


    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookPos = playertarget.transform.position - transform.position;

        lookPos.Normalize();
        velocity = MyRB.velocity; 
        if(!isfollowing)
        {
            velocity.x = 0;
        }
        if (isfollowing)
        {
            velocity.x = lookPos.x * movementspeed;

            if (velocity.x > 0)
                GetComponent<SpriteRenderer>().flipX = false;
            else if (velocity.x < 0)
                GetComponent<SpriteRenderer>().flipX = true;
        }
        MyRB.velocity = velocity;


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isfollowing && (collision.gameObject.name == "Player"))
            isfollowing = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (isfollowing && (collision.gameObject.name == "Player"))
            isfollowing = false;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.name.Contains("bullet"))
    //    {
    //        health--;
    //        if  (health <= 0)
    //        {
    //            Instantiate(Mist, gameObject.transform.position, Quaternion.identity);
    //            
    //
    //        }
    //    }
    //}

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Die();
        }

    }
    
    void Die()
    {
        Instantiate(Mist, gameObject.transform.position, Quaternion.identity);

        Destroy(gameObject, 0.02f);

    }

    public void Freeze()
    {

    }
}
