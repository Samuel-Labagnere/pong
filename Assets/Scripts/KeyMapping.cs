using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyMapping : MonoBehaviour
{

    public InputField inputHaut;
    public InputField inputBas;
    public InputField inputGauche;
    public InputField inputDroite;

    // Start is called before the first frame update
    void Start()
    {
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
}
