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
            }
        }else{
            subTitle.gameObject.SetActive(false);
            // To animate
            /*while(pos.anchoredPosition.y < 270f){
                float ht = pos.anchoredPosition.y + 0.1f;
                pos.anchoredPosition = new Vector2(pos.anchoredPosition.x, ht);
            }*/
            while(pos.anchoredPosition.y < 270f){
                float ht = pos.anchoredPosition.y + 0.1f;
                pos.anchoredPosition += Vector2.up;
            }
            //
            playButton.gameObject.SetActive(true);
            optionsButton.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
        }
    }

   void Play(){
       SceneManager.LoadScene("Game");
   }

   void Options(){
       mainCamera.gameObject.SetActive(false);
       optionsCamera.gameObject.SetActive(true);
   }
   void Return(){
       mainCamera.gameObject.SetActive(true);
       optionsCamera.gameObject.SetActive(false);
   }

   void Quit(){
       Application.Quit();
   }

}
