using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DrawingUtil : MonoBehaviour {

    public Vector3 start;
    public Vector3 end;

    public GameObject particle;
    public List<GameObject> particleClone = new List<GameObject>();

    void Awake() {
        DontDestroyOnLoad(this);
    }

    public void drawlineFromTo(Vector3[] path, Vector3 playerloc)
    {
        playerloc.y += 4;
        //DrawLine(playerloc, path[0], Color.red, 10);
        
        //spawnDust();
        
        //GameObject particleclone = (GameObject)Instantiate(particle, playerloc, transform.rotation);
        StartCoroutine(spawnDust(path, playerloc, 10));


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

    public IEnumerator spawnDust(Vector3[] path, Vector3 loc, int time)
    {

        for (int i = 0; i < path.Length - 1; i++)
        {
           

            start = path[i];
            end = path[i + 1];
            start.y += 2;
            // end.y += 4;
            var gameobject = Instantiate(particle, start, transform.rotation);
            particleClone.Add(gameobject);
            // DrawLine(start, end, Color.cyan, 60);
        }
        
        yield return new WaitForSeconds(time);

        foreach (var par in particleClone)
        {
            Destroy(par);
        }
    }


}
