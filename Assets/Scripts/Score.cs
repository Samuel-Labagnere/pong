using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score;
    public static int lifes;
    public AudioSource toc;
    public Text scoreTxt;
    public Text lifesTxt;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreTxt.text = "Score : " + score;
        lifesTxt.text = "Vies : " + lifes;
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Ball"){
            toc.Play();
            score += 1;
            
        }
    }
}
