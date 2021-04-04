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
    public RawImage hudSelect;
    public AudioSource clickSound;
    public AudioSource selectChange;
    private bool anim;
    private RectTransform hudSelectPos;
    private float hudSelectY;

    private enum selectPos {Play, Options, Quit};
    private selectPos currentSelectPos = selectPos.Play;
    private int selectPosNumber = 0;

    public Button returnButton;

    public Camera mainCamera;
    public Camera optionsCamera;

    private bool homeScreen;

    private void Start() {
        homeScreen = true;
        anim = false;
        pos = title.GetComponent<RectTransform>();
        hudSelectPos = hudSelect.gameObject.GetComponent<RectTransform>();

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
        hudSelect.gameObject.SetActive(false);
    }

    void FixedUpdate(){
        if(homeScreen){
            subTitle.gameObject.SetActive(true);
            playButton.gameObject.SetActive(false);
            optionsButton.gameObject.SetActive(false);
            quitButton.gameObject.SetActive(false);
            hudSelect.gameObject.SetActive(false);
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

    void Update(){
        if(anim){
            switch(selectPosNumber){
                case 0:
                    currentSelectPos = selectPos.Play;
                break;
                case 1:
                    currentSelectPos = selectPos.Options;
                break;
                case 2:
                    currentSelectPos = selectPos.Quit;
                break;
                default:
                    currentSelectPos = selectPos.Play;
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
                case selectPos.Play:
                    hudSelectY = -52.07f;
                    hudSelectPos.anchoredPosition = new Vector2(hudSelectPos.anchoredPosition.x, hudSelectY);
                break;
                case selectPos.Options:
                    hudSelectY = -173.29f;
                    hudSelectPos.anchoredPosition = new Vector2(hudSelectPos.anchoredPosition.x, hudSelectY);
                break;
                case selectPos.Quit:
                    hudSelectY = -346.48f;
                    hudSelectPos.anchoredPosition = new Vector2(hudSelectPos.anchoredPosition.x, hudSelectY);
                break;
            }

            if(Input.GetKeyDown(KeyCode.Return)){
                switch(currentSelectPos){
                    case selectPos.Play:
                        Play();
                    break;
                    case selectPos.Options:
                        Options();
                    break;
                    case selectPos.Quit:
                        Quit();
                    break;
                }
            }
        }
    }

    IEnumerator LiftUp(){
        yield return new WaitForSeconds(1);
        playButton.gameObject.SetActive(true);
        optionsButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        hudSelect.gameObject.SetActive(true);
        playObject.color = new Color(1, 1, 1, 0);
        optionsObject.color = new Color(1, 1, 1, 0);
        quitObject.color = new Color(1, 1, 1, 0);
        hudSelect.color = new Color(1, 1, 1, 0);

        for(float i = 0; i < 1; i += 0.1f){
            yield return new WaitForSeconds(0.1f);
            playObject.color = new Color(1, 1, 1, i);
            optionsObject.color = new Color(1, 1, 1, i);
            quitObject.color = new Color(1, 1, 1, i);
            hudSelect.color = new Color(1, 1, 1, i);
        }

        anim = true;
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
