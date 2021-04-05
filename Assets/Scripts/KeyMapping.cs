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

    public Text button;
    public PlayerInput pInput; 

    private enum selectPos {Clavier, Souris, Up, Left, Down, Right, RotLeft, RotRight, Confirm};
    private selectPos currentPos = selectPos.Clavier;
    private int selectPosNumber = 0;

    public RawImage select;
    private RectTransform selectRect;
    public AudioSource changeSound;
    public AudioSource clickSound;
    public Camera mainCamera;
    public Camera optionsCamera;

    // Start is called before the first frame update
    void Start()
    {
        selectRect = select.GetComponent<RectTransform>();

        fireAction = pInput.actions.FindAction("Fire");
        moveAction = pInput.actions.FindAction("Move");
        up = moveAction.bindings.IndexOf(x => x.name == "up");
    }

    // Update is called once per frame
    void Update()
    {
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
                selectRect.anchoredPosition = new Vector2(-700, -180);
                selectRect.sizeDelta = new Vector2(150, 150);
            break;
            case selectPos.Left:
                selectRect.anchoredPosition = new Vector2(-450, -180);
                selectRect.sizeDelta = new Vector2(150, 150);
            break;
            case selectPos.Down:
                selectRect.anchoredPosition = new Vector2(-200, -180);
                selectRect.sizeDelta = new Vector2(150, 150);
            break;
            case selectPos.Right:
                selectRect.anchoredPosition = new Vector2(50, -180);
                selectRect.sizeDelta = new Vector2(150, 150);
            break;
            case selectPos.RotLeft:
                selectRect.anchoredPosition = new Vector2(300, -180);
                selectRect.sizeDelta = new Vector2(150, 150);
            break;
            case selectPos.RotRight:
                selectRect.anchoredPosition = new Vector2(550, -180);
                selectRect.sizeDelta = new Vector2(150, 150);
            break;
            case selectPos.Confirm:
                selectRect.anchoredPosition = new Vector2(700, 345);
                selectRect.sizeDelta = new Vector2(150, 150);
            break;
        }

        if(Input.GetKeyDown(KeyCode.Return)){
            switch(currentPos){
                case selectPos.Clavier:
                    clickSound.Play();
                    //
                break;
                case selectPos.Souris:
                    clickSound.Play();
                    //
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
                    clickSound.Play();
                    mainCamera.gameObject.SetActive(true);
                    optionsCamera.gameObject.SetActive(false);
                break;
            }
        }
    }

    void Test(InputField input){
        if(input.text.Length > 0){
            Debug.Log("Test! Value = " + input.text);
        }
        if(input.text.Length == 0){
            Debug.Log("Test! Empty!");
        }
    }

    void Remap(){
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
                Debug.Log(rbOperation.selectedControl.name);
                button.text = rbOperation.selectedControl.name;
                Debug.Log("saved");
                moveAction.Enable();
                moveAction.Dispose();
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
