using System;
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
        public const float MAX_HEALTH = 100;

        public static readonly string ColliderTag = "Ground";

        private CharacterInteraction _interaction;
        private CharacterRotation _rotation;
        private CharacterTranslation _translation;
        private GridNode _current;

        private AudioSource jumpland;
        private AudioSource jump;
        private AudioSource walk;
        public AudioSource[] asource;
        

        public EventHandler NodeChanged;
        public GridNode Node {
            get { return _current; }
            set {
                _current = value;
                OnNodeChanged();
            }
        }

        public float DEF { get; set; }
        public float ATT { get; set; }
        public float Health { get; set; }
        public float Speed { get; set; }
        public float JumpForce { get; set; }
        public int Points { get; set; }

        public GameObject Breadcrumb;
        private GameObject Breadcrumbgo;

        private bool _damageable;

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

            _damageable = true;

            asource = GetComponents<AudioSource>();
            jumpland = asource[0];
            jump = asource[1];
            walk = asource[2];

            SetStats();

        }

        private void FixedUpdate() {
            if (_interaction == null)
                _interaction = new CharacterInteraction(this);

            if (Health <= 0) SceneManager.LoadScene(4);

            if (Input.GetKeyDown("b"))
            {
                var loc = GameManager.Instance.Character.transform.position;
                Breadcrumbgo = Instantiate(Breadcrumb, loc, transform.rotation);
            }

            if (Input.GetKeyDown("w") || Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("d"))
            {
                walk.Play();
            }

            _translation.Update();
            _rotation.Update();
            _interaction.Update();
        }

        private void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.name.Equals(ColliderTag) && _translation.Airborne) {
                jumpland.Play();
                _translation.Airborne = false;
            }
        }

        public void Damage(float damage) {
            damage = damage - DEF;
            if (damage > 0.0f && _damageable) {
                Health -= damage;
            }
        }

        public void ToggleDamagable() {
            _damageable = !_damageable;
        }

        public void PlayJumpSound() {
            jump.Play();
        }

        public void PlayWalkingSound() {
            walk.Play();
        }

        private void OnNodeChanged() {
            if(NodeChanged != null)
                NodeChanged(this, EventArgs.Empty);
        }

        private void OnTriggerEnter(Collider collision) {
            if (collision.gameObject.name == "End") GameManager.Instance.LoadNextLevel();
            if (collision.gameObject.tag == "EnemyWeapon") Damage(5.0f);
        }

        public void SetStats() {
            Health = 100f;
            Speed = 4f;
            JumpForce = 5f;
            Points = 0;
        }
    }
}