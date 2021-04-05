using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{

    public GameObject slider;
    private float sliderVal;

    public AudioSource music;
    public AudioSource click;
    public AudioSource change;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("volume")){
            slider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("volume");
        }else{
            slider.GetComponent<Slider>().value = 0.2f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        sliderVal = slider.GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("volume", sliderVal);

        music.volume = sliderVal;
        click.volume = sliderVal;
        change.volume = sliderVal;
    }
}
