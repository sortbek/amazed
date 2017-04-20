using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsBehaviour : MonoBehaviour
{
    private float Brightness;
    public Slider VolumeSlider;
    public AudioSource VolumeAudio;

    // Use this for initialization
    void Start() {
        //VolumeSlider.value = VolumeAudio.volume;
    }

    // Update is called once per frame
    void Update() {
        //RenderSettings.ambientLight = new Color(brightness, brightness, brightness, 1.0f);
    }


    public void AdjustBrightness(float brightness) {
      //  this.Brightness = brightness;
    }

    public void AdjustVolume() {
       // AudioListener.volume = VolumeSlider.value;
    }
}
