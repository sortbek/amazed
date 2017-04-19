using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Character {

    public class Character : MonoBehaviour{

        public float DEF { get; set; }
        public float ATT { get; set; }
        public float Health { get; set; }
        public float Speed { get; set; }
        public float JumpForce { get; set; }

        public static readonly string ColliderTag = "Ground";

        private CharacterTranslation _translation;
        private CharacterRotation _rotation;


        void Awake() {
            _translation = new CharacterTranslation(this);
            _rotation = new CharacterRotation(this);
            Health = 100;
            Speed = 10f;
            JumpForce = 5f;
        }

        void Update() {
            _translation.Update();
            _rotation.Update();
        }

        void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.name.Equals(ColliderTag))
                _translation.Airborne = false;
        }
    }
    
}
