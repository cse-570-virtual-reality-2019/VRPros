using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class MapInfra : MonoBehaviour
{
    public MapDataReader map;
   
    protected Vector3 GetCentre(MapPaths way)       //Getting the centre of a infrastructure
    {
        Vector3 total = Vector3.zero;
        for (int i = 0; i < way.NodeID.Count; i++)
        {
            var index = way.NodeID[i];
            total += map.nodes[index];
        }
        total = total / way.NodeID.Count;
        return total;
    }
    void Awake()
    {
        map = GetComponent<MapDataReader>();
    }
    protected void CreateObject(MapPaths way, Material mat, string objectName)
    {
        if (string.IsNullOrEmpty(objectName))
        {
            objectName = "OsmWay";
        }
        GameObject gameObject = new GameObject(objectName);
        MeshFilter mfilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer mrenderer = gameObject.AddComponent<MeshRenderer>();
        mrenderer.material = mat;

        Vector3 localOrigin = GetCentre(way);
        gameObject.transform.position = localOrigin - map.bounds.Centre;
        if (objectName == "Road") {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.1f, gameObject.transform.position.z);
        }
        
        List<Vector3> normals, vectors;
        List<Vector2> xys = new List<Vector2>();
        List<int> indices = new List<int>();

        vectors = new List<Vector3>();
        normals = new List<Vector3>();

        OnObjectCreated(normals, xys, indices, way, localOrigin, vectors);
        createMeshFilter(normals,vectors,xys,indices,mfilter);
    }
    protected abstract void OnObjectCreated(List<Vector3> normals, List<Vector2> xys, List<int> indices,MapPaths way, Vector3 origin, List<Vector3> 
        vectors);

    void createMeshFilter(List<Vector3> normals, List<Vector3> vectors, List<Vector2> xys,List<int> indices, MeshFilter mfilter)
    {
        Vector3[] var1, var2;
        var1= vectors.ToArray();
        var2= normals.ToArray();
        mfilter.mesh.vertices = var1;
        mfilter.mesh.normals = var2;


        mfilter.mesh.triangles = indices.ToArray();
        mfilter.mesh.uv = xys.ToArray();
    }
    
}
