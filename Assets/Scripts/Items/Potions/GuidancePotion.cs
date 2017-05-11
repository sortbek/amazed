using System;
using Assets.Scripts.pathfinding;
using Assets.Scripts.World;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Items.Potions
{
    class GuidancePotion : Potion
    {
        public GameObject character;
        public Vector3 playerloc;

        //testvar
        public Vector3 start;
        public Vector3 end;

        private bool test;
        public Vector3[] _path;

        public GuidancePotion(Character.Character player) : base(player)
        {
            Texture = (Texture) Resources.Load("Sprites/potion_guidance", typeof(Texture));  
            test = true;
            character = GameObject.Find("Character");
            Duration = 10;
        }

        public override void Use()
        {
            playerloc = character.transform.position;
            PathRequestManager.RequestPath(playerloc, GameManager.Instance.GetEndpoint(), OnPathFound);

            Debug.Log("rawr");

            //foreach (Vector3 v in _path)
            //{
            //    Debug.Log(v + "sf asf ");

            //}
            //Debug.Log(playerloc);

            //if (test)
            //{
            playerloc.y += 4;
            _path[0].y += 4;
            DrawLine(playerloc, _path[0], Color.red, 10);
            for (int i = 0; i < _path.Length - 1; i++)
                {            
                    start = _path[i];
                    end = _path[i + 1];
                    start.y += 4;
                    end.y += 4;
                    DrawLine(start,end,Color.cyan,10);
                }
                //DrawLine(playerloc, newloc, Color.blue, 10);
              //  test = false;
            //}


            //test = false;
            base.Use();
        }

        public override void RemoveEffect()
        {
            test = true;
            base.RemoveEffect();
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

        

        public void OnPathFound(Vector3[] newPath, bool pathFound)
        {
            if (pathFound)
            {
                _path = newPath;
            }
        }

    }
}
