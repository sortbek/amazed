using Assets.Scripts.Character;
using UnityEngine;

namespace Interaction{
	public class OpenInteraction : InteractionBehaviour{
		public bool IsOpen;

		private Animator _animator;

		protected override void Start(){
			base.Start();
			_animator = GetComponent<Animator>();
			_animator.Play(IsOpen ? "ChestClose" : "ChestOpen");
		}

		protected override void Interact(Character actor){
			_animator.Play(IsOpen ? "ChestClose" : "ChestOpen");
			IsOpen = !IsOpen;

			base.Interact(actor);
		}

		public override void PossibleInteraction(Character actor){
			base.PossibleInteraction(actor);

			if (Interaction == null) return;

			Interaction.text = IsOpen? string.Format("Press 'F' to close {0}", Name) : string.Format("Press 'F' to open {0}", Name);

			if (Input.GetKeyDown(KeyCode.F)) {
				Interact(actor);
			}
		}
	}
}
