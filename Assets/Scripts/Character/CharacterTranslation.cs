using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Character {
    public class CharacterTranslation : ICharacterTransformation {

        private readonly Character _character;
        private Vector3 _momentum;
        public bool Airborne { get; set; }

        public CharacterTranslation(Character character) {
            _character = character;
            Airborne = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void Update() {
            var vertical = Input.GetAxis("Vertical") * _character.Speed * Time.deltaTime;
            var horizontal = Input.GetAxis("Horizontal") * _character.Speed * Time.deltaTime;
            var translation = Airborne ? _momentum : new Vector3(horizontal, 0, vertical);
            _momentum = translation;
            _character.transform.Translate(translation);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
                Cursor.lockState = CursorLockMode.None;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && !Airborne)
            {
                Airborne = true;
                _character.PlayAudio(_character.AudioJumping);
                _character.GetComponent<Rigidbody>().velocity += _character.JumpForce * Vector3.up;
            }
        }
    }
}
