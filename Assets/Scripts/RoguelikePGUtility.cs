using System.Collections;
using System.Collections.Generic;

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
    }
}
