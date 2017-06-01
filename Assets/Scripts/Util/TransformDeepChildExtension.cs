using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Util {

    // Created by:
    // Eelco Eikelboom
    // S1080542
    public static class TransformDeepChildExtension {

        /// <summary>
        /// Uses BFS technique to find any child under a parent.
        /// The normal Find() method only looks for direct children.
        /// This method finds grandchildren aswell.
        /// </summary>
        /// <param name="parent">The parent object</param>
        /// <param name="name">The name of the gameobject</param>
        /// <returns>An instance of the found object</returns>
        public static Transform FindDeepChild(this Transform parent, string name) {
            var result = parent.Find(name);
            if (result != null)
                return result;
            foreach (Transform child in parent) {
                result = child.FindDeepChild(name);
                if (result != null)
                    return result;
            }
            return null;
        }
    }
}
