using System;
using Assets.Scripts.Util;
using Assets.Scripts.World;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Character {
    // Created by:
    // Eelco Eikelboom
    // S1080542
    // Jordi Wolthuis
    // s10854303
    [RequireComponent(typeof(AudioSource))]
    public class Character : MonoBehaviour {
        public const float MaxHealth = 100;

        public static readonly string ColliderTag = "Ground";

        private GridNode _current;

        private bool _damageable;
        private CharacterInteraction _interaction;
        private CharacterRotation _rotation;
        private CharacterTranslation _translation;
        public AudioSource Attack;

        public GameObject BreadcrumbPrefab;
        public AudioSource Jump;

        public AudioSource Jumpland;

        public EventHandler NodeChanged;
        public AudioSource Walk;

        public GridNode Node {
            get { return _current; }
            set {
                _current = value;
                OnNodeChanged();
            }
        }

        public float Def { get; set; }
        public float Att { get; set; }
        public float Health { get; set; }
        public float Speed { get; set; }
        public float JumpForce { get; set; }
        public int Points { get; set; }

        public Transform Camera {
            get { return transform.FindDeepChild("Camera"); }
        }

        private void Awake() {
            DontDestroyOnLoad(this);
            if (FindObjectsOfType(GetType()).Length > 1) Destroy(gameObject);

            _translation = new CharacterTranslation(this);
            _rotation = new CharacterRotation(this);
            _interaction = new CharacterInteraction(this);

            Health = 50f;
            Speed = 4f;
            JumpForce = 5f;
            Points = 0;

            _damageable = true;

            SetStats();
        }

        private void FixedUpdate() {
            if (_interaction == null)
                _interaction = new CharacterInteraction(this);

            if (Health <= 0) SceneManager.LoadScene(4);

            if (Input.GetKeyDown("b")) {
                var loc = GameManager.Instance.Character.transform.position;
                Instantiate(BreadcrumbPrefab, loc, transform.rotation);
            }

            _translation.Update();
            _rotation.Update();
            _interaction.Update();
        }

        private void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.name.Equals(ColliderTag) && _translation.Airborne) {
                Jumpland.Play();
                _translation.Airborne = false;
            }
        }

        private void OnTriggerEnter(Collider collision) {
            if (collision.gameObject.name == "End") GameManager.Instance.LoadNextLevel();
            if (collision.gameObject.tag == "EnemyWeapon") TakeDamage(5.0f);
        }

        public void TakeDamage(float damage) {
            damage = damage - Def;
            if (damage > 0.0f && _damageable) Health -= damage;
        }

        public void ToggleDamagable() {
            _damageable = !_damageable;
        }

        public void PlayJumpSound() {
            Jump.Play();
        }

        public void PlayWalkingSound() {
            if (!Walk.isPlaying) Walk.Play();
        }

        public void PlayAttackSound() {
            if (!Attack.isPlaying) Attack.Play();
        }

        private void OnNodeChanged() {
            if (NodeChanged != null)
                NodeChanged(this, EventArgs.Empty);
        }

        public void SetStats() {
            Health = 100f;
            Speed = 4f;
            JumpForce = 5f;
            Points = 0;
        }
    }
}