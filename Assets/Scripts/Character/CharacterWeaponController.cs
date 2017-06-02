using System.Collections.Generic;
using Assets.Scripts.Util;
using UnityEngine;
using Util;

namespace Assets.Scripts.Character {
    // Created by:
    // Eelco Eikelboom
    // S1080542
    public class CharacterWeaponController : MonoBehaviour {
// Created By:
// Niek van den Brink
// S1078937       
        private readonly Dictionary<int, Item> _itemNumberToEnum = new Dictionary<int, Item> {
            {1, Item.Sword},
            {2, Item.BattleAxe},
            {3, Item.Maul},
            {4, Item.Dagger}
        };

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

// Created By:
// Niek van den Brink
// S1078937        
        private readonly Dictionary<Item, string> _weaponEnumToAnimation = new Dictionary<Item, string> {
            {Item.Dagger, "daggerAttack"},
            {Item.BattleAxe, "characterAttacking"},
            {Item.Sword, "characterAttacking"},
            {Item.Maul, "characterAttacking"},
            {Item.Null, "characterAttacking"}
        };

        private int _currentWeaponNumber;
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
                        if (weapon.Access) {
                            Equip(weapon.Object);
                            _currentWeaponNumber = i + 1;
                        }
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

// Created By:
// Niek van den Brink
// S1078937
        public void Add(Item item) {
            switch (item) {
                case Item.Sword:
                    Add(1);
                    break;
                case Item.BattleAxe:
                    Add(2);
                    break;
                case Item.Maul:
                    Add(3);
                    break;
                case Item.Dagger:
                    Add(4);
                    break;
                default:
                    break;
            }
        }

// Created By:
// Niek van den Brink
// S1078937
        public string GetWeaponAnimation() {
            if (_currentWeaponNumber == 0)
                return "characterAttacking";

            return _weaponEnumToAnimation[_itemNumberToEnum[_currentWeaponNumber]];
        }

        //Allows the character to use the weapon located at the given slot
        public void Add(int slot) {
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