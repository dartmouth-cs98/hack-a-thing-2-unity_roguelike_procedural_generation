using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoguelikePG;

[System.Serializable]
public struct Door {
    public Vector3 location;
    public Direction dir;

    public Door(Vector3 location, Direction dir) {
        this.location = location;
        this.dir = dir;
    }
}

public class Room : MonoBehaviour {
    public Vector3[] Cells;
    public Door[] Doors;

    void Start() {
        
    }

    void Update() {
        
    }
}
