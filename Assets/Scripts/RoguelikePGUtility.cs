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

        public static void RoundVector3(Vector3 vector3) {
            //Debug.Log("unrounded vector3: " + vector3);
            vector3.x = Mathf.Round(vector3.x);
            vector3.y = Mathf.Round(vector3.y);
            vector3.z = Mathf.Round(vector3.z);
            //Debug.Log("round vector3: " + vector3);
        }
    }
}
