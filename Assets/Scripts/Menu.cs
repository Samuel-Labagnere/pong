using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public float speed = 1f;

    public GameObject title;
    public Text subTitle;
    private RectTransform pos;

    public Button playButton;
    public Button optionsButton;
    public Button quitButton;
    public Text playObject;
    public Text optionsObject;
    public Text quitObject;
    public AudioSource clickSound;

    public Button returnButton;

    public Camera mainCamera;
    public Camera optionsCamera;

    private bool homeScreen;

    private void Start() {
        homeScreen = true;
        pos = title.GetComponent<RectTransform>();

        Button pb = playButton.GetComponent<Button>();
        pb.onClick.AddListener(Play);
        Button ob = optionsButton.GetComponent<Button>();
        ob.onClick.AddListener(Options);
        Button qb = quitButton.GetComponent<Button>();
        qb.onClick.AddListener(Quit);
        Button rb = returnButton.GetComponent<Button>();
        rb.onClick.AddListener(Return);

        mainCamera.gameObject.SetActive(true);
        optionsCamera.gameObject.SetActive(false);
    }

    void FixedUpdate(){
        if(homeScreen){
            subTitle.gameObject.SetActive(true);
            playButton.gameObject.SetActive(false);
            optionsButton.gameObject.SetActive(false);
            quitButton.gameObject.SetActive(false);
            if(Input.anyKey){
                homeScreen = false;
                StartCoroutine("LiftUp");
            }
        }else{
            subTitle.gameObject.SetActive(false);
            if(pos.anchoredPosition.y < 270f){
                pos.anchoredPosition += Vector2.up * 3f;
            }
        }
    }

    IEnumerator LiftUp(){
        yield return new WaitForSeconds(1);
        playButton.gameObject.SetActive(true);
        optionsButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        playObject.color = new Color(1, 1, 1, 0);
        optionsObject.color = new Color(1, 1, 1, 0);
        quitObject.color = new Color(1, 1, 1, 0);

        for(float i = 0; i < 1; i += 0.1f){
            yield return new WaitForSeconds(0.1f);
            playObject.color = new Color(1, 1, 1, i);
            optionsObject.color = new Color(1, 1, 1, i);
            quitObject.color = new Color(1, 1, 1, i);
        }
    }

   void Play(){
       clickSound.Play();
       SceneManager.LoadScene("Game");
   }

   void Options(){
       clickSound.Play();
       mainCamera.gameObject.SetActive(false);
       optionsCamera.gameObject.SetActive(true);
   }
   void Return(){
       clickSound.Play();
       mainCamera.gameObject.SetActive(true);
       optionsCamera.gameObject.SetActive(false);
   }

   void Quit(){
       clickSound.Play();
       Application.Quit();
   }

}
