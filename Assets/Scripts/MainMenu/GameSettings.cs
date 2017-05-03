using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings {

    private bool fullscreen;
    private int textureQuality;
    private int antialiasing;
    private int vSync;
    private int resolutionIndex;
    private float musicVolume;

    public bool Fullscreen
    {
        get
        {
            return fullscreen;
        }

        set
        {
            fullscreen = value;
        }
    }

    public int TextureQuality
    {
        get
        {
            return textureQuality;
        }

        set
        {
            textureQuality = value;
        }
    }

    public int Antialiasing
    {
        get
        {
            return antialiasing;
        }

        set
        {
            antialiasing = value;
        }
    }

    public int VSync
    {
        get
        {
            return vSync;
        }

        set
        {
            vSync = value;
        }
    }

    public int ResolutionIndex
    {
        get
        {
            return resolutionIndex;
        }

        set
        {
            resolutionIndex = value;
        }
    }

    public float MusicVolume
    {
        get
        {
            return musicVolume;
        }

        set
        {
            musicVolume = value;
        }
    }
}
