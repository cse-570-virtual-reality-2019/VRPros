/* Program Which is used to read the map data from the open Street maps xml file 
 * 
 * */
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using System.IO;

class MapDataReader : MonoBehaviour
{
    public GameObject groundPlane;

    public bool IsReady;
    public string resourceFile;             //Variable to hold the xml file
    public MapBoundary bounds;
    public Dictionary<ulong, MapNodes> nodes;
    public List<MapPaths> ways;
    MapBoundary mB;
    void Start()
    {
        //MapBoundary mB = new MapBoundary();
        //groundPlane =PlaneCreator.Plane( mB.lenght,mB.width);
        //resourceFile = MapLocationHolder.FILENAME;
        nodes = new Dictionary<ulong, MapNodes>();
        ways = new List<MapPaths>();
        readXMLFile();
        IsReady = true;
        
    }

    void Update()
    {
        Color c = Color.red ;
        foreach (MapPaths w in ways){
            if (w.Visible){
                c=checkVariousTypes(w, c);
                for(int i = 1; i < w.NodeID.Count; i++){
                    Vector3 val1, val2;
                    MapNodes point2 = nodes[w.NodeID[i]];
                    MapNodes point1 = nodes[w.NodeID[i - 1]];
                    val1 = point1 - bounds.Centre;
                    val2 = point2 - bounds.Centre;
                    Debug.DrawLine(val1, val2, c);
                }
            }
        }
    }

    void readXMLFile()                          //Reading the xml file
    {
        var txtAsset = Resources.Load<TextAsset>(resourceFile);
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(txtAsset.text);
        getVariousAttributes(doc);
        convertLanLongToXY();
    }

    void SetBounds(XmlNode xmlNode)
    {
        bounds = new MapBoundary(xmlNode);
    }

    void GetNodes(XmlNodeList xmlNodeList)      //Getting a node list
    {
        for (int i = 0; i < xmlNodeList.Count; i++)
        {
            XmlNode newnode = xmlNodeList[i];
            MapNodes node = new MapNodes(newnode);
            nodes[node.ID] = node;
        }
    }
    void GetWays(XmlNodeList xmlNodeList)       //Getting a path list
    {
        for (int i = 0; i < xmlNodeList.Count; i++)
        {
            XmlNode node = xmlNodeList[i];
            MapPaths way = new MapPaths(node);
            ways.Add(way);
        }
    }
    void convertLanLongToXY()                   //Convert lantitude & longitude to x & y coord
    {
        float minxCoord, minyCoord, maxxCoord, maxyCoord,x,y,z;
        minxCoord = (float)MapMercator.lonToX(bounds.MinLongitude);
        minyCoord = (float)MapMercator.latToY(bounds.MinLatitude);
        maxxCoord = (float)MapMercator.lonToX(bounds.MaxLongitude);
        maxyCoord = (float)MapMercator.latToY(bounds.MaxLatitude);
        x = (maxxCoord-minxCoord)/2;
        z = (maxyCoord-minyCoord)/2;
        y = 1;
        groundPlane.transform.localScale = new Vector3(x,y,z);
    }
    Color checkVariousTypes(MapPaths w, Color c)     //Matching for a particular type of boundary or path
    {
        if (w.buildingExist)
            c = Color.red;                 //coloring the buildings
        if (w.parkExist)
            c = Color.green;               //coloring for the park
        if (w.amenityExist)
            c = Color.yellow;              //coloring for the amenities
        if (!w.boundaryExist)
            c = Color.black;                // coloring for  the roads
        if (w.waterBodyExist)
            c = Color.blue;                 //Color for the waterbody
        return c;
    }
    void getVariousAttributes(XmlDocument document) //getting to a particular tag in the xml document i.e. Bound tag
    {
        SetBounds(document.SelectSingleNode("/osm/bounds"));         
        GetNodes(document.SelectNodes("/osm/node"));
        GetWays(document.SelectNodes("/osm/way"));
    }
}
