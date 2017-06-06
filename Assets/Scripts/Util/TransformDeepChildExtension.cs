using UnityEngine;

namespace Assets.Scripts.Util {
    public static class TransformDeepChildExtension {
        /// <summary>
        ///     Method found at: http://answers.unity3d.com/questions/799429/transformfindstring-no-longer-finds-grandchild.html
        ///     Uses BFS technique to find any child under a parent.
        ///     The normal Find() method only looks for direct children.
        ///     This method finds grandchildren aswell.
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