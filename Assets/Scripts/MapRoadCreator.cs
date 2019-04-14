using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 class MapRoadCreator : MapInfra
{
    public Material roadmat;
    protected override void OnObjectCreated(List<Vector3> normals, List<Vector2> xys, List<int> indices, MapPaths way, Vector3 origin, List<Vector3> vectors)
    {
        MapNodes point1, point2;
        Vector3 spoint1, spoint2;
        int length = way.NodeID.Count;
        for (int i = 1; i < length; i++) {
            point1 = map.nodes[way.NodeID[i - 1]];
            point2 = map.nodes[way.NodeID[i]];

            spoint1 = point1 - origin;
            spoint2 = point2 - origin;

            Vector3 normalisedDifference = (spoint2 - spoint1).normalized;

            var crossing = Vector3.Cross(normalisedDifference, Vector3.up) * 3.5f * way.Lanes;

            Vector3 width1 = spoint1 + crossing;
            Vector3 width2 = spoint1 - crossing;

            Vector3 width3 = spoint2 + crossing;
            Vector3 width4 = spoint2 - crossing;

            xys.Add(new Vector2(0, 0));
            xys.Add(new Vector2(1, 0));
            xys.Add(new Vector2(0, 1));
            xys.Add(new Vector2(1, 1));

            vectors.Add(width1);
            vectors.Add(width2);
            vectors.Add(width3);
            vectors.Add(width4);

            int p1, p2, p3, p4;

            p1 = vectors.Count - 4;
            p2 = vectors.Count - 3;
            p3 = vectors.Count - 2;
            p4 = vectors.Count - 1;


            indices.Add(p1);
            indices.Add(p3);
            indices.Add(p2);

            indices.Add(p3);
            indices.Add(p4);
            indices.Add(p2);

            normals.Add(Vector3.up);
            normals.Add(Vector3.up);
            normals.Add(Vector3.up);
            normals.Add(Vector3.up);
        }
    }

    IEnumerator Start() {
        while (map.IsReady == false) {
            yield return null;
        }
        foreach (var way in map.ways.FindAll((w) => { return w.roadExist; }))
        {
            CreateObject(way, roadmat, "Road");
            yield return null;
        }
    }

}
