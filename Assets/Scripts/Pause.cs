using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Pause : MonoBehaviour
{

    public RawImage hud;
    public RawImage hudSelect;
    // public GameObject player;

    private Rigidbody2D ballRb2D;
    private Vector2 ballRb2DVelocity;
    private float ballRb2DAngularVelocity;
    private GameObject ball;
    private bool isPaused;
    private float ballSpeed;
    private MoveBar.inputTypes playerCurrentType;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        ball = GameObject.Find("Ball");
        ballRb2D = ball.GetComponent<Rigidbody2D>();
        // BallController ballScript = ball.GetComponent<BallController>();
        // MoveBar playerScript = player.GetComponent<MoveBar>();

        hud.gameObject.SetActive(false);
        hudSelect.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        // if(ball == null){
        //     ball = GameObject.Find("Ball(clone)");
        //     ballRb2D = ball.GetComponent<Rigidbody2D>();
        // }
        
        if(Input.GetKeyDown(KeyCode.Space)){
            isPaused = !isPaused;
            if(isPaused){
                // BallController ballSpeed;
                // MoveBar playerCurrentType;
                // playerCurrentType = playerScript.currentInput;
                // ballSpeed = ballScript.speed;
                playerCurrentType = MoveBar.currentInput;
                ballRb2DVelocity = ballRb2D.velocity;
                ballRb2DAngularVelocity = ballRb2D.angularVelocity;

                MoveBar.currentInput = MoveBar.inputTypes.None;
                ballRb2D.velocity = Vector2.zero;
                ballRb2D.angularVelocity = 0f;

                hud.gameObject.SetActive(true);
                hudSelect.gameObject.SetActive(true);
            }else{
                if(MoveBar.currentInput == MoveBar.inputTypes.None){
                    MoveBar.currentInput = playerCurrentType;
                }
                if(ballRb2D.angularVelocity == 0f){
                    
                    ballRb2D.angularVelocity = ballRb2DAngularVelocity;
                }
                
                hud.gameObject.SetActive(false);
                hudSelect.gameObject.SetActive(false);
            }
        }
    }
}

/*

//// VALEURS POSITION HUDSELECT ////

/// POSITION 1 (Reprendre)
    115 (PosY)

/// POSITION 2 (Rejouer)
    -53 (PosY)

/// POSITION 3 (Quitter)
    -220 (PosY)

*/