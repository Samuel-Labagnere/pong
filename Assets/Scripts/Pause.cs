using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Pause : MonoBehaviour
{

    public RawImage hud;
    public RawImage hudSelect;
    // public GameObject player;

    private GameObject ball;
    private bool isPaused;
    private float ballSpeed;
    private MoveBar.inputTypes playerCurrentType;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        ball = GameObject.Find("Ball");
        // BallController ballScript = ball.GetComponent<BallController>();
        // MoveBar playerScript = player.GetComponent<MoveBar>();

        hud.gameObject.SetActive(false);
        hudSelect.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(ball == null){
            ball = GameObject.Find("Ball (clone)");
        }
        
        if(Input.GetKeyDown(KeyCode.Space)){
            isPaused = !isPaused;
        }

        if(isPaused){
            // BallController ballSpeed;
            // MoveBar playerCurrentType;
            // playerCurrentType = playerScript.currentInput;
            // ballSpeed = ballScript.speed;
            playerCurrentType = MoveBar.currentInput;
            ballSpeed = BallController.speed;

            MoveBar.currentInput = MoveBar.inputTypes.None;
            BallController.speed = 0f;

            hud.gameObject.SetActive(true);
            hudSelect.gameObject.SetActive(true);
        }else{
            if(MoveBar.currentInput == MoveBar.inputTypes.None){
                MoveBar.currentInput = playerCurrentType;
            }
            if(BallController.speed == 0f){
                BallController.speed = ballSpeed;
            }
            
            hud.gameObject.SetActive(false);
            hudSelect.gameObject.SetActive(false);
        }
    }
}
