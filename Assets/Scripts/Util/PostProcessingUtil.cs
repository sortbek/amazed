using Assets.Scripts.World;
using UnityEngine;
using UnityEngine.PostProcessing;

namespace Assets.Scripts.Util {
    internal class PostProcessingUtil : MonoBehaviour {
        private const int VignetteDiv = 140, GrainDiv = 300;
        private PostProcessingBehaviour _behaviour;
        private Character.Character _character;

        private void Start() {
            _behaviour = GetComponent<PostProcessingBehaviour>();
        }

        private void Update() {
            if (_character == null) {
                _character = GameManager.Instance.Character;
            }
            else {
                var intensity = Character.Character.MaxHealth - _character.Health;

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