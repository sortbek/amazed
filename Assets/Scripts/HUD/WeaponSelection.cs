using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Character;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelection : MonoBehaviour
{
    public RawImage[] WeaponImages;

    public Character Character;    
	// Use this for initialization
	void Start ()
	{
	    WeaponImages = GetComponentsInChildren<RawImage>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    for (var index = 0; index < WeaponImages.Length; index++)
	    {
	        var image = WeaponImages[index];
	        image.color = Color.gray;
	        if(index == Character.DEF) image.color = Color.white;
	    }
	}
}
