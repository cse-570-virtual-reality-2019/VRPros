using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCreator : MonoBehaviour
{
    public static GameObject Plane(float lenght, float width)
    {
        GameObject newPlane = new GameObject("Plane");
        MeshFilter mFilter = newPlane.AddComponent(typeof(MeshFilter)) as MeshFilter;
        MeshCollider mCollider = newPlane.AddComponent(typeof(MeshCollider)) as MeshCollider;
        MeshRenderer mRenderer = newPlane.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        Mesh newMesh = new Mesh();

        newMesh.vertices = new Vector3[]
        {
            new Vector3(0,0,0),new Vector3(lenght, 0, 0),new Vector3(lenght,width,0),new Vector3(0,width,0)
        };

        newMesh.uv = new Vector2[]
        {
            new Vector2(0,0),new Vector2(0,1),new Vector2(1,1), new Vector2(1,0)
        };

        newMesh.triangles = new int[] { 0, 1, 2, 0, 2, 3 };
        mFilter.mesh = newMesh;
        newMesh.RecalculateBounds();
        newMesh.RecalculateNormals();

        return newPlane;
    }
}
