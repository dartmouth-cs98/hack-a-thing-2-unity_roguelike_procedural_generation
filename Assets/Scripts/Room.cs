using UnityEngine;
using RoguelikePG;


public class Room : MonoBehaviour {
    public Cell[] Cells;
    public Door[] Doors;

    //void Start() { } void Update() { }

    /* **************** RoomsOverlap() ****************
     * Determines whether two rooms overlap. A more appropriate name would be
     * "RoomsOverlapOrBlockADoor()", but it's unlikely that the functionality
     * would be needed where Rooms that don't overlap but do block door(s)
     * should return false, so these two operations are combined.
     * 
     * Parameters:
     *   roomA (Room): The first room to evaluate
     *   locA (Vector3): Local position Vector3 of roomA
     *   rotA (float): Local y rotation of roomA
     *   roomB (Room): The second room to evaluate
     *   locB (Vector3): Local position Vector3 of roomB
     *   rotB (float): Local y rotation of roomB
     * 
     * Returns:
     *   bool:
     *     true if there exists shared volume between the Cells of the Rooms
     *     true if there exists some door1 of cell1 that is on cell2 and does
     *       not have a corresponding door2 of cell2 that shares the same
     *       location
     *     false otherwise
     */
    public static bool RoomsOverlap(Room roomA, Vector3 locA, float rotA, Room roomB, Vector3 locB, float rotB) {
        Vector3[] doorsOnCellsA = new Vector3[roomA.Doors.Length];
        Vector3[] doorsOnCellsB = new Vector3[roomB.Doors.Length];

        foreach (Cell cellA in roomA.Cells) {
            Vector3Int minCornerA, maxCornerA;
            GetMinAndMaxCorners(cellA, locA, rotA, out minCornerA, out maxCornerA);

            foreach (Cell cellB in roomB.Cells) {
                Vector3Int minCornerB, maxCornerB;
                GetMinAndMaxCorners(cellB, locB, rotB, out minCornerB, out maxCornerB);

                if (Mathf.Max(minCornerA.x, minCornerB.x) < Mathf.Min(maxCornerA.x, maxCornerB.x) &&
                    Mathf.Max(minCornerA.y, minCornerB.y) < Mathf.Min(maxCornerA.y, maxCornerB.y) &&
                    Mathf.Max(minCornerA.z, minCornerB.z) < Mathf.Min(maxCornerA.z, maxCornerB.z))
                {
                    return true;
                }

                for (int dA = 0; dA < roomA.Doors.Length; dA++) {
                    Vector3 doorOnCellA;

                    if (DoorIsOnCell(roomA.Doors[dA], locA, rotA, cellB, locB, rotB, out doorOnCellA)) {
                        doorsOnCellsA[dA] = doorOnCellA;
                    }
                }
            }

            for (int dB = 0; dB < roomB.Doors.Length; dB++)
            {
                Vector3 doorOnCellB;

                if (DoorIsOnCell(roomB.Doors[dB], locB, rotB, cellA, locA, rotA, out doorOnCellB))
                {
                    doorsOnCellsB[dB] = doorOnCellB;
                }
            }
        }

        Debug.Log("doors on cells: " + RoguelikePGUtility.Vector3ArrayString(doorsOnCellsA) + ",\n " + RoguelikePGUtility.Vector3ArrayString(doorsOnCellsB));

        for (int dA = 0; dA < roomA.Doors.Length; dA++) {
            for (int dB = 0; dB < roomB.Doors.Length; dB++) {
                if (doorsOnCellsA[dA] != Vector3.zero && doorsOnCellsA[dA] == doorsOnCellsB[dB]) {
                    Debug.Log("match found: " + doorsOnCellsA[dA]);
                    doorsOnCellsA[dA] = Vector3.zero;
                    doorsOnCellsB[dB] = Vector3.zero;
                }

                if (dA == roomA.Doors.Length - 1 && doorsOnCellsB[dB] != Vector3.zero) return true;
            }

            if (doorsOnCellsA[dA] != Vector3.zero) return true;
        }

        return false;
    }

