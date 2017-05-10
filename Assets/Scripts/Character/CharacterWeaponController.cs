using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Character {
    public class CharacterWeaponController : MonoBehaviour {

        [SerializeField]
        public GameObject[] Weapons;
        public GameObject CurrentWeapon;

        private readonly KeyCode[] _numKeys = {
             KeyCode.Alpha1,
             KeyCode.Alpha2,
             KeyCode.Alpha3,
             KeyCode.Alpha4,
             KeyCode.Alpha5,
             KeyCode.Alpha6,
             KeyCode.Alpha7,
             KeyCode.Alpha8,
             KeyCode.Alpha9,
        };

        private Transform _weaponPosition;
        private Dictionary<int, WeaponObject> _equipment;

        void Start() {
            _equipment = new Dictionary<int, WeaponObject>();
            _weaponPosition = transform.FindChild("WeaponPosition");
            Load();
            Add(1);
            Add(2);
        }

        void Update() {
            for (int i = 0; i < Weapons.Length; i++) {
                if (Input.GetKeyDown(_numKeys[i])) {
                    if (_equipment.ContainsKey(i)) {
                        WeaponObject weapon = _equipment[i];
                        if (weapon.Access)
                            Equip(weapon.Object);
                    }
                }
            }
        }

        private void Load() {
            foreach (GameObject obj in Weapons) {
                var stat = obj.GetComponent<WeaponStat>();
                var position = new Vector3(_weaponPosition.position.x, _weaponPosition.position.y + obj.transform.position.y, _weaponPosition.position.z);
                var weaponObject = Instantiate(obj, position, _weaponPosition.localRotation, transform);
                var weapon = new WeaponObject() { Access = stat.Default, Object = weaponObject };
                weaponObject.transform.Rotate(stat.OffsetRotation);
                weaponObject.SetActive(false);
                _equipment[stat.WeaponID] = weapon;
            }
        }

        public void Add(int slot) {
            WeaponObject obj = _equipment[slot];
            if (!obj.Access)
                obj.Access = true;
            _equipment[slot] = obj;
        }

        public void Equip(GameObject obj) {
            if (CurrentWeapon != null)
                CurrentWeapon.SetActive(false);
            CurrentWeapon = obj;
            obj.SetActive(true);
        }

    }

    public struct WeaponObject {

        public bool Access { get; set; }
        public GameObject Object { get; set; }

    }
}
