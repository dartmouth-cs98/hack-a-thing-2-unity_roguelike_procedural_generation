using System;
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

    public class RPGUtil
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

        public static string Vector3LongString(Vector3 vector3)
        {
            return String.Format("<{0:F12}, {1:F12}, {2:F12}>", vector3.x, vector3.y, vector3.z);
        }

        public static Vector3 RoundVec3(Vector3 vector3, int bits) {
            return RoundVec3(vector3 * (1 << bits)) / (1 << bits);
        }

        public static Vector3 RoundVec3(Vector3 vector3) {
            vector3.x = Mathf.RoundToInt(vector3.x);
            vector3.y = Mathf.RoundToInt(vector3.y);
            vector3.z = Mathf.RoundToInt(vector3.z);
            return vector3;
        }
    }
}
