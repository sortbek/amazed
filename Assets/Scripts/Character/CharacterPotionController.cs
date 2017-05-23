using Assets.Scripts.Items.Potions;
using UnityEngine;

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

        public void Add(string type) {
            switch (type) {
                case "Health Potion":
                    Health.Amount += 1;
                    break;
                case "Health Regeneration Potion":
                    HealthRegeneration.Amount += 1;
                    break;
                case "Damage Potion":
                    Damage.Amount += 1;
                    break;
                case "Defense Potion":
                    Defense.Amount += 1;
                    break;
                case "Speed Potion":
                    Speed.Amount += 1;
                    break;
                case "Guidance Potion":
                    Guidance.Amount += 1;
                    break;
            }
        }
    }
}
