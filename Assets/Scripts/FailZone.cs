using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailZone : MonoBehaviour
{

    public GameObject ball;
    public AudioSource looseSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Ball"){
            IEnumerator coroutine = BallInstantiate(other);
            looseSound.Play();  
            StartCoroutine(coroutine);
        }
    }

    IEnumerator BallInstantiate(Collider2D other){
        yield return new WaitForSeconds(1);
        Destroy(other.gameObject);
        Instantiate(ball, new Vector2(-1, 0), Quaternion.identity);
    }
}
