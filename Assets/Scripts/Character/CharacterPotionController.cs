using System;
using Assets.Scripts.Items.Potions;
using UnityEngine;
using Util;

namespace Assets.Scripts.Character {
    public class CharacterPotionController : MonoBehaviour {

        public Potion Health, HealthRegeneration, Speed, Damage, Defense, Guidance;
        private Character _player;

        void Awake() {
            _player = GetComponent<Character>();
            Health = new HealthPotion(_player);
            HealthRegeneration = new HealthRegenerationPotion(_player);
            Speed = new SpeedPotion(_player);
            Damage = new DamagePotion(_player);
            Defense = new DefensePotion(_player);
            Guidance = new GuidancePotion(_player);

            Speed.Amount += 9;
        }

        public void Add(Item type) {
            switch (type) {
                case Item.HealthPot:
                    Health.Amount += 1;
                    break;
                case Item.HealthRegenPot:
                    HealthRegeneration.Amount += 1;
                    break;
                case Item.DamagePot:
                    Damage.Amount += 1;
                    break;
                case Item.DefensePot:
                    Defense.Amount += 1;
                    break;
                case Item.SpeedPot:
                    Speed.Amount += 1;
                    break;
                case Item.GuidancePot:
                    Guidance.Amount += 1;
                    break;
                default:
                    break;
            }
        }
    }
}
