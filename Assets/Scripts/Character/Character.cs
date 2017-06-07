using System;
using Assets.Scripts.World;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Character {
    // Created by:
    // Eelco Eikelboom
    // S1080542
    public class Character : MonoBehaviour {
        public const float MAX_HEALTH = 100;

        public static readonly string ColliderTag = "Ground";

        private CharacterInteraction _interaction;
        private CharacterRotation _rotation;
        private CharacterTranslation _translation;
        private GridNode _current;

        [SerializeField]
        public AudioClip AudioJumping, AudioLanding;
        [SerializeField]
        public AudioClip[] AudioWalking;

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
        }

        private void FixedUpdate() {
            if (_interaction == null)
                _interaction = new CharacterInteraction(this);

            if (Input.GetKeyDown("p")) SceneManager.LoadScene(3);
            if (Input.GetKeyDown("o")) SceneManager.LoadScene("GameOver");
            _translation.Update();
            _rotation.Update();
            _interaction.Update();
        }

        private void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.name.Equals(ColliderTag) && _translation.Airborne) {
                PlayAudio(AudioLanding);
                _translation.Airborne = false;
            }
        }

        private void OnNodeChanged() {
            if(NodeChanged != null)
                NodeChanged(this, EventArgs.Empty);
        }

        // Collider for the end point
        private void OnTriggerEnter(Collider collision) {
            if (collision.gameObject.name == "End") GameManager.Instance.LoadNextLevel();
        }

        public void PlayAudio(AudioClip clip) {
            if (clip == null) return;
            var src = GetComponent<AudioSource>();
            src.clip = clip;
            src.Play();
        }
    }
}