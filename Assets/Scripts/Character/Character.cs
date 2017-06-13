using System;
using System.Collections;
using Assets.Scripts.Util;
using UnityEngine;
using Assets.Scripts.World;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Character {
    // Created by:
    // Eelco Eikelboom
    // S1080542
    // Jordi Wolthuis
    // s10854303
    [RequireComponent(typeof(AudioSource))]
    public class Character : MonoBehaviour {
        private CharacterInteraction _interaction;
        private CharacterRotation _rotation;
        private CharacterTranslation _translation;

        private GridNode _current;

        private AudioSource _jumpland;
        private AudioSource _jump;
        private AudioSource _walk;
        private AudioSource[] _asource;

        private GameObject _breadcrumb;
        private bool _damageable;

        public EventHandler NodeChanged;

        public GridNode Node {
            get { return _current; }
            set {
                _current = value;
                OnNodeChanged();
            }
        }

        public const float MAX_HEALTH = 100;

        public static readonly string ColliderTag = "Ground";

        public float DEF { get; set; }
        public float ATT { get; set; }
        public float Health { get; set; }
        public float Speed { get; set; }
        public float JumpForce { get; set; }
        public int Points { get; set; }

        public GameObject BreadcrumbPrefab;

        public Transform Camera {
            get { return transform.FindDeepChild("Camera"); }
        }

        void Awake() {
            DontDestroyOnLoad(this);
            if (FindObjectsOfType(GetType()).Length > 1) Destroy(gameObject);

            _translation = new CharacterTranslation(this);
            _rotation = new CharacterRotation(this);
            _interaction = new CharacterInteraction(this);

            Health = 50f;
            Speed = 4f;
            JumpForce = 5f;
            Points = 0;

            _asource = GetComponents<AudioSource>();
            _jumpland = _asource[0];
            _jump = _asource[1];
            _walk = _asource[2];

            SetStats();
        }

        private void FixedUpdate() {
            if (_interaction == null)
                _interaction = new CharacterInteraction(this);

            if (Health <= 0) SceneManager.LoadScene(4);

            if (Input.GetKeyDown("b")) {
                var loc = GameManager.Instance.Character.transform.position;
                _breadcrumb = Instantiate(BreadcrumbPrefab, loc, transform.rotation);
            }

            if (Input.GetKeyDown("w") || Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("d")) {
                _walk.Play();
            }

            _translation.Update();
            _rotation.Update();
            _interaction.Update();
        }

        private void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.name.Equals(ColliderTag) && _translation.Airborne) {
                _jumpland.Play();
                _translation.Airborne = false;
            }
        }

        private void OnTriggerEnter(Collider collision) {
            if (collision.gameObject.name == "End") GameManager.Instance.LoadNextLevel();
            if (collision.gameObject.tag == "EnemyWeapon") TakeDamage(5.0f);
        }

        public void TakeDamage(float damage) {
            damage = damage - DEF;
            if (damage > 0.0f && _damageable) {
                Health -= damage;
            }
        }

        public void ToggleDamagable() {
            _damageable = !_damageable;
        }

        public void PlayJumpSound() {
            _jump.Play();
        }

        public void PlayWalkingSound() {
            _walk.Play();
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