using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score;
    public static int lifes;
    public AudioSource music;
    public AudioSource bam;
    public AudioSource toc;
    public AudioSource loose;
    public AudioSource click;
    public AudioSource change;
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

        if(PlayerPrefs.HasKey("volume")){
            music.volume = PlayerPrefs.GetFloat("volume");
            bam.volume = PlayerPrefs.GetFloat("volume");
            toc.volume = PlayerPrefs.GetFloat("volume");
            loose.volume = PlayerPrefs.GetFloat("volume");
            click.volume = PlayerPrefs.GetFloat("volume");
            change.volume = PlayerPrefs.GetFloat("volume");
        }else{
            music.volume = 0.2f;
            bam.volume = 0.2f;
            toc.volume = 0.2f;
            loose.volume = 0.2f;
            click.volume = 0.2f;
            change.volume = 0.2f;
        }
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Ball"){
            toc.Play();
            score += 1;
            
        }
    }
}
