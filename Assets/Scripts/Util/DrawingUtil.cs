using System.Collections.Generic;
using UnityEngine;

// Created By:
// Jordi Wolthuis
// S1085303

public class DrawingUtil : MonoBehaviour {
    public const float Speed = 5f;
    public const float BaseHeight = -1f;
    public const float ParticleHeight = 4f;
    public int NodeIndex;

    public GameObject Particle;
    public List<GameObject> ParticleClone = new List<GameObject>();
    public GameObject ParticleGo;

    public List<Vector3> Path;
    public Vector3 Playerloc;

    private void Update() {
        if (Path == null || Path.Count == 0 || Path.Count <= NodeIndex)
            return;

        //define next node
        var nextNode = new Vector3(
            Path[NodeIndex].x,
            Path[NodeIndex].y + ParticleHeight,
            Path[NodeIndex].z);

        // Make the gameobject move forward (own position, target, movementspeed)
        ParticleGo.transform.position = Vector3.MoveTowards(
            ParticleGo.transform.position, nextNode, Time.deltaTime * Speed);

        // If reached node, go to next node
        if (Vector3.Distance(nextNode, ParticleGo.transform.position) < 0.1f) NodeIndex++;
    }

    public void SpawnDust(Vector3[] path, Vector3 loc) {
        Path = new List<Vector3>(path);
        // inserts player location at the start of the list 
        Path.Insert(0, loc);

        ParticleGo = Instantiate(Particle, loc, transform.rotation);
    }
}