    /* **************** GetMinAndMaxCorners() ****************
     * Void method that calculates the min and max corners of a Cell, taking
     * into account the Cell's Room's location and rotation.
     * 
     * Parameters:
     *   cell (Cell): Cell to evaluate
     *   loc (Vector3): Local position Vector3 of the Room that the Cell is in
     *   rot (float): Local y rotation of the Room that the Cell is in
     * 
     * Out Parameters:
     *   minCorner (Vector3Int): Vector3Int with the minimum x, y, and z values
     *     out of the corner Vector3s of the Cell, adjusted for location and
     *     rotation
     *   maxCorner (Vector3Int): Vector3Int with the maximum x, y, and z values
     *     out of the corner Vector3s of the Cell, adjusted for location and
     *     rotation
     */
    private static void GetMinAndMaxCorners(Cell cell, Vector3 loc, float rot, out Vector3Int minCorner, out Vector3Int maxCorner) {
        Vector3 corner1 = loc + Quaternion.AngleAxis(rot, Vector3.up) * cell.corner1;
        Vector3 corner2 = loc + Quaternion.AngleAxis(rot, Vector3.up) * cell.corner2;
        minCorner = new Vector3Int(
            (int)Mathf.Min(corner1.x, corner2.x),
            (int)Mathf.Min(corner1.y, corner2.y),
            (int)Mathf.Min(corner1.z, corner2.z));
        maxCorner = new Vector3Int(
            (int)Mathf.Max(corner1.x, corner2.x),
            (int)Mathf.Max(corner1.y, corner2.y),
            (int)Mathf.Max(corner1.z, corner2.z));
    }

    /* **************** DoorIsOnCell() ****************
     * Determines whether a Door is "on" a Cell--that is to say that the Door's
     * location Vector3 falls somewhere on a vertical surface of the Cell. Be-
     * cause the Door's adjusted location Vector3 is used if the Door is on the
     * Cell, this calculated value is an out parameter.
     * 
     * Parameters:
     *   door (Door): Door to evaluate
     *   doorRoomLoc (Vector3): Local position Vector3 of the Room that the
     *     Door is in
     *   doorRoomRot (float): Local y rotation of the Room that the Door is in
     *   cell (Cell): Cell to evaluate
     *   cellRoomLoc (Vector3): Local position Vector3 of the Room that the
     *     Cell is in
     *   cellRoomRot (float): Local y rotation of the Room that the Cell is in
     * 
     * Out Parameters:
     *   doorLoc (Vector3): Local position Vector3 of the Door relative to the
     *     it's Room's parent Transform
     * 
     * Returns:
     *   bool: true iff Door is on one of the vertical planes of the Cell
     */
    private static bool DoorIsOnCell(Door door, Vector3 doorRoomLoc, float doorRoomRot, Cell cell, Vector3 cellRoomLoc, float cellRoomRot, out Vector3 doorLoc) {
        doorLoc = doorRoomLoc + Quaternion.AngleAxis(doorRoomRot, Vector3.up) * door.location;
        Vector3Int minCorner, maxCorner;
        GetMinAndMaxCorners(cell, cellRoomLoc, cellRoomRot, out minCorner, out maxCorner);

        /*
         * Hopefully the spacing of this return statements help clarify what it does, but in case it doesn't:
         * First and foremost, the door must fall in the cell's y range, since all doors are perpendicular to the XZ plane
         * There are two cases for the next check:
         *      Case 1: The door is parallel to the YZ plane
         *          The door must then be on the x boundary of the cell (either the min or max),
         *          and the door must fall in the cell's z range
         *      Case 2: The door is parallel to the XY plane
         *          The door must then be on the z boundary of the cell (either the min or max)
         *          and the door must fall in the cell's x range
         */

        return
            minCorner.y < doorLoc.y &&
            doorLoc.y < maxCorner.y &&
            (   (   (   doorLoc.x == minCorner.x ||
                        doorLoc.x == maxCorner.x
                    ) &&
                    minCorner.z < doorLoc.z &&
                    doorLoc.z < maxCorner.z
                ) ||
                (   (   doorLoc.z == minCorner.z ||
                        doorLoc.z == maxCorner.z
                    ) &&
                    minCorner.x < doorLoc.x &&
                    doorLoc.x < maxCorner.x
                )
            );
    }
}
