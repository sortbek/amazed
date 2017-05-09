using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.World;
using UnityEngine;

namespace Assets.Scripts.pathfinding
{
    public class NewBehaviourScript : MonoBehaviour {

        // private PathRequestManager _requestManager;
        private Generator _generator;

        void Awake()
        {
            //_requestManager = GetComponent<PathRequestManager>();
            _generator = GetComponent<Generator>();
        }

        IEnumerator FindPath(Vector3 startPos, Vector3 endPos)
        {
            var waypoints = new GridNode[0];
            var pathFound = false;

            var openSet = new Heap<GridNode>(GameManager.Instance.Size);
            var closedSet = new HashSet<GridNode>();


            yield return null;
        }
    }
}



