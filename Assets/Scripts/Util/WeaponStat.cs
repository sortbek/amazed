using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Util {

    // Created by:
    // Eelco Eikelboom
    // S1080542
    public class WeaponStat : MonoBehaviour {
        [SerializeField]
        public float Damage = 1f;
        [SerializeField]
        public float Knockback = .5f;
        [SerializeField]
        public int WeaponID = 0;
        [SerializeField]
        public bool Default = false;

    }
}
