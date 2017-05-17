using System;
using Assets.Scripts.pathfinding;
using Assets.Scripts.World;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Items.Potions
{
    class GuidancePotion : Potion
    {
        public GameObject Character;
        public Vector3 Playerloc;

        //testvar
        public Vector3 Start;
        public Vector3 End;

        private bool _test;
        public Vector3[] Path;

        public GuidancePotion(Character.Character player) : base(player)
        {
            Texture = (Texture) Resources.Load("Sprites/potion_guidance", typeof(Texture));  
            Character = GameObject.Find("Character");
            Duration = 10;
        }

        public override void Use()
        {
            Playerloc = Character.transform.position;
            PathRequestManager.RequestPath(Playerloc, GameManager.Instance.GetEndpoint(), OnPathFound);

            Playerloc.y += 4;
            Path[0].y += 4;
            DrawLine(Playerloc, Path[0], Color.red, 10);
            for (int i = 0; i < Path.Length - 1; i++)
                {            
                    Start = Path[i];
                    End = Path[i + 1];
                    Start.y += 4;
                    End.y += 4;
                    DrawLine(Start,End,Color.cyan,10);
                }

            base.Use();
        }

        public override void RemoveEffect()
        {
            base.RemoveEffect();
        }

        void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
        {
            GameObject myLine = new GameObject();
            myLine.transform.position = start;
            myLine.AddComponent<LineRenderer>();
            LineRenderer lr = myLine.GetComponent<LineRenderer>();
            lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
            lr.startColor = color;
            lr.endColor = color;
            lr.startWidth = 0.1f;
            lr.endWidth = 0.1f;
            lr.SetPosition(0, start);
            lr.SetPosition(1, end);
            GameObject.Destroy(myLine, duration);
        }

        public void OnPathFound(Vector3[] newPath, bool pathFound)
        {
            if (pathFound)
            {
                Path = newPath;
            }
        }

    }
}
