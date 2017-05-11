using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Character {
    public class CharacterRotation : ICharacterTransformation {

        private Vector2 _mouseLook, _smoothingVector;
        private GameObject _camera;
        private readonly Character _character;

        public float Sensivity { get; set; }
        public float Smoothing { get; set; }

        public CharacterRotation(Character character) {
            _character = character;
            Sensivity = 1f;
            Smoothing = 2f;
        }

        public void Update() {
            if(_camera == null)
                _camera = _character.GetComponentsInChildren<Camera>()[0].gameObject;
            var delta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxis("Mouse Y"));
            delta = Vector2.Scale(delta, new Vector2(Sensivity * Smoothing, Sensivity * Smoothing));
            _smoothingVector.x = Mathf.Lerp(_smoothingVector.x, delta.x, 1f / Smoothing);
            _smoothingVector.y = Mathf.Lerp(_smoothingVector.y, delta.y, 1f / Smoothing);
            _mouseLook += _smoothingVector;
            _mouseLook.y = Mathf.Clamp(_mouseLook.y, -90f, 90f);

            _camera.transform.localRotation = Quaternion.AngleAxis(-_mouseLook.y, Vector3.right);
            _character.transform.localRotation = Quaternion.AngleAxis(_mouseLook.x, _character.transform.up);

        }
    }
}
