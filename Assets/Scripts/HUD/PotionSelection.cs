using Assets.Scripts.Items.Potions;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.HUD {
    public class PotionSelection : MonoBehaviour {
        private int _selectedPotionId;

        private RawImage _selectedPotionImage;
        private Text _amountLabel;
        private Character.Character _player;

        private Potion _selectedPotion, _health, _healthRegeneration, _speed, _damage, _defense;

        // Use this for initialization
        private void Start() {
            _selectedPotionImage = GetComponentInChildren<RawImage>();
            _amountLabel = GetComponentInChildren<Text>();
            _player = FindObjectOfType<Character.Character>();

            _health = new HealthPotion(_player);
            _healthRegeneration = new HealthRegenerationPotion(_player);
            _speed = new SpeedPotion(_player);
            _damage = new DamagePotion(_player);
            _defense = new DefensePotion(_player);

            _selectedPotion = _health;
        }

        // Update is called once per frame
        private void Update() {
            // Input listeners
            if (Input.GetAxis("Mouse ScrollWheel") != 0f)
                _selectedPotionId += Input.GetAxis("Mouse ScrollWheel") > 0 ? 1 : -1;
            if (Input.GetKeyDown(KeyCode.Q)) _selectedPotionId -= 1;
            if (Input.GetKeyDown(KeyCode.E)) _selectedPotionId += 1;

            if (Input.GetKeyDown(KeyCode.X) && _selectedPotion.Amount > 0) _selectedPotion.Use();

            // Check if another potions has been selected
            switch (_selectedPotionId) {
                case -1:
                    _selectedPotionId = 4;
                    break;
                case 0:
                    _selectedPotion = _health;
                    break;
                case 1:
                    _selectedPotion = _healthRegeneration;
                    break;
                case 2:
                    _selectedPotion = _damage;
                    break;
                case 3:
                    _selectedPotion = _defense;
                    break;
                case 4:
                    _selectedPotion = _speed;
                    break;
                case 5:
                    _selectedPotionId = 0;
                    break;
            }

            // Change the view components of the selected potion indicator
            _selectedPotionImage.texture = _selectedPotion.Texture;
            _amountLabel.text = _selectedPotion.Amount.ToString();
        }
    }
}