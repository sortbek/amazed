using Assets.Scripts.Character;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.HUD {
    public class WeaponSelection : MonoBehaviour {
        public RawImage[] WeaponImages;
        public Character.Character Character;

        private CharacterWeaponController _controller;

        // Use this for initialization
        void Start() {
            _controller = FindObjectOfType<CharacterWeaponController>();
            WeaponImages = GetComponentsInChildren<RawImage>();
            foreach (var image in WeaponImages) image.color = Color.black;
        }

        // Update is called once per frame
        void Update() {
            CheckEquipment();
            if (_controller.CurrentWeapon != null) UpdateHUD();
        }

        void UpdateHUD() {
            int currentWeaponID = _controller.CurrentWeapon.GetComponent<WeaponStat>().WeaponID;
            for (var index = 0; index < WeaponImages.Length; index++) {
                WeaponImages[index].color = index == currentWeaponID ? Color.white : Color.black;
            }
        }

        void CheckEquipment() {
            for (var index = 0; index < WeaponImages.Length; index++) {
                WeaponImages[index].enabled = _controller.GetEquipment()[index].Access;
            }
        }

    }
}
