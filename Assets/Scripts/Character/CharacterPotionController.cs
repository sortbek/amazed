using Assets.Scripts.Items.Potions;
using UnityEngine;
using Util;

namespace Assets.Scripts.Character {
    // Created by:
    // Hugo Kamps
    // S1084074
    public class CharacterPotionController : MonoBehaviour {
        private Character _player;

        public Potion Health, HealthRegeneration, Speed, Damage, Defense, Guidance;

        private void Awake() {
            _player = GetComponent<Character>();
            Health = new HealthPotion(_player);
            HealthRegeneration = new HealthRegenerationPotion(_player);
            Speed = new SpeedPotion(_player);
            Damage = new DamagePotion(_player);
            Defense = new DefensePotion(_player);
            Guidance = new GuidancePotion(_player);
        }

// Created By:
// Niek van den Brink
// S1078937
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