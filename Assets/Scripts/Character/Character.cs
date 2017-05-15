using UnityEngine;
 using Assets.Scripts.World;

namespace Assets.Scripts.Character {

    public class Character : MonoBehaviour {

        [SerializeField]
        public AudioClip AudioJumping, AudioLanding;
        [SerializeField]
        public AudioClip[] AudioWalking;

        public float DEF { get; set; }
        public float ATT { get; set; }
        public float Health { get; set; }
        public float Speed { get; set; }
        public float JumpForce { get; set; }
        public int Points { get; set; }

        public static readonly string ColliderTag = "Ground";

        private CharacterTranslation _translation;
        private CharacterRotation _rotation;
        private CharacterInteraction _interaction;

        void Awake() {
            DontDestroyOnLoad(this);

            if (FindObjectsOfType(GetType()).Length > 1) {
                Destroy(gameObject);
            }

            _translation = new CharacterTranslation(this);
            _rotation = new CharacterRotation(this);
            _interaction = new CharacterInteraction(this);


            Health = 50f;
            Speed = 3f;
            JumpForce = 5f;
            Points = 0;
        }

        void Update() {
            _translation.Update();
            _rotation.Update();
            _interaction.Update();
        }

        void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.name.Equals(ColliderTag) && _translation.Airborne) {
                PlayAudio(AudioLanding);
                _translation.Airborne = false;
            }
        }

        void OnTriggerEnter(Collider collision) {
            if (collision.gameObject.tag == "startend") {
                GameManager.Instance.LoadNextLevel();
            }
        }

        public void PlayAudio(AudioClip clip) {
            if (clip == null) return;
            AudioSource src = GetComponent<AudioSource>();
            src.clip = clip;
            src.Play();
        }
    }

}
