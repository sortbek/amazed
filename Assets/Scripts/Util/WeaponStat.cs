using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Util {
    public class WeaponStat : MonoBehaviour {

        [SerializeField]
        public float Damage = 1f;
        [SerializeField]
        public float Knockback = .5f;

    }
}
