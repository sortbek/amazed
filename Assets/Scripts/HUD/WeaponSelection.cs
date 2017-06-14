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
        private RawImage[] _weaponImages;

        // Use this for initialization
        private void Start() {
            _controller = FindObjectOfType<CharacterWeaponController>();
            _weaponImages = GetComponentsInChildren<RawImage>();
            foreach (var image in _weaponImages) image.color = Color.black;
        }

        // Update is called once per frame
        private void Update() {
            CheckEquipment();
            if (_controller.CurrentWeapon != null) UpdateHud();
        }

        private void UpdateHud() {
            var currentWeaponId = _controller.CurrentWeapon.GetComponent<WeaponStat>().WeaponId;
            for (var index = 0; index < _weaponImages.Length; index++)
                _weaponImages[index].color = index == currentWeaponId ? Color.white : Color.black;
        }

        private void CheckEquipment() {
            for (var index = 0; index < _weaponImages.Length; index++)
                _weaponImages[index].enabled = _controller.GetEquipment()[index].Access;
        }
    }
}