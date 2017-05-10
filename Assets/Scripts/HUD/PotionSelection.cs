using System.Diagnostics;
using Assets.Scripts.Items.Potions;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.HUD {
    public class PotionSelection : MonoBehaviour {
        private int _selectedPotionId;

        private RawImage _selectedPotionImage;
        private Text _amountLabel, _regenLeftLabel, _damageLeftLabel, _defenseLeftLabel, _speedLeftLabel;
        private RawImage _regenLeftImage, _damageLeftImage, _defenseLeftImage, _speedLeftImage;
        private Character.Character _player;

        public Potion SelectedPotion, Health, HealthRegeneration, Speed, Damage, Defense;

        void Awake()
        {
            _player = FindObjectOfType<Character.Character>();
            Health = new HealthPotion(_player);
            HealthRegeneration = new HealthRegenerationPotion(_player);
            Speed = new SpeedPotion(_player);
            Damage = new DamagePotion(_player);
            Defense = new DefensePotion(_player);
        }

        // Use this for initialization
        private void Start() {
            foreach (var text in GetComponentsInChildren<Text>()) {
                switch (text.name) {
                    case "Potion_Amount":
                        _amountLabel = text;
                        break;
                    case "DefenseLeft":
                        _defenseLeftLabel = text;
                        break;
                    case "DamageLeft":
                        _damageLeftLabel = text;
                        break;
                    case "SpeedLeft":
                        _speedLeftLabel = text;
                        break;
                    case "RegenLeft":
                        _regenLeftLabel = text;
                        break;
                }
            }

            foreach (var image in GetComponentsInChildren<RawImage>())
            {
                switch (image.name)
                {
                    case "Selected_Potion":
                        _selectedPotionImage = image;
                        break;
                    case "RegenLeftImage":
                        _regenLeftImage = image;
                        break;
                    case "DamageLeftImage":
                        _damageLeftImage = image;
                        break;
                    case "DefenseLeftImage":
                        _defenseLeftImage = image;
                        break;
                    case "SpeedLeftImage":
                        _speedLeftImage = image;
                        break;
                }
            }
            SelectedPotion = Health;
        }

        // Update is called once per frame
        private void Update() {
            // Input listeners
            if (Input.GetAxis("Mouse ScrollWheel") != 0f)
                _selectedPotionId += Input.GetAxis("Mouse ScrollWheel") > 0 ? -1 : 1;
            if (Input.GetKeyDown(KeyCode.Q)) _selectedPotionId -= 1;
            if (Input.GetKeyDown(KeyCode.E)) _selectedPotionId += 1;

            if (Input.GetKeyDown(KeyCode.X) && SelectedPotion.Amount > 0 && !SelectedPotion.Active) SelectedPotion.Use();

            // Check if another potions has been selected
            switch (_selectedPotionId) {
                case -1:
                    _selectedPotionId = 4;
                    break;
                case 0:
                    SelectedPotion = Health;
                    break;
                case 1:
                    SelectedPotion = HealthRegeneration;
                    break;
                case 2:
                    SelectedPotion = Damage;
                    break;
                case 3:
                    SelectedPotion = Defense;
                    break;
                case 4:
                    SelectedPotion = Speed;
                    break;
                case 5:
                    _selectedPotionId = 0;
                    break;
            }

            CheckActivePotions();
            UpdatePotionInformation();
        }

        private void UpdatePotionInformation() {
            _selectedPotionImage.texture = SelectedPotion.Texture;
            _amountLabel.text = SelectedPotion.Amount.ToString();

            _defenseLeftLabel.text = Defense.TimeLeft.ToString();
            _damageLeftLabel.text = Damage.TimeLeft.ToString();
            _speedLeftLabel.text = Speed.TimeLeft.ToString();
            _regenLeftLabel.text = HealthRegeneration.TimeLeft.ToString();
        }

        private void CheckActivePotions()
        {
            _regenLeftImage.enabled = HealthRegeneration.Active;
            _damageLeftImage.enabled = Damage.Active;
            _defenseLeftImage.enabled = Defense.Active;
            _speedLeftImage.enabled = Speed.Active;

            _regenLeftLabel.enabled = HealthRegeneration.Active;
            _damageLeftLabel.enabled = Damage.Active;
            _defenseLeftLabel.enabled = Defense.Active;
            _speedLeftLabel.enabled = Speed.Active;
        }

    }
}