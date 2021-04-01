using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    public Rigidbody2D rb2D;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 500f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
           rb2D.AddForce(transform.up * speed);
        }
    }
}
