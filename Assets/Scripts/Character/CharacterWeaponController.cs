using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Util;
using UnityEngine;
using Util;

namespace Assets.Scripts.Character {
    // Created by:          
    // Eelco Eikelboom      Niek van den Brink
    // S1080542             S1078937
    public class CharacterWeaponController : MonoBehaviour {
        private readonly KeyCode[] _numKeys = {
            KeyCode.Alpha1,
            KeyCode.Alpha2,
            KeyCode.Alpha3,
            KeyCode.Alpha4,
            KeyCode.Alpha5,
            KeyCode.Alpha6,
            KeyCode.Alpha7,
            KeyCode.Alpha8,
            KeyCode.Alpha9
        };

        private Dictionary<int, WeaponObject> _equipment;
        private Transform _weaponPosition;
        public GameObject CurrentWeapon;

        [SerializeField] public GameObject[] Weapons;


        private void Start() {
            _equipment = new Dictionary<int, WeaponObject>();
            _weaponPosition = transform.FindDeepChild("WeaponPosition");
            Load();
            Add(1);
            Add(2);
            Add(3);
            Add(4);
        }

        private void Update() {
            for (var i = 0; i < Weapons.Length; i++)
                if (Input.GetKeyDown(_numKeys[i]))
                    if (_equipment.ContainsKey(i)) {
                        var weapon = _equipment[i];
                        if (!weapon.Access) continue;
                        Equip(weapon.Object);
                    }
        }

        private void Load() {
            foreach (var obj in Weapons) {
                var stat = obj.GetComponent<WeaponStat>();
                var position = new Vector3(_weaponPosition.position.x,
                    _weaponPosition.position.y + obj.transform.position.y, _weaponPosition.position.z);
                var weaponObject = Instantiate(obj, position, _weaponPosition.rotation, _weaponPosition);
                var weapon = new WeaponObject {Access = stat.Default, Object = weaponObject};
                weaponObject.SetActive(false);
                _equipment[stat.WeaponID] = weapon;
            }
        }

        public void Attack() {
            CurrentWeapon.gameObject.GetComponent<BoxCollider>().enabled = true;
            StartCoroutine(ResetColliders());
        }

        private IEnumerator ResetColliders() {
            yield return new WaitForSeconds(0.5f);
            CurrentWeapon.gameObject.GetComponent<BoxCollider>().enabled = false;
        }

        public void Add(Item item) {
            Add((int) item);
        }

        public string GetWeaponAnimation() {
            return CurrentWeapon == null
                ? "characterAttacking"
                : CurrentWeapon.GetComponent<WeaponStat>().AnimationTag;
        }

        //Allows the character to use the weapon located at the given slot
        public void Add(int slot) {
            if (slot < 0 || slot > Weapons.Length) slot = 1;
            var obj = _equipment[slot - 1];
            if (!obj.Access)
                obj.Access = true;
            _equipment[slot - 1] = obj;
        }

        //Equips the given gameobject as the weapon
        public void Equip(GameObject obj) {
            if (CurrentWeapon != null)
                CurrentWeapon.SetActive(false);
            CurrentWeapon = obj;
            obj.SetActive(true);
        }

        public Dictionary<int, WeaponObject> GetEquipment() {
            return _equipment;
        }
    }

    public struct WeaponObject {
        public bool Access { get; set; }
        public GameObject Object { get; set; }
    }
}