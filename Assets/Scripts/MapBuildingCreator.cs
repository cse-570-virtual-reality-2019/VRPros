using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MapBuildingCreator : MapInfra
{
    public Material building;

    IEnumerator Start()
    {
        while (map.IsReady==false)  yield return null;
        for(int i=0;i<map.ways.Count;i++)// Iterate through all the buildings in the 'ways' list
        {
            MapPaths way = map.ways[i];
            if (way.buildingExist && way.NodeID.Count > 1)
            {
                CreateObject(way, building, "Building");        // Creating the object.
                yield return null;
            }
        }
    }

    protected override void OnObjectCreated(List<Vector3> normals, List<Vector2> xys, List<int> indices,MapPaths way, Vector3 origin, 
        List<Vector3> vectors)
    {

        Vector3 TopCentre = new Vector3(0, way.Height, 0);
        int Cnt = way.NodeID.Count;
        xys.Add(new Vector2(0.5f, 0.5f));
        vectors.Add(TopCentre);
        normals.Add(Vector3.up);
        
        for (int i = 1; i < Cnt; i++)
        {
            int idx1, idx2, idx3, idx4;
            MapNodes point2 = map.nodes[way.NodeID[i]];
            MapNodes point1 = map.nodes[way.NodeID[i - 1]];
           
            creatingVectors(vectors,origin,point1,point2,way);
            addToVector2(xys, new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1));

            addNormals(normals);

            idx1 = AssignValues(vectors.Count, 4);
            idx2 = AssignValues(vectors.Count, 3);
            idx3 = AssignValues(vectors.Count, 2);
            idx4 = AssignValues(vectors.Count, 1);
            addingTriangles(indices, idx1, idx2, idx3, idx4);            
        }
    }
 
    void creatingVectors(List<Vector3> vectors, Vector3 origin,MapNodes point1, MapNodes point2,MapPaths way)
    {
        Vector3 vec1 = point1 - origin;
        Vector3 vec2 = point2 - origin;   
        addToVector3(vectors,vec1,vec2, vec1 + new Vector3(0, way.Height, 0), vec2 + new Vector3(0, way.Height, 0));
    }
    void addNormals(List<Vector3> normals)
    {
        addToVector3(normals, -Vector3.forward, -Vector3.forward, -Vector3.forward, -Vector3.forward);
    }

    void addingTriangles(List<int> indices,int idx1, int idx2,int idx3, int idx4)
    {
        
        makeTriangle(indices, idx1, idx3, idx2); //1st Triangle created
        makeTriangle(indices, idx2, idx3, idx1); //2nd Triangle created
        makeTriangle(indices, idx3, idx4, idx2); //3rd Traingle created
        makeTriangle(indices, idx2, idx4, idx3); //4th Triangle created
        makeTriangle(indices, idx4, idx3, 0); 
        makeTriangle(indices, 0, idx3, idx4);
    }

    void makeTriangle(List<int> list, int var1, int var2, int var3)
    {
        list.Add(var1);
        list.Add(var2);
        list.Add(var3);
    }

    void addToVector2(List<Vector2> list,Vector2 var1, Vector2 var2, Vector2 var3, Vector2 var4)
    {
        list.Add(var1);
        list.Add(var2);
        list.Add(var3);
        list.Add(var4);
    }
    void addToVector3(List<Vector3> list, Vector3 var1, Vector3 var2, Vector3 var3, Vector3 var4)
    {
        list.Add(var1);
        list.Add(var2);
        list.Add(var3);
        list.Add(var4);
    }
    int AssignValues(int val1,int val2)
    {
        return val1 - val2;
    }
}
  
