using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class KeyMapping : MonoBehaviour
{
    
    private InputAction fireAction;
    private InputAction moveAction;
    private int up;

    public PlayerInput pInput; 

    public InputField inputHaut;
    public InputField inputBas;
    public InputField inputGauche;
    public InputField inputDroite;

    // Start is called before the first frame update
    void Start()
    {

        fireAction = pInput.actions.FindAction("Fire");
        moveAction = pInput.actions.FindAction("Move");
        up = moveAction.bindings.IndexOf(x => x.name == "up");

        InputField ih = inputHaut.GetComponent<InputField>();
        ih.onEndEdit.AddListener(delegate {Test(ih); });
    }

    // Update is called once per frame
    void Update()
    {
        
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
        moveAction.PerformInteractiveRebinding(up)
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
