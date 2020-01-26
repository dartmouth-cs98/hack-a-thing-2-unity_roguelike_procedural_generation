using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoguelikePG;

public class RoomManager : MonoBehaviour
{
    public GameObject[] prefabs;

    public GameObject roomAGO;
    public GameObject roomBGO;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 newRoomBLoc;
        float newRoomBRot;

        Door doorA = roomAGO.GetComponent<Room>().Doors[1];
        Transform roomATransform = roomAGO.transform;
        Door doorB = roomBGO.GetComponent<Room>().Doors[1];

        //Door.GetAdjacentRoomLocAndRot(roomATransform, doorA, doorB, out newRoomBLoc, out newRoomBRot);

        //roomBGO.transform.localPosition = newRoomBLoc;
        //roomBGO.transform.Rotate(Vector3.up, newRoomBRot);
        //Debug.Log("newRoomBLoc: " + newRoomBLoc + "\nnewRoomBRot: " + newRoomBRot);

        Room roomA = roomAGO.GetComponent<Room>(), roomB = roomBGO.GetComponent<Room>();
        Debug.Log(Room.RoomsOverlap(
            roomA, roomAGO.transform.localPosition, roomAGO.transform.localRotation.eulerAngles.y,
            roomB, roomBGO.transform.localPosition, roomBGO.transform.localRotation.eulerAngles.y));

        {
            /*
            roomCGO.transform.localPosition = new Vector3(1, 1, 1);
            Debug.Log("c at " + roomCGO.transform.localPosition + ": " + Room.RoomsOverlap(
                roomA, roomAGO.transform.localPosition, roomAGO.transform.localRotation.eulerAngles.y,
                roomC, roomCGO.transform.localPosition, roomCGO.transform.localRotation.eulerAngles.y));
            roomCGO.transform.localPosition = new Vector3(1, 0, 1);
            Debug.Log("c at " + roomCGO.transform.localPosition + ": " + Room.RoomsOverlap(
                roomA, roomAGO.transform.localPosition, roomAGO.transform.localRotation.eulerAngles.y,
                roomC, roomCGO.transform.localPosition, roomCGO.transform.localRotation.eulerAngles.y));
            roomCGO.transform.localPosition = new Vector3(0, 0, 0);
            Debug.Log("c at " + roomCGO.transform.localPosition + ": " + Room.RoomsOverlap(
                roomA, roomAGO.transform.localPosition, roomAGO.transform.localRotation.eulerAngles.y,
                roomC, roomCGO.transform.localPosition, roomCGO.transform.localRotation.eulerAngles.y));
            roomCGO.transform.localPosition = new Vector3(2, 0, 0);
            Debug.Log("c at " + roomCGO.transform.localPosition + ": " + Room.RoomsOverlap(
                roomA, roomAGO.transform.localPosition, roomAGO.transform.localRotation.eulerAngles.y,
                roomC, roomCGO.transform.localPosition, roomCGO.transform.localRotation.eulerAngles.y));
            roomCGO.transform.localPosition = new Vector3(2, 0, 0);
            Debug.Log("c at " + roomCGO.transform.localPosition + ": " + Room.RoomsOverlap(
                roomA, roomAGO.transform.localPosition, roomAGO.transform.localRotation.eulerAngles.y,
                roomC, roomCGO.transform.localPosition, roomCGO.transform.localRotation.eulerAngles.y));
            roomCGO.transform.localPosition = new Vector3(2, 0, 2);
            Debug.Log("c at " + roomCGO.transform.localPosition + ": " + Room.RoomsOverlap(
                roomA, roomAGO.transform.localPosition, roomAGO.transform.localRotation.eulerAngles.y,
                roomC, roomCGO.transform.localPosition, roomCGO.transform.localRotation.eulerAngles.y));
            roomCGO.transform.localPosition = new Vector3(4, 0, 0);
            Debug.Log("c at " + roomCGO.transform.localPosition + ": " + Room.RoomsOverlap(
                roomA, roomAGO.transform.localPosition, roomAGO.transform.localRotation.eulerAngles.y,
                roomC, roomCGO.transform.localPosition, roomCGO.transform.localRotation.eulerAngles.y));
            roomCGO.transform.localPosition = new Vector3(2, 0, -2);
            Debug.Log("c at " + roomCGO.transform.localPosition + ": " + Room.RoomsOverlap(
                roomA, roomAGO.transform.localPosition, roomAGO.transform.localRotation.eulerAngles.y,
                roomC, roomCGO.transform.localPosition, roomCGO.transform.localRotation.eulerAngles.y));
            */
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
