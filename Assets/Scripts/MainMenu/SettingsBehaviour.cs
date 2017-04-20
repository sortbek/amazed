using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsBehaviour : MonoBehaviour
{
    private float brightness;
    public Slider volumeSlider;
    public AudioSource volumeAudio;

    // Use this for initialization
    void Start()
    {
        volumeSlider.value = volumeAudio.volume;
    }

    // Update is called once per frame
    void Update()
    {
        //RenderSettings.ambientLight = new Color(brightness, brightness, brightness, 1.0f);
    }


    public void AdjustBrightness(float brightness)
    {
        this.brightness = brightness;
    }

    public void AdjustVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }
}
