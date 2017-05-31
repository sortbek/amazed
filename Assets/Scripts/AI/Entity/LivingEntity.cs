using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.AI.Entity.Behaviours;
using UnityEngine;

namespace Assets.Scripts.AI.Entity {
    public class LivingEntity : MonoBehaviour {

        [SerializeField]
        public float Health = 10f;
        [SerializeField]
        public float Energy = 8f;

        private IEntityBehaviour _currentBehaviour;

        public void SetBehaviour(IEntityBehaviour behaviour) {
            _currentBehaviour = behaviour;
        }

        public IEntityBehaviour GetCurrentBehaviour() {
            return _currentBehaviour;
        }

        void Update() {
            if (_currentBehaviour != null)
                transform.position = _currentBehaviour.Update(this);
        }

    }
}
