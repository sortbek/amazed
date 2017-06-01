using UnityEngine;
using Assets.Scripts.pathfinding;
using Assets.Scripts.World;

namespace Assets.Scripts.Items.Potions {

    // Created by:
    // Hugo Kamps
    // S1084074
    class GuidancePotion : Potion {
        public GameObject Character;

        public DrawingUtil DrawUtil;

        public Vector3 Playerloc;
        public Vector3[] NodeList;
        public Vector3[] Path;

        public GuidancePotion(Character.Character player) : base(player) {
            Texture = (Texture) Resources.Load("Sprites/potion_guidance", typeof(Texture));

            Duration = 10;
            Amount = 1;
        }

        public override void Use() {
            Playerloc = GameManager.Instance.Character.transform.position;
            PathRequestManager.RequestPath(Playerloc, GameManager.Instance.GetEndpoint(), OnPathFound);
        }

        public void OnPathFound(Vector3[] newPath, bool pathFound) {
            if (pathFound) {
                var go = GameObject.Find("DrawDust");
                DrawUtil = go.GetComponent<DrawingUtil>();

                Path = newPath;
                DrawUtil.SpawnDust(Path, Playerloc);

                base.Use();
            }
        }
    }
}