using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailZone : MonoBehaviour
{

    private Rigidbody2D ballRb2D;

    public GameObject ball;
    public AudioSource looseSound;
    public GameObject spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        Score.lifes = 3;
        ballRb2D = ball.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Score.lifes <= 0){
            SceneManager.LoadScene("Menu");
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Ball"){
            IEnumerator coroutine = BallInstantiate(other);
            looseSound.Play();  
            Score.lifes -= 1;
            StartCoroutine(coroutine);
        }
    }

    IEnumerator BallInstantiate(Collider2D other){
        yield return new WaitForSeconds(1);
        ballRb2D.velocity = Vector2.zero;
        ballRb2D.angularVelocity = 0f;

        ball.transform.position = spawnPoint.transform.position;

        // Destroy(other.gameObject);
        // Instantiate(ball, new Vector2(-1, 0), Quaternion.identity);
    }
}
