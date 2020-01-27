using UnityEngine;

namespace RoguelikePG {
    [System.Serializable]
    public class Door {
        public Vector3 location;
        public Direction dir;

        public Door(Vector3 location, Direction dir) {
            this.location = location;
            this.dir = dir;
        }

        public static void GetAdjacentRoomLocAndRot(Door doorA, Vector3 locA, float rotA, Door doorB, out Vector3 locB, out float rotB) {
            rotB = rotA                                        // Start with the rotational offset of Room A,
                + RPGUtil.AngleTo(Direction.North, doorA.dir)  // add the offset for Door A relative to Room A,
                + 180                                          // flip around since Door B points the opposite direction of Door A
                + RPGUtil.AngleTo(doorB.dir, Direction.North); // add the offset for Room B relative to Door B
            rotB %= 360;                                       // Reestablish rotB within [0, 360)
            rotB = Mathf.RoundToInt(rotB / 90) * 90;           // Scrub any error

            locB = locA; // Start at Room A location
            locB += Quaternion.AngleAxis(rotA, Vector3.up) * doorA.location; // Move to the mutual Door location
            locB -= Quaternion.AngleAxis(rotB, Vector3.up) * doorB.location; // Move to the Room B location
            locB = RPGUtil.RoundVec3(locB); // Scrub any error

            // Thank you NCarter from https://forum.unity.com/threads/rotating-a-vector-by-an-eular-angle.18485/
        }
    }
}
