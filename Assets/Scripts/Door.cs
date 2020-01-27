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

        //public static void GetAdjacentRoomLocAndRot(Transform transformA, Door doorA, Door doorB, out Vector3 roomBLoc, out float roomBRot) {
        //    float doorARotation = transformA.localRotation.eulerAngles.y + RoguelikePGUtility.AngleTo(Direction.North, doorA.dir);
        //    roomBRot = doorARotation + 180 + RoguelikePGUtility.AngleTo(doorB.dir, Direction.North);
        //    roomBRot %= 360;
        //    roomBLoc = transformA.localPosition; // Start at Room A location
        //    roomBLoc += Quaternion.AngleAxis(transformA.localRotation.eulerAngles.y, Vector3.up) * doorA.location; // Move to the door location
        //    roomBLoc -= Quaternion.AngleAxis(roomBRot, Vector3.up) * doorB.location;
        //    // Thank you NCarter from https://forum.unity.com/threads/rotating-a-vector-by-an-eular-angle.18485/
        //}
        public static void GetAdjacentRoomLocAndRot(Transform transformA, Door doorA, Door doorB, out Vector3 roomBLoc, out float roomBRot)
        {
            GetAdjacentRoomLocAndRot(doorA, transformA.localPosition, transformA.localRotation.eulerAngles.y, doorB, out roomBLoc, out roomBRot);
        }

        public static void GetAdjacentRoomLocAndRot(Door doorA, Vector3 locA, float rotA, Door doorB, out Vector3 locB, out float rotB)
        {
            rotB = rotA                                                   // Start with the rotational offset of Room A,
                + RoguelikePGUtility.AngleTo(Direction.North, doorA.dir)  // add the offset for Door A relative to Room A,
                + 180                                                     // flip around since Door B points the opposite direction of Door A
                + RoguelikePGUtility.AngleTo(doorB.dir, Direction.North); // add the offset for Room B relative to Door B
            rotB %= 360;                                                  // Reestablish rotB within [0, 360)
            locB = locA; // Start at Room A location
            locB += Quaternion.AngleAxis(rotA, Vector3.up) * doorA.location; // Move to the door location
            locB -= Quaternion.AngleAxis(rotB, Vector3.up) * doorB.location;
            // Thank you NCarter from https://forum.unity.com/threads/rotating-a-vector-by-an-eular-angle.18485/
        }
    }
}
