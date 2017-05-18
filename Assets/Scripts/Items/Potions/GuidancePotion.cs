
using UnityEngine;
using System;
using Assets.Scripts.pathfinding;
using Assets.Scripts.World;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Assets.Scripts.Items.Potions
{
    class GuidancePotion : Potion
    {
        public GameObject character;
        
        public DrawingUtil drawUtil;

        public Vector3 playerloc;
        public Vector3[] nodeList;
        public Vector3[] _path;

        public GuidancePotion(Character.Character player) : base(player)
        {
            Texture = (Texture) Resources.Load("Sprites/potion_guidance", typeof(Texture));
            character = GameObject.Find("Character");
            var go = GameObject.Find("DrawDust");
            drawUtil = go.GetComponent<DrawingUtil>();
            Duration = 10;
        }

        public override void Use()
        {
            playerloc = character.transform.position;
            PathRequestManager.RequestPath(playerloc, GameManager.Instance.GetEndpoint(), OnPathFound);
        }

        public override void RemoveEffect()
        {
            base.RemoveEffect();
        }


        public void OnPathFound(Vector3[] newPath, bool pathFound) {

            if (pathFound) {

                _path = newPath;
                drawUtil.drawlineFromTo(_path, playerloc);

                base.Use();
            }
        }
    }

}
