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


    private float timer;
    private float timedifference = 2.5f;


    public bool enemy1;
    public bool enemy2;
    public bool enemy3;



    private AudioSource speaker;
    public AudioClip enemyhurt;
    public AudioClip cry1;
    public AudioClip cry2;
    public AudioClip cry3;
    public AudioClip attack1;
    public AudioClip attack2;
    public AudioClip attack3;
    public AudioClip diecry;


    // Start is called before the first frame update
    void Start()
    {
        speaker = GetComponent<AudioSource>();


        MyRB = GetComponent<Rigidbody2D>();

        playertarget = GameObject.Find("Player");
        timer = 0;
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
                speaker.clip = cry1;
                speaker.Play();
            }
        }

        if (enemy2 == true)
        {
            timer += Time.deltaTime;

            if (timer >= timedifference)
            {
                speaker.clip = cry2;
                speaker.Play();
            }

        }

        if (enemy3 == true)
        {
            timer += Time.deltaTime;

            if (timer >= timedifference)
            {
                speaker.clip = cry3;
                speaker.Play();
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

        if (collision.gameObject.name.Contains("Player"))
        {

            if (enemy1 == true)
            {

                    speaker.clip = attack1;
                    speaker.Play();

            }

            if (enemy2 == true)
            {

                speaker.clip = attack2;
                speaker.Play();

            }

            if (enemy3 == true)
            {

                speaker.clip = attack3;
                speaker.Play();

            }
        }

            if (collision.gameObject.name.Contains("bullet"))
            {
                health--;
                if  (health <= 0)
                {

                    Die();
                
                }


            }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        speaker.clip = enemyhurt;
        speaker.Play();

        if(health <= 0)
        {
            Die();
        }

    }
    
    void Die()
    {


        Instantiate(Mist, gameObject.transform.position, Quaternion.identity);




        speaker.clip = diecry;

        speaker.Play();

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
