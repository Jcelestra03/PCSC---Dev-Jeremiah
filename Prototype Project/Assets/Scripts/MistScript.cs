using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistScript : MonoBehaviour
{

    public float Lifespan = 5;
    public float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= Lifespan)
            Destroy(gameObject);



    }
}
