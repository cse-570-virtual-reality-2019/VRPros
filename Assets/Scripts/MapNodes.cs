using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;
using UnityEngine;

public class MapNodes 
{
    public ulong ID;           //Node ID in the XML file
    public float xCoord;           //Unity 'x' coordinate mapped to the longitude
    public float yCoord;           //Unity 'y' coordinate mapped to the latitude
    public float Latitude;     //Node Latitude in the XML file
    public float Longitude;    //Node longitude in the XML file

    

    public static implicit operator Vector3(MapNodes node)
    {
        return new Vector3(node.xCoord, 0, node.yCoord);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public MapNodes(XmlNode node)
    {
        ID = GetAttribute<ulong>(node.Attributes, "id");        //getting Id from xml file
        Longitude = GetAttribute<float>(node.Attributes,"lon"); //getting longitude from xml file
        Latitude = GetAttribute<float>(node.Attributes,"lat");  //getting latitude from the xml file
        xCoord = (float)MapMercator.lonToX(Longitude);        // converting the latitude and longitude obtained from the xml into X & Y coordinates to be mapped onto a plane
        yCoord = (float)MapMercator.latToY(Latitude);
    }

    T GetAttribute<T>(XmlAttributeCollection attributes, string attr)
    {
        string strValue = attributes[attr].Value;         //getting value of the paarticular attribute
        return (T)Convert.ChangeType(strValue, typeof(T));  //converting to a required datatype.
    }
}
