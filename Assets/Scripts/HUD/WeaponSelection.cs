﻿using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Assets.Scripts.Character;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelection : MonoBehaviour {
    public RawImage[] WeaponImages;
    public Character Character;

    private CharacterWeaponController _controller;

    // Use this for initialization
    void Start() {
        _controller = FindObjectOfType<CharacterWeaponController>();
        WeaponImages = GetComponentsInChildren<RawImage>();
    }

    // Update is called once per frame
    void Update() {
        int currentWeaponID = _controller.CurrentWeapon.GetComponent<WeaponStat>().WeaponID;
        for (var index = 0; index < WeaponImages.Length; index++) {
            WeaponImages[index].color = Color.gray;
            if (index == currentWeaponID) WeaponImages[index].color = Color.white;
        }
    }
}