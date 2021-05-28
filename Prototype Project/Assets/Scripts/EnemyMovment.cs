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
    public bool stopped;
    public bool enemy1;
    public bool enemy2;
    public bool enemy3;

    //enemysounds
    public AudioClip enemyCrying;
    public AudioClip enemyAttack;
    public AudioClip heavyCrying;
    public AudioClip heavyAttack;
    public AudioClip speedyCrying;
    public AudioClip speedyAttack;
    public AudioClip enemyDeath;
    private float timer;
    private float timedifference;
    private AudioSource Speaker;


    // Start is called before the first frame update
    void Start()
    {
        MyRB = GetComponent<Rigidbody2D>();

        playertarget = GameObject.Find("Player");

        timer = 0;

        timedifference = 3;

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


        if (GameObject.Find("Player").GetComponent<PlayerController>().Stop == false)
        {
            stopped = false;
        }

        if (enemy1 == true)
        {
            timer += Time.deltaTime;

            if (timer >= timedifference)
            {
                Speaker.clip = enemyCrying;
                Speaker.play();
                timer = 0;
            }
        }

        if (enemy2 == true)
        {
            timer += Time.deltaTime;

            if (timer >= timedifference)
            {
                Speaker.clip = heavyCrying;
                Speaker.play();
                timer = 0;
            }
        }

        if (enemy3 == true)
        {
            timer += Time.deltaTime;

            if (timer >= timedifference)
            {
                Speaker.clip = speedyCrying;
                Speaker.play();
                timer = 0;
            }
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isfollowing && (collision.gameObject.name == "Player"))
            isfollowing = true;

        if (collision.gameObject.name.Contains("Radar") && GameObject.Find("Player").GetComponent<PlayerController>().Stop == true)
        {
            stopped = true;
            StartCoroutine("Freeze");

        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (isfollowing && (collision.gameObject.name == "Player"))
            isfollowing = false;

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("bullet"))
        {
            health--;
            if  (health <= 0)
            {
                Die();
                
            }


        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("player"))
        {
            if (enemy1 == true)
            {
                Speaker.clip = enemyDeath;
                Speaker.play();
            }
        
        }

    }

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

        Speeker.clip = enemyDeath;
        Speaker.play();
        

        Destroy(gameObject, 0.02f);

    }

    private IEnumerator Freeze()
    {


        while (stopped == true)
        {

            isfollowing = false;

            yield return null;
        }

        
    }


}
