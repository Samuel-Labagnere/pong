using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

    public RawImage hud;
    public RawImage hudSelect;
    // public GameObject player;

    private enum selectPos {Resume, Restart, Quit};
    private selectPos currentSelectPos = selectPos.Resume;
    private int selectPosNumber = 0;
    public AudioSource clickSound;
    public AudioSource selectChange;
    private RectTransform hudSelectPos;
    private float hudSelectY;

    private Rigidbody2D ballRb2D;
    private Vector2 ballRb2DVelocity;
    private float ballRb2DAngularVelocity;
    private GameObject ball;
    private bool isPaused;
    private float ballSpeed;
    private int playerCurrentType;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        ball = GameObject.Find("Ball");
        ballRb2D = ball.GetComponent<Rigidbody2D>();
        // MoveBar playerScript = player.GetComponent<MoveBar>();

        hud.gameObject.SetActive(false);
        hudSelect.gameObject.SetActive(false);

        hudSelectPos = hudSelect.gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.Space)){
            isPaused = !isPaused;
            if(isPaused){
                ballSpeed = BallController.speed;
                playerCurrentType = MoveBar.inputNumber;
                ballRb2DVelocity = ballRb2D.velocity;
                ballRb2DAngularVelocity = ballRb2D.angularVelocity;

                MoveBar.inputNumber = 2;
                ballRb2D.velocity = Vector2.zero;
                ballRb2D.angularVelocity = 0f;

                hud.gameObject.SetActive(true);
                hudSelect.gameObject.SetActive(true);

            }else{
                if(MoveBar.inputNumber == 2){
                    MoveBar.inputNumber = playerCurrentType;
                }
                if(ballRb2D.angularVelocity == 0f){
                    
                    ballRb2D.angularVelocity = ballRb2DAngularVelocity;
                    ballRb2D.AddForce(new Vector2(UnityEngine.Random.Range(0, 5), UnityEngine.Random.Range(0, 5)) * ballSpeed);
                }
                
                hud.gameObject.SetActive(false);
                hudSelect.gameObject.SetActive(false);
            }
        }

        if(isPaused){
            switch(selectPosNumber){
                case 0:
                    currentSelectPos = selectPos.Resume;
                break;
                case 1:
                    currentSelectPos = selectPos.Restart;
                break;
                case 2:
                    currentSelectPos = selectPos.Quit;
                break;
                default:
                    currentSelectPos = selectPos.Resume;
                break;
            }
            if(Input.GetKeyDown(KeyCode.UpArrow)){
                selectPosNumber -= 1;
                selectChange.Play();
            }
            if(Input.GetKeyDown(KeyCode.DownArrow)){
                selectPosNumber += 1;
                selectChange.Play();
            }
            if(selectPosNumber == System.Enum.GetValues(typeof(selectPos)).Length){
                selectPosNumber = 0;
            }
            if(selectPosNumber < 0){
                selectPosNumber = System.Enum.GetValues(typeof(selectPos)).Length - 1;
            }

            switch(currentSelectPos){
                case selectPos.Resume:
                    hudSelectY = 115f;
                    hudSelectPos.anchoredPosition = new Vector2(hudSelectPos.anchoredPosition.x, hudSelectY);
                break;
                case selectPos.Restart:
                    hudSelectY = -53f;
                    hudSelectPos.anchoredPosition = new Vector2(hudSelectPos.anchoredPosition.x, hudSelectY);
                break;
                case selectPos.Quit:
                    hudSelectY = -219;
                    hudSelectPos.anchoredPosition = new Vector2(hudSelectPos.anchoredPosition.x, hudSelectY);
                break;
            }

            if(Input.GetKeyDown(KeyCode.Return)){
                switch(currentSelectPos){
                    case selectPos.Resume:
                        Resume();
                    break;
                    case selectPos.Restart:
                        Restart();
                    break;
                    case selectPos.Quit:
                        Quit();
                    break;
                }
            }
        }
    }

    void Resume(){
       clickSound.Play();
       isPaused = false;
       if(MoveBar.inputNumber == 2){
            MoveBar.inputNumber = playerCurrentType;
        }
        if(ballRb2D.angularVelocity == 0f){
            
            ballRb2D.angularVelocity = ballRb2DAngularVelocity;
            ballRb2D.AddForce(new Vector2(UnityEngine.Random.Range(0, 5), UnityEngine.Random.Range(0, 5)) * ballSpeed);
        }
        
        hud.gameObject.SetActive(false);
        hudSelect.gameObject.SetActive(false);
    }

    void Restart(){
       clickSound.Play();
       if(MoveBar.inputNumber == 2){
            MoveBar.inputNumber = playerCurrentType;
        }
       SceneManager.LoadScene("Game");
    }

    void Quit(){
        clickSound.Play();
        if(MoveBar.inputNumber == 2){
            MoveBar.inputNumber = playerCurrentType;
        }
        SceneManager.LoadScene("Menu");
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