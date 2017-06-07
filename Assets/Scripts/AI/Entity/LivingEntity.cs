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

        public bool Dead { get; set; }
        public LivingEntityPerspective Perspective { get; private set; }

        [SerializeField] public float Energy = 8f;
        [SerializeField] public float Health = 10f;
        [SerializeField] public float Speed = 5.0f;

        public void SetBehaviour(AbstractEntityBehaviour behaviour) {
            if (_currentBehaviour != null && behaviour == _currentBehaviour) return;
            _currentBehaviour = behaviour;
        }

        private void Awake() {
            Perspective = new LivingEntityPerspective(this);
        }

        private void Update() {
            if (_currentBehaviour != null && !Dead) 
                transform.position = _currentBehaviour.Update();
        }

        public AbstractEntityBehaviour GetCurrentBehaviour() {
            return _currentBehaviour;
        }

        public void PlayAnimation(Animation animation) {
            if (_animation == null)
                _animation = GetComponentInChildren<UnityEngine.Animation>();
            _animation.Play(Enum.GetName(typeof(Animation), animation).ToLower());
        }

        private void OnCollisionEnter(Collision collision) {
            Health -= 2.0f;

            if (!(Health <= 0.0f)) return;

            // TODO: Add points to players score
            Dead = true;
            PlayAnimation(Animation.Death);
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponentInChildren<MeshCollider>().enabled = false;
            GetComponentInChildren<BoxCollider>().enabled = false;
        }
        
        public void Rotate(Vector3 dir, float rotationSpeed = 10f) {
            dir.y = 0;
            
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward,
                dir - transform.position,
                Time.deltaTime * rotationSpeed, 0.0f));
        }
    }

    public enum Animation {
        Attack1,
        Walk,
        Idle,
        Run,
        Death
    }
}