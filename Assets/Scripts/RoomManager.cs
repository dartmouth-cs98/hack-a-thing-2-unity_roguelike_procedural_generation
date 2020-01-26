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
        Debug.Log("East to West = " + RoguelikePGUtility.AngleTo(Direction.East, Direction.West));
        Debug.Log("North to West = " + RoguelikePGUtility.AngleTo(Direction.North, Direction.West));
        Debug.Log("North to East = " + RoguelikePGUtility.AngleTo(Direction.North, Direction.East));
        Debug.Log("South to North = " + RoguelikePGUtility.AngleTo(Direction.South, Direction.North));

        Vector3 newRoomBLoc;
        float newRoomBRot;

        Door doorA = roomAGO.GetComponent<Room>().Doors[1];
        Transform roomATransform = roomAGO.transform;
        Door doorB = roomBGO.GetComponent<Room>().Doors[1];

        Door.GetAdjacentRoomLocAndRot(roomATransform, doorA, doorB, out newRoomBLoc, out newRoomBRot);

        roomBGO.transform.localPosition = newRoomBLoc;
        roomBGO.transform.Rotate(Vector3.up, newRoomBRot);
        Debug.Log("newRoomBLoc: " + newRoomBLoc + "\nnewRoomBRot: " + newRoomBRot);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
