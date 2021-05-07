using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D MyRB;
    private Vector2 velocity;
    public int movmentspeed = 3;
    public float jumpheight = 6.5f;
    private Vector2 groundDetection;
    public float groundDetectDistance = .1f;
    private Quaternion zero;
    public bool shooting = false;
    public bool PowerUp1 = false;

    public float bulletLifespan = 1;
    public float bulletspeed = 5;
    public GameObject bullet;
    private Vector2 mouseposition;

    // Start is called before the first frame update
    void Start()
    {
        MyRB = GetComponent<Rigidbody2D>();
        zero = new Quaternion();

    }

    // Update is called once per frame
    void Update()
    {
        velocity = MyRB.velocity;
        velocity.x = Input.GetAxisRaw("Horizontal") * movmentspeed;


        groundDetection = new Vector2(transform.position.x, transform.position.y - .51f);

        if (Input.GetKeyDown(KeyCode.Space) && Physics2D.Raycast(groundDetection, Vector2.down, groundDetectDistance))
        {
            
            velocity.y = jumpheight;
        }

        mouseposition.x = Input.mousePosition.x;
        mouseposition.y = Input.mousePosition.y;

        MyRB.velocity = velocity;


        if (shooting == true && (Input.GetKeyDown(KeyCode.Mouse1)))
        {

            GameObject b = Instantiate(bullet, gameObject.transform);
            Physics2D.IgnoreCollision(b.GetComponent<CircleCollider2D>(), GetComponent<CircleCollider2D>());

            Vector3 lookPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;



            b.GetComponent<Rigidbody2D>().velocity = new Vector2(lookPos.x * bulletspeed, lookPos.y * bulletspeed);
            Destroy(b, bulletLifespan);
        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Powerup1")
            PowerUp1 = true;
       


    }
}
