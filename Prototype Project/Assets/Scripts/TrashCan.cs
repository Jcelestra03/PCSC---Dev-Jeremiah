using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public GameObject Explosion;
    public float health = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        



    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
      if(collision.gameObject.name == "Player" || collision.gameObject.name.Contains ("Oni") || collision.gameObject.name.Contains ("Phantom") || collision.gameObject.name.Contains ("Spirit"))
      {
            Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);

            Destroy(gameObject, 0.02f);
      }
        if (collision.gameObject.name.Contains("bullet"))
            health--;

        

    }


}
