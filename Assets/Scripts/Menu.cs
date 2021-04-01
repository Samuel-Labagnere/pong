using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public Button playButton;
    public Button optionsButton;
    public Button quitButton;

    public Button returnButton;

    public Camera mainCamera;
    public Camera optionsCamera;

    private void Start() {
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
