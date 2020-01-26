using System.Collections;
using System.Collections.Generic;
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

        //public Transform GetAdjacentRoomTransform(Transform , Door adjacentDoor) {
        //    float adjustedDir = my

        //    return null;
        //}

        public static void GetAdjacentRoomLocAndRot(Transform transformA, Door doorA, Door doorB, out Vector3 roomBLoc, out float roomBRot) {
            float doorARotation = transformA.localRotation.eulerAngles.y + RoguelikePGUtility.AngleTo(Direction.North, doorA.dir);
            roomBRot = doorARotation + 180 + RoguelikePGUtility.AngleTo(doorB.dir, Direction.North);
            roomBRot %= 360;
            roomBLoc = transformA.localPosition; // Start at Room A location
            roomBLoc += Quaternion.AngleAxis(transformA.localRotation.eulerAngles.y, Vector3.up) * doorA.location; // Move to the door location
            Debug.Log("local y rot: " + transformA.localRotation.eulerAngles.y);
            roomBLoc -= Quaternion.AngleAxis(roomBRot, Vector3.up) * doorB.location;
            // Thank you NCarter from https://forum.unity.com/threads/rotating-a-vector-by-an-eular-angle.18485/
        }
    }
}
