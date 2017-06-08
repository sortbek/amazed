using System;
using System.Security.Cryptography;
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

       // [SerializeField]

        //private AudioSource walk;

        private AudioSource jumpland;
        private AudioSource jump;
        private AudioSource walk;
        public AudioSource[] asource;
        

        public float DEF { get; set; }
        public float ATT { get; set; }
        public float Health { get; set; }
        public float Speed { get; set; }
        public float JumpForce { get; set; }
        public int Points { get; set; }

        public static readonly string ColliderTag = "Ground";
        public const float MAX_HEALTH = 100;

        private CharacterTranslation _translation;
        private CharacterRotation _rotation;
        private CharacterInteraction _interaction;

        public GameObject Breadcrum;
        private GameObject Breadcrumgo;

        void Awake() {
            DontDestroyOnLoad(this);
            if (FindObjectsOfType(GetType()).Length > 1) {
                Destroy(gameObject);
            }
            _translation = new CharacterTranslation(this);
            _rotation = new CharacterRotation(this);
            _interaction = new CharacterInteraction(this);
            
            Health = 50f;
            Speed = 4f;
            JumpForce = 5f;
            Points = 0;

            asource = GetComponents<AudioSource>();
            jumpland = asource[0];
            jump = asource[1];
            walk = asource[2];


        }

        void FixedUpdate() {
            if(Input.GetKeyDown("p")) SceneManager.LoadScene(3);
            if(Input.GetKeyDown("o")) SceneManager.LoadScene("GameOver");

            if (Input.GetKeyDown("b")) {
                var loc = GameManager.Instance.Character.transform.position;
                Breadcrumgo = Instantiate(Breadcrum, loc, transform.rotation);
            }

            if (Input.GetKeyDown("w") || Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("d")) {
                walk.Play();
            }

           
            _translation.Update();
            _rotation.Update();
            _interaction.Update();
        }

        void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.name.Equals(ColliderTag) && _translation.Airborne) {
                jumpland.Play();
                _translation.Airborne = false;
            }
        }

        // Collider for the end point
        void OnTriggerEnter(Collider collision) {
            if (collision.gameObject.name == "End") {
                GameManager.Instance.LoadNextLevel();
            }
        }

        public void PlayJumpSound() {
            jump.Play();
        }

        public void PlayWalkingSound() {
            walk.Play();
        }

        public void PlayAudio(AudioClip clip) {
            if (clip == null) return;
              AudioSource src = GetComponent<AudioSource>();
              src.clip = clip;
              src.Play();
           
        }
    }
}
