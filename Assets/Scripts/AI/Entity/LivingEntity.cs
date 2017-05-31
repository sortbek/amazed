using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.AI.Entity.Behaviours;
using UnityEngine;

namespace Assets.Scripts.AI.Entity {
    public class LivingEntity : MonoBehaviour {

        [SerializeField]
        public float Health = 10f;
        [SerializeField]
        public float Energy = 8f;
        [SerializeField] public float Speed = 5.0f;
        [SerializeField] public Vector3 Target;

        private IEntityBehaviour _currentBehaviour;
        private UnityEngine.Animation _animation;

        public bool Dead;

        public void SetBehaviour(IEntityBehaviour behaviour) {
            _currentBehaviour = behaviour;
        }

        public IEntityBehaviour GetCurrentBehaviour() {
            return _currentBehaviour;
        }

        void Update() {
            if (_currentBehaviour != null && !Dead)
                transform.position = _currentBehaviour.Update(this);
        }

        public void PlayAnimation(Animation animation) {
            if (_animation == null) _animation = GetComponentInChildren<UnityEngine.Animation>();
            switch (animation) {
                case Animation.Attack:
                    _animation.Play("attack1");
                    break;
                case Animation.Walk:
                    _animation.Play("walk");
                    break;
                case Animation.Run:
                    _animation.Play("run");
                    break;
                case Animation.Idle:
                    _animation.Play("idle");
                    break;
                case Animation.Death:
                    _animation.Play("death");
                    break;
            }
        }

        void OnCollisionEnter() {
            if (!Dead) {
                PlayAnimation(Animation.Death);
                GetComponent<CapsuleCollider>().enabled = false;
            }
            Dead = true;
        }


    }

    public enum Animation {
        Attack, Walk, Idle, Run, Death
    }
}
