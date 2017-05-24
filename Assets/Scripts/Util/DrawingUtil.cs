using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class DrawingUtil : MonoBehaviour {
    public Vector3 playerloc;

    public const float speed = 5f;
    public const float baseHeight = -1f;
    public const float particleHeight = 4f;

    public List<Vector3> _path;
    public int nodeIndex;

    public GameObject particle;
    public GameObject particleGO;
    public List<GameObject> particleClone = new List<GameObject>();

    private void Update() {
        if (_path == null || _path.Count == 0 || _path.Count <= nodeIndex)
            return;

        //define next node
        var nextNode = new Vector3(
            _path[nodeIndex].x,
            _path[nodeIndex].y + particleHeight,
            _path[nodeIndex].z);

        // Make the gameobject move forward (own position, target, movementspeed)
        particleGO.transform.position = Vector3.MoveTowards(
            particleGO.transform.position, nextNode, Time.deltaTime * speed);

        // If reached node, go to next node
        if (Vector3.Distance(nextNode, particleGO.transform.position) < 0.1f) nodeIndex++;
    }

    public void SpawnDust(Vector3[] path, Vector3 loc) {
        _path = new List<Vector3>(path);
        // inserts player location at the start of the list 
        _path.Insert(0, loc);

        particleGO = Instantiate(particle, loc, transform.rotation);
    }
}