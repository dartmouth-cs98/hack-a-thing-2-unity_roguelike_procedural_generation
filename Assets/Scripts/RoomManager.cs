using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoguelikePG;

public class RoomManager : MonoBehaviour
{
    public GameObject[] prefabs;

    public GameObject roomAGO;
    public GameObject roomBGO;

    /* List of the active rooms, where each active room is described by an int, vector3 and float
       The int is the index of the room prefab, the vector3 is the local position of the room
       and the float is the y rotation of the room
     */ 
    private List<int> _activeRoomPrefabIndices;
    private List<Vector3> _activeRoomPoses;
    private List<float> _activeRoomRots;

    //private List<GameObject> _activeRooms;

    // Start is called before the first frame update
    void Start()
    {
        //Vector3 newRoomBLoc;
        //float newRoomBRot;

        //_activeRooms = new List<Tuple<int, Vector3, float>>();
        _activeRoomPrefabIndices = new List<int>();
        _activeRoomPoses = new List<Vector3>();
        _activeRoomRots = new List<float>();

        //_activeRooms.Add((0, Vector3.zero, 0));
        _activeRoomPrefabIndices.Add(0);
        _activeRoomPoses.Add(Vector3.zero);
        _activeRoomRots.Add(0);

        Queue<int> openDoors = new Queue<int>();

        for (int d = 0; d < prefabs[0].GetComponent<Room>().Doors.Length; d++)
        {
            openDoors.Enqueue(d);
        }

        int count = 0;
        int randRoom;
        int randDoor;
        int startRoom;
        int startDoor;
        bool matchFound;

        while (_activeRoomPrefabIndices.Count < 20 && openDoors.Count > 0)
        {
            int nextDoorData = openDoors.Dequeue();
            int activeRoomsIdx = nextDoorData >> 4, doorIdx = nextDoorData & 0xF;
            //Tuple<int, Vector3, float> currRoomTuple = _activeRooms[activeRoomsIdx];
            //int currRoomPrefab = currRoomTuple[0];
            //Vector3 currRoomLoc = currRoomTuple[1];
            //float currRoomRot = currRoomTuple[2];
            int currRoomPrefab = _activeRoomPrefabIndices[activeRoomsIdx];
            Vector3 currRoomLoc = _activeRoomPoses[activeRoomsIdx];
            float currRoomRot = _activeRoomRots[activeRoomsIdx];
            Room currRoom = prefabs[currRoomPrefab].GetComponent<Room>();
            Door currDoor = currRoom.Doors[doorIdx];


            // pick a random room prefab and remember this is the index you started on
            randRoom = Random.Range(0, prefabs.Length);
            startRoom = randRoom;

            do
            {
                Room nextRoom = prefabs[randRoom].GetComponent<Room>();
                Vector3 nextRoomLoc;
                float nextRoomRot;

                // pick a random door and remember this is the index you started on
                randDoor = Random.Range(0, nextRoom.Doors.Length);
                startDoor = randDoor;
                do
                {
                    matchFound = true;
                    Door nextDoor = nextRoom.Doors[randDoor];

                    Door.GetAdjacentRoomLocAndRot(currDoor, currRoomLoc, currRoomRot, nextDoor, out nextRoomLoc, out nextRoomRot);

                    for (int ar = 0; ar < _activeRoomPrefabIndices.Count; ar++)
                    {
                        //int activeRoomPrefab = currRoomTuple[0];
                        //Vector3 activeRoomLoc = currRoomTuple[1];
                        //float activeRoomRot = currRoomTuple[2];
                        int activeRoomPrefab = _activeRoomPrefabIndices[ar];
                        Vector3 activeRoomLoc = _activeRoomPoses[ar];
                        float activeRoomRot = _activeRoomRots[ar];
                        Room activeRoom = prefabs[activeRoomPrefab].GetComponent<Room>();

                        Debug.Log("activeRoom index: " + ar + ", randRoom: " + randRoom);

                        if (Room.RoomsOverlap(activeRoom, activeRoomLoc, activeRoomRot, nextRoom, nextRoomLoc, nextRoomRot))
                        {
                            matchFound = false;
                            break;
                        }
                    }

                    if (matchFound)
                    {
                        // eventually break
                        // add to active rooms
                        //_activeRooms.Add((randRoom, nextRoomLoc, nextRoomRot));
                        _activeRoomPrefabIndices.Add(randRoom);
                        _activeRoomPoses.Add(nextRoomLoc);
                        _activeRoomRots.Add(nextRoomRot);
                        // add open doors to open doors
                        for (int d = 0; d < nextRoom.Doors.Length; d++)
                        {
                            if (d != randDoor)
                            {
                                openDoors.Enqueue(((_activeRoomPrefabIndices.Count - 1) << 4) | d);
                            }
                        }
                        break;
                    }

                    randDoor++;
                    randDoor %= nextRoom.Doors.Length;

                } while (randDoor != startDoor && !matchFound);

                randRoom++;
                randRoom %= prefabs.Length;
            } while (randRoom != startRoom && !matchFound);
        }

        for(int ar = 0; ar < _activeRoomPrefabIndices.Count; ar++)
        {
            //Tuple<int, Vector3, float> activeRoomTuple = _activeRooms[activeRoomsIdx];
            //int activeRoomPrefab = activeRoomTuple[0];
            //Vector3 activeRoomLoc = activeRoomTuple[1];
            //float activeRoomRot = activeRoomTuple[2];

            int activeRoomPrefab = _activeRoomPrefabIndices[ar];
            Vector3 activeRoomLoc = _activeRoomPoses[ar];
            float activeRoomRot = _activeRoomRots[ar];
            GameObject activeRoomGO = Object.Instantiate(prefabs[activeRoomPrefab]);
            activeRoomGO.transform.localPosition = activeRoomLoc;
            activeRoomGO.transform.Rotate(Vector3.up, activeRoomRot);


        }
        //Door doorA = roomAGO.GetComponent<Room>().Doors[1];
        //Transform roomATransform = roomAGO.transform;
        //Door doorB = roomBGO.GetComponent<Room>().Doors[1];

        ////Door.GetAdjacentRoomLocAndRot(roomATransform, doorA, doorB, out newRoomBLoc, out newRoomBRot);

        ////roomBGO.transform.localPosition = newRoomBLoc;
        ////roomBGO.transform.Rotate(Vector3.up, newRoomBRot);
        ////Debug.Log("newRoomBLoc: " + newRoomBLoc + "\nnewRoomBRot: " + newRoomBRot);

        //Room roomA = roomAGO.GetComponent<Room>(), roomB = roomBGO.GetComponent<Room>();
        //Debug.Log(Room.RoomsOverlap(
        //roomA, roomAGO.transform.localPosition, roomAGO.transform.localRotation.eulerAngles.y,
        //roomB, roomBGO.transform.localPosition, roomBGO.transform.localRotation.eulerAngles.y));

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
