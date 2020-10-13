using UnityEngine;
using System;

    [Serializable]
    public class MeshVertex
    {
        public Vector3 myPosition;
        public Vector3 myNormal;
        public Vector2 myUv;

        public MeshVertex(Vector3 aPos, Vector3 aNormal, Vector2 aUv)
        {
            this.myPosition = aPos;
            this.myNormal = aNormal;
            this.myUv = aUv;
        }

        public MeshVertex(Vector3 aPos, Vector3 aNormal)
            : this(aPos, aNormal, Vector2.zero)
        {
        }
    }
