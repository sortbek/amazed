using System.Diagnostics;
using Assets.Scripts.Character;
using Assets.Scripts.Items.Potions;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.HUD {

    // Created by:
    // Hugo Kamps
    // S1084074
    public class PotionSelection : MonoBehaviour {
        private int _selectedPotionId;

        private RawImage _selectedPotionImage;
        private Text _amountLabel, _regenLeftLabel, _damageLeftLabel, _defenseLeftLabel, _speedLeftLabel;
        private RawImage _regenLeftImage, _damageLeftImage, _defenseLeftImage, _speedLeftImage;
        private Character.Character _player;
        private CharacterPotionController _controller;
        public Potion SelectedPotion;
        void Awake() {
            _player = FindObjectOfType<Character.Character>();
            _controller = FindObjectOfType<CharacterPotionController>();
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

            foreach (var image in GetComponentsInChildren<RawImage>()) {
                switch (image.name) {
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

            SelectedPotion = _controller.Health;
            _player = FindObjectOfType<Character.Character>();
        }

        // Update is called once per frame
        private void Update() {
            // Input listeners
            if (Input.GetAxis("Mouse ScrollWheel") != 0f)
                _selectedPotionId += Input.GetAxis("Mouse ScrollWheel") > 0 ? -1 : 1;
            if (Input.GetKeyDown(KeyCode.Q)) _selectedPotionId -= 1;
            if (Input.GetKeyDown(KeyCode.E)) _selectedPotionId += 1;

            if (Input.GetKeyDown(KeyCode.X) && SelectedPotion.Amount > 0 && !SelectedPotion.Active) SelectedPotion.Use();

            // Check if another potion has been selected
            switch (_selectedPotionId) {
                case -1:
                    _selectedPotionId = 5;
                    break;
                case 0:
                    SelectedPotion = _controller.Health;
                    break;
                case 1:
                    SelectedPotion = _controller.HealthRegeneration;
                    break;
                case 2:
                    SelectedPotion = _controller.Damage;
                    break;
                case 3:
                    SelectedPotion = _controller.Defense;
                    break;
                case 4:
                    SelectedPotion = _controller.Speed;
                    break;
                case 5:
                    SelectedPotion = _controller.Guidance;
                    break;
                case 6:
                    _selectedPotionId = 0;
                    break;
            }

            CheckActivePotions();
            UpdatePotionInformation();
        }

        // Updates the potion information on the HUD
        private void UpdatePotionInformation() {
            _selectedPotionImage.texture = SelectedPotion.Texture;
            _amountLabel.text = SelectedPotion.Amount.ToString();

            _defenseLeftLabel.text = _controller.Defense.TimeLeft.ToString();
            _damageLeftLabel.text = _controller.Damage.TimeLeft.ToString();
            _speedLeftLabel.text = _controller.Speed.TimeLeft.ToString();
            _regenLeftLabel.text = _controller.HealthRegeneration.TimeLeft.ToString();
        }

        // Updates the HUD items according to which potions (with a duration) are active
        private void CheckActivePotions() {
            _regenLeftImage.enabled = _controller.HealthRegeneration.Active;
            _damageLeftImage.enabled = _controller.Damage.Active;
            _defenseLeftImage.enabled = _controller.Defense.Active;
            _speedLeftImage.enabled = _controller.Speed.Active;

            _regenLeftLabel.enabled = _controller.HealthRegeneration.Active;
            _damageLeftLabel.enabled = _controller.Damage.Active;
            _defenseLeftLabel.enabled = _controller.Defense.Active;
            _speedLeftLabel.enabled = _controller.Speed.Active;
        }

    }
}