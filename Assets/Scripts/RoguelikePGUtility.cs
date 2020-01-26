using UnityEngine;

namespace RoguelikePG
{
    [System.Serializable]
    public enum Direction : byte
    {
        East,
        North,
        West,
        South
    }

    public class RoguelikePGUtility
    {
        public static float AngleTo(Direction from, Direction to) {
            return 90 * ((from - to) % 4);
        }

        public static string Vector3ArrayString(Vector3[] vector3s) {
            string retStr = "[";

            for (int i = 0; i < vector3s.Length; i++) {
                if (i != 0) retStr += ", ";
                retStr += vector3s[i];
            }

            return retStr + "]";
        }
    }
}
