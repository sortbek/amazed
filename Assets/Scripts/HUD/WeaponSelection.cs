using Assets.Scripts.Character;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.HUD {
    // Created by:
    // Hugo Kamps
    // S1084074
    public class WeaponSelection : MonoBehaviour {
        private CharacterWeaponController _controller;
        public Character.Character Character;
        public RawImage[] WeaponImages;

        // Use this for initialization
        private void Start() {
            _controller = FindObjectOfType<CharacterWeaponController>();
            WeaponImages = GetComponentsInChildren<RawImage>();
            foreach (var image in WeaponImages) image.color = Color.black;
        }

        // Update is called once per frame
        private void Update() {
            CheckEquipment();
            if (_controller.CurrentWeapon != null) UpdateHUD();
        }

        private void UpdateHUD() {
            var currentWeaponID = _controller.CurrentWeapon.GetComponent<WeaponStat>().WeaponID;
            for (var index = 0; index < WeaponImages.Length; index++)
                WeaponImages[index].color = index == currentWeaponID ? Color.white : Color.black;
        }

        private void CheckEquipment() {
            for (var index = 0; index < WeaponImages.Length; index++)
                WeaponImages[index].enabled = _controller.GetEquipment()[index].Access;
        }
    }
}