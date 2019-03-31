/* Program to get the  boundary of the selected map area 
 * */

using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;
using UnityEngine;

public class MapBoundary 
{
    public float MinLongitude;
    public float MaxLongitude;
    public float MinLatitude;
    public float MaxLatitude;
    public float lenght;
    public float width;
    public Vector3 Centre;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public MapBoundary (XmlNode node)
    {
        MinLongitude = GetAttribute<float>(node.Attributes, "minlon");
        MaxLongitude = GetAttribute<float>(node.Attributes, "maxlon");
        MinLatitude = GetAttribute<float>(node.Attributes, "minlat");
        MaxLatitude = GetAttribute<float>(node.Attributes, "maxlat");

        float xCoord1 =(float)MapMercator.lonToX(MaxLongitude);
        float xCoord2 =(float)MapMercator.lonToX(MinLongitude);
        float yCoord1 =(float)MapMercator.latToY(MaxLatitude);
        float yCoord2 = (float)MapMercator.latToY(MinLatitude);
        float xCoord = (( xCoord1+xCoord2)/ 2);
        float yCoord = (( yCoord1+yCoord2)/ 2);

        lenght = (float)(MapMercator.lonToX(MaxLongitude) + MapMercator.lonToX(MinLongitude));
        width = (float)(MapMercator.latToY(MaxLatitude) + MapMercator.latToY(MinLatitude));
        Centre = new Vector3(xCoord, 0, yCoord);
    }

    T GetAttribute<T>(XmlAttributeCollection attributes, string attr)
    {
        string strValue = attributes[attr].Value;         //getting value of the paarticular attribute
        return (T)Convert.ChangeType(strValue, typeof(T));  //converting to a required datatype.
    }
    public void createPlane(XmlNode node)
    {
        MinLongitude = GetAttribute<float>(node.Attributes, "minlon");
        MaxLongitude = GetAttribute<float>(node.Attributes, "maxlon");
        MinLatitude = GetAttribute<float>(node.Attributes, "minlat");
        MaxLatitude = GetAttribute<float>(node.Attributes, "maxlat");
    }
}
