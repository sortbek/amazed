using System.Collections;
using Assets.Scripts.Character;
using UnityEngine;

// Created By:
// Niek van den Brink
// S1078937
namespace Interaction {
    public class OpenInteraction : SearchInteraction {
        private bool _animating;

        private Animator _animator;
        public bool IsOpen;

        protected override void Start() {
            base.Start();
            _animator = GetComponent<Animator>();
            _animating = false;
        }

        private void Animating() {
            StartCoroutine(Animate());
        }

        private IEnumerator Animate() {
            _animating = true;
            _animator.Play(IsOpen ? "ChestClose" : "ChestOpen");
            yield return new WaitForSeconds(1);
            _animating = false;
        }

        protected override void Interact(Character actor) {
            if (IsOpen && !HasBeenInteractedWith) {
                base.Interact(actor);
                return;
            }

            Animating();
            ClearInteraction();
            IsOpen = !IsOpen;
        }

        public override void PossibleInteraction(Character actor) {
            ClearInteraction();

            if (Interaction == null) return;

            if (IsOpen && !HasBeenInteractedWith) {
                base.PossibleInteraction(actor);
                return;
            }

            if (_animating) return;
            if (ShowEventLog) return;
            Interaction.text = IsOpen
                ? string.Format("[F] Close {0}", Name)
                : string.Format("[F] Open {0}", Name);

            if (Input.GetKeyDown(KeyCode.F)) Interact(actor);
        }
    }
}