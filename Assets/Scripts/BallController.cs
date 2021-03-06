using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    public Rigidbody2D rb2D;
    public static float speed = 500f;
    public float multiplier = 1.05f;
    public AudioSource laserToc;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other) {
        rb2D.AddForce(other.contacts[0].normal * speed);
        if(other.gameObject.tag == "Player"){
            laserToc.Play();
            if(speed <= 10000f){
                speed = speed * multiplier;
            }
        }
    }
}
