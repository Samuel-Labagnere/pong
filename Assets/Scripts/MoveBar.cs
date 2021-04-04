using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveBar : MonoBehaviour
{

    public float speed = 10f;
    public Rigidbody2D rb2D;

    public enum inputTypes {Mouse, Keyboard, None};
    public inputTypes currentInput = inputTypes.Keyboard;
    public static int inputNumber = 1;

    private float newRot = 0f;

    private Vector2 move;
    private InputAction moveAction;

    public PlayerInput test; 

    // Start is called before the first frame update
    void Start()
    {
        moveAction = test.actions.FindAction("Move");
        moveAction.PerformInteractiveRebinding()
                    // To avoid accidental input from mouse motion
                    .WithControlsExcluding("Mouse")
                    .OnMatchWaitForAnother(0.1f)
                    .Start();
    }

    public void Move(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();

        Debug.Log(move.x);
        Debug.Log(move.y);
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
            inputNumber += 1;
            if(inputNumber == System.Enum.GetValues(typeof(inputTypes)).Length){
                inputNumber = 0;
            }
        }
        switch(inputNumber){
            case 0:
                currentInput = inputTypes.Mouse;
            break;
            case 1:
                currentInput = inputTypes.Keyboard;
            break;
            case 2:
                currentInput = inputTypes.None;
            break;
        }

        if(currentInput == inputTypes.Mouse){
            if (Input.GetKey(KeyCode.Mouse0)){
                newRot += 1.02f;
                transform.rotation = Quaternion.Euler(Vector3.forward * newRot);
            }
            if (Input.GetKey(KeyCode.Mouse1)){
                newRot -= 1.02f;
                transform.rotation = Quaternion.Euler(Vector3.forward * newRot);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(currentInput == inputTypes.Mouse){
            Vector3 newPos = Input.mousePosition;
            newPos.z = 10f; // Set this to be the distance you want the object to be placed in front of the camera.
            rb2D.MovePosition(Camera.main.ScreenToWorldPoint(newPos));
        }else if(currentInput == inputTypes.Keyboard){
            Vector2 newPos = rb2D.position;

            if (Input.GetAxis("Horizontal") < 0f)
            {
                newPos += Time.fixedDeltaTime * speed * Vector2.left;
            }
            if (Input.GetAxis("Horizontal") > 0f)
            {
                newPos += Time.fixedDeltaTime * speed * Vector2.right;
            }
            if (Input.GetAxis("Vertical") > 0f)
            {
                newPos += Time.fixedDeltaTime * speed * Vector2.up;
            }
            if (Input.GetAxis("Vertical") < 0f)
            {
                newPos += Time.fixedDeltaTime * speed * Vector2.down;
            }
            if (Input.GetAxis("Fire1") > 0f){
                newRot += 2f;
                transform.rotation = Quaternion.Euler(Vector3.forward * newRot);
            }
            if (Input.GetAxis("Fire2") > 0f){
                newRot -= 2f;
                transform.rotation = Quaternion.Euler(Vector3.forward * newRot);
            }

            rb2D.MovePosition(newPos);
        }else if(currentInput == inputTypes.None){
            // Nothing happen here :D
        }
    }
}
