using System.Collections.Generic;
using UnityEngine;
using RoguelikePG;

public class RoomManager : MonoBehaviour {
    public GameObject[] prefabs;

    /*
     * These Lists keep track of which rooms are "active"--those which have
     * already been added to the structure. These rooms aren't necessarily in
     * the scene, but all the data necessary to instantiate them is saved,
     * hence the three lists. These were originally stored as one List of
     * Tuples, but Unity's lack of Tuple support made that difficult. The
     * PrefabIndices List keeps track of the index of prefabs used for a room.
     * RoomPoses keeps track of the Vector3 local positions of the rooms.
     * RoomRots keeps track of the y rotation of the rooms.
     */
    private List<int> _activeRoomPrefabIndices;
    private List<Vector3> _activeRoomPoses;
    private List<float> _activeRoomRots;

    // Start is called before the first frame update
    void Start() {
        _activeRoomPrefabIndices = new List<int>();
        _activeRoomPoses = new List<Vector3>();
        _activeRoomRots = new List<float>();

        _activeRoomPrefabIndices.Add(0);
        _activeRoomPoses.Add(Vector3.zero);
        _activeRoomRots.Add(0);

        /*
         * openDoors is a Queue of ints, where each int represents the index to
         * an "open" door (one that isn't coupled with the door of another
         * room) in an indexable room of the _activeRoom Lists. The two indices
         * are stored in the following manner, given the 32 bit signed Queue
         * members: 0x rr rr rr rd. The first 7 nibbles (excluding the leading
         * sign bit) are for the index of the room in the _activeRoom Lists.
         * The last nibble is for the Door's index within the indexed Room.
         */
        Queue<int> openDoors = new Queue<int>();

        for (int d = 0; d < prefabs[0].GetComponent<Room>().Doors.Length; d++) {
            openDoors.Enqueue(d);
        }

        int randRoom;
        int randDoor;
        int startRoom;
        int startDoor;
        bool matchFound;

        while (_activeRoomPrefabIndices.Count < 20 && openDoors.Count > 0) {
            int currDoorData = openDoors.Dequeue();
            int activeRoomsIdx = currDoorData >> 4, currDoorIdx = currDoorData & 0xF;
            int currRoomPrefabIdx = _activeRoomPrefabIndices[activeRoomsIdx];
            //Room currRoom = prefabs[currRoomPrefabIdx].GetComponent<Room>();
            Vector3 currRoomLoc = _activeRoomPoses[activeRoomsIdx];
            float currRoomRot = _activeRoomRots[activeRoomsIdx];
            Door currDoor = prefabs[currRoomPrefabIdx].GetComponent<Room>().Doors[currDoorIdx];


            // pick a random room prefab and remember this is the index you started on
            randRoom = Random.Range(0, prefabs.Length);
            startRoom = randRoom;

            do {
                Room nextRoom = prefabs[randRoom].GetComponent<Room>();
                Vector3 nextRoomLoc;
                float nextRoomRot;

                // pick a random door and remember this is the index you started on
                randDoor = Random.Range(0, nextRoom.Doors.Length);
                startDoor = randDoor;

                do {
                    matchFound = true;
                    Door nextDoor = nextRoom.Doors[randDoor];
                    Door.GetAdjacentRoomLocAndRot(currDoor, currRoomLoc, currRoomRot,
                        nextDoor, out nextRoomLoc, out nextRoomRot);

                    for (int ar = 0; ar < _activeRoomPrefabIndices.Count; ar++) {
                        int activeRoomPrefabIdx = _activeRoomPrefabIndices[ar];
                        Vector3 activeRoomLoc = _activeRoomPoses[ar];
                        float activeRoomRot = _activeRoomRots[ar];
                        Room activeRoom = prefabs[activeRoomPrefabIdx].GetComponent<Room>();

                        if (Room.RoomsOverlap(activeRoom, activeRoomLoc, activeRoomRot, nextRoom, nextRoomLoc, nextRoomRot)) {
                            matchFound = false;
                            break;
                        }
                    }

                    if (matchFound) {
                        _activeRoomPrefabIndices.Add(randRoom);
                        _activeRoomPoses.Add(nextRoomLoc);
                        _activeRoomRots.Add(nextRoomRot);

                        // add open doors to open doors
                        for (int d = 0; d < nextRoom.Doors.Length; d++) {
                            if (d != randDoor) {
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

        for(int ar = 0; ar < _activeRoomPrefabIndices.Count; ar++) {
            int activeRoomPrefabIdx = _activeRoomPrefabIndices[ar];
            Vector3 activeRoomLoc = _activeRoomPoses[ar];
            float activeRoomRot = _activeRoomRots[ar];
            GameObject activeRoomGO = Object.Instantiate(prefabs[activeRoomPrefabIdx]);
            activeRoomGO.transform.localPosition = activeRoomLoc;
            activeRoomGO.transform.Rotate(Vector3.up, activeRoomRot);
        }
    }

    // Update is called once per frame void Update() { }
}
