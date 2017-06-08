using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.World;
using UnityEngine;
using UnityEngine.PostProcessing;

namespace Assets.Scripts.Util {
    class PostProcessingUtil : MonoBehaviour {
        private Character.Character _character;
        private PostProcessingBehaviour _behaviour;

        private const int VignetteDiv = 140, GrainDiv = 300;

        void Start() {
            _behaviour = GetComponent<PostProcessingBehaviour>();
        }

        void Update() {
            if (_character == null) _character = GameManager.Instance.Character;
            else {
                var intensity = Character.Character.MAX_HEALTH - _character.Health;

                var vignette = _behaviour.profile.vignette.settings;
                vignette.intensity = intensity / VignetteDiv;
                _behaviour.profile.vignette.settings = vignette;

                var grain = _behaviour.profile.grain.settings;
                grain.intensity = intensity / GrainDiv;
                _behaviour.profile.grain.settings = grain;
            }
        }
    }
}
