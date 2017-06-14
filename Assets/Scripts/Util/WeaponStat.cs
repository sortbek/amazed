using UnityEngine;

namespace Assets.Scripts.Util {
    // Created by:
    // Eelco Eikelboom
    // S1080542
    public class WeaponStat : MonoBehaviour {
        [SerializeField] public string AnimationTag;

        [SerializeField] public float Damage = 1f;

        [SerializeField] public bool Default = false;

        [SerializeField] public float Knockback = .5f;

        [SerializeField] public int WeaponId = 0;
    }
}