using UnityEngine;

namespace Assets.Scripts.AI.Entity {
    public class LivingEntityPerspective {
        private readonly Camera _camera;
        private readonly LivingEntity _entity;

        public LivingEntityPerspective(LivingEntity entity) {
            _entity = entity;
            _camera = entity.GetComponentInChildren<Camera>();
        }

        //Check whether the given axis is within the FOV (which is always between 0 and 1)
        private bool InFov(float axis) {
            return axis >= 0 && axis <= 1;
        }

        //Determines whether the given gameobject is visible by the LivingEntity
        public bool Visible(GameObject target) {
            //Fetch the viewport from the camera based on the given position
            //and check if the target is within the bounds of that viewport 
            //aswell as in front of the camera (z > 0)
            var viewPort = _camera.WorldToViewportPoint(target.transform.position);
            if (!(viewPort.z > 0) || !InFov(viewPort.x) || !InFov(viewPort.y))
                return false;
            //Use the raycast to fetch the first gameobject that is in front of the camera
            //based on the collision with that raycasthit
            RaycastHit hit;
            var direction = _entity.transform.position - target.transform.position;
            if (!Physics.Raycast(target.transform.position, direction, out hit))
                return true;
            return hit.collider.name == _entity.name;
        }
    }
}