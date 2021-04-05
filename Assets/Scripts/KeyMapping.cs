using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class KeyMapping : MonoBehaviour
{
    
    private InputAction fireAction;
    private InputAction moveAction;
    private InputActionRebindingExtensions.RebindingOperation rbOperation;
    private int up;
    private int left;
    private int down;
    private int right;

    public Text upText;
    public Text leftText;
    public Text downText;
    public Text rightText;
    public Text rotLeftText;
    public Text rotRightText;

    public Text button;
    public PlayerInput pInput; 

    private enum selectPos {Clavier, Souris, Up, Left, Down, Right, RotLeft, RotRight, Confirm};
    private selectPos currentPos = selectPos.Clavier;
    private int selectPosNumber = 0;

    public RawImage select;
    public RawImage coche;
    private RectTransform selectRect;
    public AudioSource changeSound;
    public AudioSource clickSound;
    public Camera mainCamera;
    public Camera optionsCamera;

    // Start is called before the first frame update
    void Start()
    {
        selectRect = select.GetComponent<RectTransform>();

        // LoadUserRebinds(pInput);

        if(PlayerPrefs.HasKey("inputTypes")){
            if(PlayerPrefs.GetInt("inputTypes") == 0){
                coche.GetComponent<RectTransform>().anchoredPosition = new Vector2(335, 0);
            }
            if(PlayerPrefs.GetInt("inputTypes") == 1){
                coche.GetComponent<RectTransform>().anchoredPosition = new Vector2(-105, 0);
            }
        }else{
            coche.GetComponent<RectTransform>().anchoredPosition = new Vector2(-105, 0);
        }

        fireAction = pInput.actions.FindAction("Fire");
        moveAction = pInput.actions.FindAction("Move");
        up = moveAction.bindings.IndexOf(x => x.name == "up");
        left = moveAction.bindings.IndexOf(x => x.name == "left");
        down = moveAction.bindings.IndexOf(x => x.name == "down");
        right = moveAction.bindings.IndexOf(x => x.name == "right");
    }

    // Update is called once per frame
    void Update()
    {
        upText.text = InputControlPath.ToHumanReadableString(moveAction.bindings[up].effectivePath);
        leftText.text = InputControlPath.ToHumanReadableString(moveAction.bindings[left].effectivePath);
        downText.text = InputControlPath.ToHumanReadableString(moveAction.bindings[down].effectivePath);
        rightText.text = InputControlPath.ToHumanReadableString(moveAction.bindings[right].effectivePath);
        rotLeftText.text = "?";
        rotRightText.text = "?";

        switch(selectPosNumber){
            case 0:
                currentPos = selectPos.Clavier;
            break;
            case 1:
                currentPos = selectPos.Souris;
            break;
            case 2:
                currentPos = selectPos.Up;
            break;
            case 3:
                currentPos = selectPos.Left;
            break;
            case 4:
                currentPos = selectPos.Down;
            break;
            case 5:
                currentPos = selectPos.Right;
            break;
            case 6:
                currentPos = selectPos.RotLeft;
            break;
            case 7:
                currentPos = selectPos.RotRight;
            break;
            case 8:
                currentPos = selectPos.Confirm;
            break;
        }
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            changeSound.Play();
            selectPosNumber -= 1;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            changeSound.Play();
            selectPosNumber += 1;
        }
        if(selectPosNumber == System.Enum.GetValues(typeof(selectPos)).Length){
            selectPosNumber = 0;
        }
        if(selectPosNumber < 0){
            selectPosNumber = System.Enum.GetValues(typeof(selectPos)).Length - 1;
        }
        
        switch(currentPos){
            case selectPos.Clavier:
                selectRect.anchoredPosition = new Vector2(-225, 0);
                selectRect.sizeDelta = new Vector2(350, 100);
            break;
            case selectPos.Souris:
                selectRect.anchoredPosition = new Vector2(225, 0);
                selectRect.sizeDelta = new Vector2(350, 100);
            break;
            case selectPos.Up:
                selectRect.anchoredPosition = new Vector2(-670, -180);
                selectRect.sizeDelta = new Vector2(200, 150);
            break;
            case selectPos.Left:
                selectRect.anchoredPosition = new Vector2(-420, -180);
                selectRect.sizeDelta = new Vector2(200, 150);
            break;
            case selectPos.Down:
                selectRect.anchoredPosition = new Vector2(-170, -180);
                selectRect.sizeDelta = new Vector2(200, 150);
            break;
            case selectPos.Right:
                selectRect.anchoredPosition = new Vector2(80, -180);
                selectRect.sizeDelta = new Vector2(200, 150);
            break;
            case selectPos.RotLeft:
                selectRect.anchoredPosition = new Vector2(330, -180);
                selectRect.sizeDelta = new Vector2(200, 150);
            break;
            case selectPos.RotRight:
                selectRect.anchoredPosition = new Vector2(580, -180);
                selectRect.sizeDelta = new Vector2(200, 150);
            break;
            case selectPos.Confirm:
                selectRect.anchoredPosition = new Vector2(700, 345);
                selectRect.sizeDelta = new Vector2(200, 150);
            break;
        }

        if(Input.GetKeyDown(KeyCode.Return)){
            switch(currentPos){
                case selectPos.Clavier:
                    clickSound.Play();
                    coche.GetComponent<RectTransform>().anchoredPosition = new Vector2(-105, 0);
                    PlayerPrefs.SetInt("inputTypes", 1);
                break;
                case selectPos.Souris:
                    clickSound.Play();
                    coche.GetComponent<RectTransform>().anchoredPosition = new Vector2(335, 0);
                    PlayerPrefs.SetInt("inputTypes", 0);
                break;
                case selectPos.Up:
                    clickSound.Play();
                    //
                break;
                case selectPos.Left:
                    clickSound.Play();
                    //
                break;
                case selectPos.Down:
                    clickSound.Play();
                    //
                break;
                case selectPos.Right:
                    clickSound.Play();
                    //
                break;
                case selectPos.RotLeft:
                    clickSound.Play();
                    //
                break;
                case selectPos.RotRight:
                    clickSound.Play();
                    //
                break;
                case selectPos.Confirm:
                    Confirm();
                break;
            }
        }
    }

    // void SaveUserRebinds(PlayerInput player){
    //     var json = JsonUtility.ToJson(player.actions);
    //     PlayerPrefs.SetString("rebinds", json);
    // }

    // void LoadUserRebinds(PlayerInput player){
    //     var json = JsonUtility.FromJson<InputActionAsset>(PlayerPrefs.GetString("rebinds"));
    //     player.actions = json;
    // }

    void Test(InputField input){
        if(input.text.Length > 0){
            Debug.Log("Test! Value = " + input.text);
        }
        if(input.text.Length == 0){
            Debug.Log("Test! Empty!");
        }
    }

    void Confirm(){
        clickSound.Play();
        mainCamera.gameObject.SetActive(true);
        optionsCamera.gameObject.SetActive(false);
    }

    void RemapUp(){
        Debug.Log("remap go");
        moveAction.Disable();
        rbOperation = moveAction.PerformInteractiveRebinding(up)
            .WithControlsExcluding("Mouse")
            .WithCancelingThrough("<Keyboard>/escape")
            .OnMatchWaitForAnother(0.2f)
            .Start().OnCancel(op =>
            {
                Debug.Log("cancel");
 
            }).OnComplete(op =>
            {
                Debug.Log("saved");
                moveAction.Enable();
                moveAction.Dispose();
                // SaveUserRebinds(pInput);
            });
    }

    void RemapLeft(){
        Debug.Log("remap go");
        moveAction.Disable();
        rbOperation = moveAction.PerformInteractiveRebinding(left)
            .WithControlsExcluding("Mouse")
            .WithCancelingThrough("<Keyboard>/escape")
            .OnMatchWaitForAnother(0.2f)
            .Start().OnCancel(op =>
            {
                Debug.Log("cancel");
 
            }).OnComplete(op =>
            {
                Debug.Log("saved");
                moveAction.Enable();
                moveAction.Dispose();
                // SaveUserRebinds(pInput);
            });
    }

    void RemapDown(){
        Debug.Log("remap go");
        moveAction.Disable();
        rbOperation = moveAction.PerformInteractiveRebinding(down)
            .WithControlsExcluding("Mouse")
            .WithCancelingThrough("<Keyboard>/escape")
            .OnMatchWaitForAnother(0.2f)
            .Start().OnCancel(op =>
            {
                Debug.Log("cancel");
 
            }).OnComplete(op =>
            {
                Debug.Log("saved");
                moveAction.Enable();
                moveAction.Dispose();
                // SaveUserRebinds(pInput);
            });
    }

    void RemapRight(){
        Debug.Log("remap go");
        moveAction.Disable();
        rbOperation = moveAction.PerformInteractiveRebinding(right)
            .WithControlsExcluding("Mouse")
            .WithCancelingThrough("<Keyboard>/escape")
            .OnMatchWaitForAnother(0.2f)
            .Start().OnCancel(op =>
            {
                Debug.Log("cancel");
 
            }).OnComplete(op =>
            {
                Debug.Log("saved");
                moveAction.Enable();
                moveAction.Dispose();
                // SaveUserRebinds(pInput);
            });
    }

    public void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire");
    }

    public void Move(InputAction.CallbackContext context)
    {
        Debug.Log("Move");
    }
}
