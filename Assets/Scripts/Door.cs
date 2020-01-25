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
    }
}
