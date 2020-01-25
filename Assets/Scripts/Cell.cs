using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoguelikePG {
    [System.Serializable]
    public class Cell {
        public Vector3 corner1;
        public Vector3 corner2;

        public Cell(Vector3 corner1, Vector3 corner2) {
            this.corner1 = corner1;
            this.corner2 = corner2;
        }
    }
}
