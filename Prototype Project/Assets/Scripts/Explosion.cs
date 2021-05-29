using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    public float LifeSpan = 2;
    public float timer = 0;


    private AudioSource speaker;

    public AudioClip Texplosion;


    void Start()
    {

        speaker = GetComponent<AudioSource>();

        speaker.clip = Texplosion;
        speaker.Play();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= LifeSpan)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains ("Player"))
        {
            GameObject.Find("Player").GetComponent<PlayerController>().PHealth--;
        }
    }
}
