using System;
using Assets.Scripts.AI.Entity.Behaviours;
using UnityEngine;

namespace Assets.Scripts.AI.Entity {
    // Created by:
    // Eelco Eikelboom     Hugo Kamps
    // S1080542            S1084074
    public class LivingEntity : MonoBehaviour {
        private UnityEngine.Animation _animation;

        private AbstractEntityBehaviour _currentBehaviour;

        public bool Dead;

        [SerializeField] public float Energy = 8f;

        [SerializeField] public float Health = 10f;

        [SerializeField] public float Speed = 5.0f;

        public void SetBehaviour(AbstractEntityBehaviour behaviour) {
            _currentBehaviour = behaviour;
        }

        public AbstractEntityBehaviour GetCurrentBehaviour() {
            return _currentBehaviour;
        }

        private void Update() {
            if (_currentBehaviour != null && !Dead)
                transform.position = _currentBehaviour.Update();
        }

        public void PlayAnimation(Animation animation) {
            if (_animation == null) _animation = GetComponentInChildren<UnityEngine.Animation>();
            _animation.Play(Enum.GetName(typeof(Animation), animation));
        }

        private void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.tag.Equals("weapon")){
                Debug.Log("WTF dude!");
                Health -= 1.0f;

                if (!(Health <= 0.0f)) return;

                Dead = true;
                PlayAnimation(Animation.death);
                GetComponent<CapsuleCollider>().enabled = false;
            }
        }
    }

    public enum Animation {
        attack1,
        walk,
        idle,
        run,
        death
    }
}