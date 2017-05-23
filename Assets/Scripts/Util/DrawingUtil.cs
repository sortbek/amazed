using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class DrawingUtil : MonoBehaviour
{

    public Vector3 playerloc;

    public const float speed = 3f;
    public const float baseHeight = -1f;
    public const float particleHeight = 4f;

    public List<Vector3> _path;
    public int nodeIndex;

    public GameObject particle;
    public GameObject particleGO;
    public List<GameObject> particleClone = new List<GameObject>();

    public void drawlineFromTo(Vector3[] path, Vector3 playerloc)
    {
        //playerloc.y -= 2;
        //DrawLine(playerloc, path[0], Color.red, 10);

        //spawnDust();

        //GameObject particleclone = (GameObject)Instantiate(particle, playerloc, transform.rotation);
        StartCoroutine(spawnDust(path, playerloc, 10));


    }


    void Update()
    {
        if (_path == null || _path.Count == 0 || _path.Count <= nodeIndex)
            return;

        Vector3 nextNode = new Vector3(
            _path[nodeIndex].x,
            _path[nodeIndex].y + particleHeight,
            _path[nodeIndex].z);

        Debug.Log("Current: " + particleGO.transform.position + " - Target: " + nextNode);

        particleGO.transform.position = Vector3.MoveTowards(
            particleGO.transform.position, nextNode, Time.deltaTime * speed);
        
        // If reached node, go to next node
        if (Vector3.Distance(nextNode, particleGO.transform.position) < 0.1f) {
            nodeIndex++;
            Debug.Log("NextNode");
        }
    }


    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetColors(color, color);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }

    public IEnumerator spawnDust(Vector3[] path, Vector3 loc, int time) {
        
        _path = new List<Vector3>(path);
        _path.Insert(0, loc);

        particleGO = Instantiate(particle, loc, transform.rotation);
        
        //for (int i = 0; i < path.Length - 1; i++)
        //{


        //    start = path[i];
        //    end = path[i + 1];
        //    start.y += 2;
        //    // end.y += 4;
        //    var gameobject = Instantiate(particle, start, transform.rotation);
        //    particleClone.Add(gameobject);
        //    // DrawLine(start, end, Color.cyan, 60);
        //}

        yield return new WaitForSeconds(time);

        //foreach (var par in particleClone)
        //{
        //    Destroy(par);
        //}
    }


}
