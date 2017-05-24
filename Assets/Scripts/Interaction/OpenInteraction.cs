using Assets.Scripts.Character;
using UnityEngine;

namespace Interaction {
    public class OpenInteraction : SearchInteraction {
        public bool IsOpen;

        private Animator _animator;

        protected override void Start() {
            base.Start();
            _animator = GetComponent<Animator>();
        }

        protected override void Interact(Character actor) {
            _animator.Play(IsOpen ? "ChestClose" : "ChestOpen");

            ClearInteraction();
        }

        public override void PossibleInteraction(Character actor) {
            ClearInteraction();

            if (Interaction == null) return;

            if (IsOpen && !HasBeenInteractedWith) {
                base.PossibleInteraction(actor);
                if (Input.GetKeyDown(KeyCode.F)) {
                    base.Interact(actor);
                }
            }
            else {
                Interaction.color = Color.red;
                Interaction.text = IsOpen
                    ? string.Format("Press 'F' to close {0}", Name)
                    : string.Format("Press 'F' to open {0}", Name);

                if (Input.GetKeyDown(KeyCode.F)) {
                    Interact(actor);
                    IsOpen = !IsOpen;
                }
            }
        }
    }
}