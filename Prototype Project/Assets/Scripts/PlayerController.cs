using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D myRB;
    private Vector2 velocity;
    public int movementspeed = 3;
    public float jumpheight = 6.5f;
    private Vector2 groundDetection;
    public float groundDetectDistance = .1f;
    private Quaternion zero;
    public bool flip;



    public bool shooting;
    //PowerUp1;
    public bool skate;
    //PowerUp2;
    public bool Ram;
    //PowerUp2;

    public float bulletLifespan = 1;
    public float bulletspeed = 5;
    public GameObject bullet;
    private Vector2 mouseposition;
    public float ammo;

    public float ramspeed;

    public bool powerON;


    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        zero = new Quaternion();
        shooting = false;
        //PowerUp1 = false;
        skate = false;
        //PowerUp2 = false;
        flip = false;
        ramspeed = 2000;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = myRB.velocity;
        velocity.x = Input.GetAxisRaw("Horizontal") * movementspeed;


        groundDetection = new Vector2(transform.position.x, transform.position.y - .51f);

        if (Input.GetKeyDown(KeyCode.Space) && Physics2D.Raycast(groundDetection, Vector2.down, groundDetectDistance))
        {
            
            velocity.y = jumpheight;
        }

        

        mouseposition.x = Input.mousePosition.x;
        mouseposition.y = Input.mousePosition.y;

        if (skate == true)
            movementspeed = 5;
        else
            movementspeed = 3;

        myRB.velocity = velocity;

        if (myRB.velocity.x < 0)
        {
            flip = true;
            ramspeed = -4000;
        }

        else if (myRB.velocity.x > 0)
        {
            flip = false;
            ramspeed = 4000;
        }


        if (ammo <= 0)
            shooting = false;

        if (shooting == true && (Input.GetKeyDown(KeyCode.Mouse1)) && ammo >= 0)
        {
            ammo = ammo - 1;
            GameObject b = Instantiate(bullet, gameObject.transform);
            Physics2D.IgnoreCollision(b.GetComponent<CircleCollider2D>(), GetComponent<CircleCollider2D>());

            Vector3 lookPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;



            b.GetComponent<Rigidbody2D>().velocity = new Vector2(lookPos.x * bulletspeed, lookPos.y * bulletspeed);
            Destroy(b, bulletLifespan);
        }

        if (Ram == true && Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (flip == true)
            {
                myRB.AddForce(transform.right * ramspeed, ForceMode2D.Force);
            }

            if (flip == false)
            { 
                myRB.AddForce(transform.right * ramspeed, ForceMode2D.Force);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Powerup1"))
        {
            shooting = true;
            powerON = true;
            skate = false;
            Ram = false;
            ammo = 5;
        }

       
        if(collision.gameObject.name.Contains("Powerup2"))
        {
            skate = true;
            powerON = true;
            shooting = false;
            Ram = false;
        }

        if (collision.gameObject.name.Contains("Powerup3"))
        {
            Ram = true;
            powerON = true;
            skate = false;
            shooting = false;
        }
    }
}
