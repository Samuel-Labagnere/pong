using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FailZone : MonoBehaviour
{

    private Rigidbody2D ballRb2D;

    public GameObject ball;
    public AudioSource looseSound;
    public GameObject spawnPoint;
    public Text lifesTxt;
    public GameObject looseTxt;

    // Start is called before the first frame update
    void Start()
    {
        Score.lifes = 3;
        looseTxt.SetActive(false);
        ballRb2D = ball.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Score.lifes == 0){
            lifesTxt.text = "Vies : FIN";
            StartCoroutine("End");
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Ball" && Score.lifes > 0){
            IEnumerator coroutine = BallInstantiate(other);
            looseSound.Play();  
            Score.lifes -= 1;
            StartCoroutine(coroutine);
        }
    }

    IEnumerator End(){
        looseTxt.SetActive(true);
        ballRb2D.velocity = Vector2.zero;
        ballRb2D.angularVelocity = 0f;
        int temp = MoveBar.inputNumber;
        MoveBar.inputNumber = 2;
        yield return new WaitForSeconds(3);
        MoveBar.inputNumber = temp;
        SceneManager.LoadScene("Menu");
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
