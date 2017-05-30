using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Assets.Scripts.AI.Entity.Behaviours {
    public class EntityWanderBehaviour : IEntityBehaviour {

        private float _speed, _interval, _maxHeadingChange, _heading;
        private Vector3 _targetRotation;
        private LivingEntity _entity;

        public void Load(LivingEntity entity) {
            _entity = entity;
            _speed = 5f;
            _interval = 1f;
            _maxHeadingChange = 30f;
        }

        private void CalculateHeadingRoutine() {
            var floor = Mathf.Clamp(_heading - _maxHeadingChange, 0, 360);
            var ceil = Mathf.Clamp(_heading + _maxHeadingChange, 0, 360);
            _heading = Random.Range(floor, ceil);
            _targetRotation = new Vector3(0, _heading, 0);
        }

        private IEnumerator CalculateHeading() {
            while (true) {
                CalculateHeadingRoutine();
                yield return new WaitForSeconds(_interval);
            }
        }

        public Vector3 Update(LivingEntity entity) {
            var transform = entity.transform;
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, _targetRotation, Time.deltaTime * _interval);
            var forward = transform.TransformDirection(Vector3.forward);
            return forward * _speed;
        }

        private Vector3 RandomSphere(Vector3 origin, float dist, int layermask) {
            Vector3 randDirection = Random.insideUnitSphere * dist;
            randDirection += origin;
            NavMeshHit navHit;
            NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
            return navHit.position;
        }
    }
}
