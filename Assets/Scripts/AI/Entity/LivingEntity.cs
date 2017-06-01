using System;
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

        private AbstractEntityBehaviour _currentBehaviour;
        private UnityEngine.Animation _animation;

        public bool Dead;

        public void SetBehaviour(AbstractEntityBehaviour behaviour) {
            _currentBehaviour = behaviour;
        }

        public AbstractEntityBehaviour GetCurrentBehaviour() {
            return _currentBehaviour;
        }

        void Update() {
            if (_currentBehaviour != null && !Dead)
                transform.position = _currentBehaviour.Update();
        }

        public void PlayAnimation(Animation animation) {
            if (_animation == null) _animation = GetComponentInChildren<UnityEngine.Animation>();
            _animation.Play(Enum.GetName(typeof(Animation), animation));
        }

        void OnCollisionEnter() {
            if (!Dead) {
                PlayAnimation(Animation.death);
                GetComponent<CapsuleCollider>().enabled = false;
            }
            Dead = true;
        }


    }

    public enum Animation {
        attack1, walk, idle, run, death
    }
}
