using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D myRB;
    private Vector2 velocity;
    public int movementspeed = 4;
    public float jumpheight = 6.5f;
    private Vector2 groundDetection;
    public float groundDetectDistance = .1f;
    private Quaternion zero;
    public bool flip;

    //Health
    public int PHealth;



    //default melee
    public Transform APoint;
    public float ARange = 0.5f;
    public LayerMask enemyLayers;



    public bool shooting;
    //PowerUp1;
    public bool skate;
    //PowerUp2;
    public bool Ram;
    public bool isRamming;
    //PowerUp3;
    public bool Baseball;
    //PowerUp4;
    public bool StopSign;
    //PowerUp5;




    public float bulletLifespan = 1;
    public float bulletspeed = 5;
    public GameObject bullet;
    private Vector2 mouseposition;
    public float ammo;

    public float ramspeed = 50;
    public float timer;
    public float timedifference;

    public float Durability;


    public int attackdamage = 2;

    public bool Stop;

    public bool powerON;

    //ANIMATIONS GO HERE VVVVVVVV
    private Animator MyAnimator;

    //
    public GameObject spawnPoint;
    //






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
        ramspeed = 60;
        timer = 0;
        timedifference = 1.2f;
        //Animation
        MyAnimator = GetComponent<Animator>();
        PHealth = 10;
    }










    // Update is called once per frame
    void Update()
    {
        /////////////////BASIC MOVEMENT
        velocity = myRB.velocity;
        velocity.x = Input.GetAxisRaw("Horizontal") * movementspeed;
        

        groundDetection = new Vector2(transform.position.x, transform.position.y - 1.1f);

        if (Input.GetKeyDown(KeyCode.Space) && Physics2D.Raycast(groundDetection, Vector2.down, groundDetectDistance))
        {
            MyAnimator.SetBool("IsJumping", true);
            velocity.y = jumpheight;
        }


        mouseposition.x = Input.mousePosition.x;
        mouseposition.y = Input.mousePosition.y;


        /////////////////HEALTH AND DAMAGE
        ///



        if (PHealth <= 0)
        {
            PHealth = 10;
            transform.position = spawnPoint.transform.position;
        }



        /////////////////Melee default
        ///



        if (Input.GetKeyDown(KeyCode.E) && powerON == false)
        {
            MyAnimator.SetBool("IsPunching", true);
            Attack();
        }



        /////////////////Melee (Baseball)
        ///



        if (Baseball == true)
        {
            ARange = 0.8f;
            attackdamage = 4;
        }

        else if (Baseball == false)
        {
            ARange = 0.55f;
            attackdamage = 2;
            
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && Baseball == true)
        {
            Attack();

        }

        if (Durability <= 0)
            powerON = false;
        /////////////////StopSign POWER UP
        ///

        if (Input.GetKeyDown(KeyCode.Mouse1) && StopSign == true)
        {
            Stop = true;
            
            timedifference = 3;
        }
        if (StopSign == false)
        {
            Stop = false;
            timer = 0;
        }
        else if (Durability <= 0)
        {
            StopSign = false;
        }
        if (Stop == true)
        {
            timer += Time.deltaTime;
            if (timer > timedifference)
            {
                Durability--;
                Stop = false;
                timer = 0;
            }
        }


        /////////////////SKATE POWER UP
        ///



        if (skate == true)
        {
            movementspeed = 8;
            MyAnimator.SetBool("UsingSkateboard", true);
        }

        else
        {
            movementspeed = 4;
            MyAnimator.SetBool("UsingSkateboard", false);
        }

        if (skate == true && Input.GetKeyDown(KeyCode.E))
        {
            Attack();
        }



        /////////////////FLIP
        ///

        if (myRB.velocity.x < 0)
        {
            //MyAnimator.SetBool("WalkingSide", true);
            flip = true;
            GetComponent<SpriteRenderer>().flipX = true;
            ramspeed = -10;
        }

        else if (myRB.velocity.x > 0)
        {
            //MyAnimator.SetBool("WalkingSide", true);
            flip = false;
            GetComponent<SpriteRenderer>().flipX = false;
            ramspeed = 10;
        }

        else if (myRB.velocity.x == 0)
        {
            MyAnimator.SetBool("WalkingSide", false);
        }
        if (isRamming == false)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                MyAnimator.SetBool("WalkingSide", true);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                MyAnimator.SetBool("WalkingSide", true);
            }
        }



        /////////////////SHOOTING POWER UP
        ///



        if (ammo <= 0)
        {
            shooting = false;
            powerON = false;
        }


        if (shooting == true && (Input.GetKeyDown(KeyCode.Mouse1)) && ammo >= 0)
        {
            ammo = ammo - 1;
            GameObject b = Instantiate(bullet, gameObject.transform);
            //Physics2D.IgnoreCollision(b.GetComponent<CircleCollider2D>(), GetComponent<CircleCollider2D>());
            Physics2D.IgnoreCollision(b.GetComponent<CircleCollider2D>(), GetComponent<PolygonCollider2D>());
            Vector3 lookPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;



            b.GetComponent<Rigidbody2D>().velocity = new Vector2(lookPos.x * bulletspeed, lookPos.y * bulletspeed);
            Destroy(b, bulletLifespan);
        }



        /////////////////SHOPPING CART POWER UP
        ///

        if (Ram == true && Input.GetKeyDown(KeyCode.Mouse1))
        {
            isRamming = true;
            MyAnimator.SetBool("UsingRam", true);
        }
        else if (Ram == false)
            isRamming = false;

        if (isRamming == true)
        {
            movementspeed = 0;
            timer += Time.deltaTime;

            StartCoroutine("Ramming");
            if (timer >= timedifference)
            {
                movementspeed = 0;
                isRamming = false;
                timer = 0;
            }

        }

        else if (isRamming == false && skate == false)
        {
            MyAnimator.SetBool("UsingRam", false);
            //StartCoroutine("cooldownRam");
            movementspeed = 4;
        }

        myRB.velocity = velocity;

    }

















    private void OnCollisionEnter2D(Collision2D collision)
    {

        /////////////////////////PICK UPS
        ///

        if (collision.gameObject.name.Contains("Powerup1"))
        {
            shooting = true;
            powerON = true;
            skate = false;
            Ram = false;
            Baseball = false;
            StopSign = false;
            ammo = 5;
        }


        if (collision.gameObject.name.Contains("Powerup2") && isRamming == false)
        {
            skate = true;
            powerON = true;
            shooting = false;
            Ram = false;
            Baseball = false;
            StopSign = false;
        }

        if (collision.gameObject.name.Contains("Powerup3"))
        {
            Ram = true;
            powerON = true;
            skate = false;
            shooting = false;
            Baseball = false;
            StopSign = false;
        }

        if (collision.gameObject.name.Contains("Powerup4"))
        {
            Baseball = true;
            powerON = true;
            skate = false;
            Ram = false;
            shooting = false;
            StopSign = false;
            Durability = 8;
        }

        if (collision.gameObject.name.Contains("Powerup5"))
        {
            StopSign = true;
            powerON = true;
            shooting = false;
            skate = false;
            Ram = false;
            Baseball = false;
            Durability = 2;

        }

        //////////////////////////PLAYER DAMAGE 
        ///

        if (collision.gameObject.name.Contains("Spirit"))
        {
            PHealth = PHealth - 2;

        }

        if (collision.gameObject.name.Contains("Oni"))
        {
            PHealth = PHealth - 3;
        }

        if (collision.gameObject.name.Contains("Phantom"))
        {
            PHealth = PHealth - 5;
        }

        if (collision.gameObject.name.Contains("Mist"))
        {
            PHealth = PHealth - 1;
        }

    }

    private IEnumerator Ramming()
    {
        while (isRamming == true)
        {
            movementspeed = 0;
            velocity.x = ramspeed;
            myRB.velocity = velocity;
            yield return null;
        }
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(APoint.position, ARange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyMovment>().TakeDamage(attackdamage);
            Debug.Log("We hit" + enemy.name);
            if (Baseball == true)
            {
                Durability--;
            }
        }
    }


    void OnDrawGizmosSelected()
    {
        if (APoint == null)
            return;
        Gizmos.DrawWireSphere(APoint.position, ARange);
    }
}