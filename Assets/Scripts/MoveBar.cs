using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBar : MonoBehaviour
{

    public float speed = 10f;
    public Rigidbody2D rb2D;

    public enum inputTypes {Mouse, Keyboard, None};
    public static inputTypes currentInput = inputTypes.Keyboard;

    // Start is called before the first frame update
    void Start()
    {
        
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

            rb2D.MovePosition(newPos);
        }else if(currentInput == inputTypes.None){
            // Nothing happen here :D
        }
    }
}
