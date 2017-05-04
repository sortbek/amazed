using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Character {
    public class CharacterWeaponController : MonoBehaviour {

        [SerializeField]
        public GameObject[] Weapons;

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

        public GameObject CurrentWeapon;
        private Dictionary<int, GameObject> _equipment;

        public CharacterWeaponController() {
            _equipment = new Dictionary<int, GameObject>();
        }

        void Start() {
            Add(Weapons[0]);
            Add(Weapons[1]);
            Add(Weapons[2]);
        }

        void Update() {
            for (int i = 0; i < Weapons.Length; i++) {
                if (Input.GetKeyDown(_numKeys[i])) {
                    if (_equipment.ContainsKey(i)) {
                        Equip(_equipment[i]);
                    }
                }
            }
        }

        public void Add(GameObject obj) {
            if (!_equipment.ContainsValue(obj)) {
                _equipment[_equipment.Count] = obj;
                //show in hotbar or something
            }
        }

        public void Equip(GameObject obj) {
            if (CurrentWeapon != null) {
                
            }
            GameObject weapon = Instantiate(obj, transform.position + transform.forward + new Vector3(0.5f, 0, 0), obj.transform.rotation);
            weapon.transform.parent = transform;
            CurrentWeapon = obj;
        }


    }
}
