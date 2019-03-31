using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;

public class MapPaths
{
    public ulong ID;
    public bool Visible;
    public float Height;
    public string Name;
    public int Lanes;
    public bool roadExist;
    public bool boundaryExist;
    public bool buildingExist;
    public bool parkExist;
    public bool waterBodyExist;
    public bool amenityExist;
    public List<ulong> NodeID;

    public MapPaths(XmlNode node)
    {
        Lanes = 1;      
        Name = "";
        NodeID = new List<ulong>();
        Height = UnityEngine.Random.Range(15.0f, 20.0f); //Adding the default height
        ID = GetAttribute<ulong>(node.Attributes,"id");
        Visible = GetAttribute<bool>(node.Attributes,"visible");

        fetchNodeIDs(node);
        boundaryExist = CheckBoundary();
        
        XmlNodeList tags = node.SelectNodes("tag");
        checkTags(tags);
    }

    T GetAttribute<T>(XmlAttributeCollection attributes, string attr)
    {
        string strValue = attributes[attr].Value;         //getting value of the paarticular attribute
        return (T)Convert.ChangeType(strValue, typeof(T));  //converting to a required datatype.
    }

    XmlNodeList fetchNodeIDs(XmlNode node)  // Fetching node ids of the way tag
    {
        XmlNodeList noderef = node.SelectNodes("nd");
        foreach (XmlNode n in noderef)
        {
            ulong refNo = GetAttribute<ulong>(n.Attributes, "ref");
            NodeID.Add(refNo);
        }
        return noderef;
    }

    bool CheckBoundary()            // function to check whether the detected node is a closed figure
    {
        if (NodeID.Count > 1)
        {
            if (NodeID[0] == NodeID[NodeID.Count - 1])
                return true;
            else
                return false;
        }
        return false;
    }

    void checkTags(XmlNodeList tags)
    {
        for(int i=0;i<tags.Count;i++)
        {
            XmlNode t = tags[i];
            string key = GetAttribute<string>(t.Attributes, "k");
            
            if (key == "height")           //If height is given in terms of heights not levels
            {
                Height = 0.3048f * GetAttribute<float>(t.Attributes, "v");
            }
            else if (key == "building:levels")       // IF height of the building is given
            {
                Height = 3.0f * GetAttribute<float>(t.Attributes, "v");
            }
            else if (key == "lanes")
            {
                Lanes = GetAttribute<int>(t.Attributes, "v");
            }
            else if (key == "building")
            {
                buildingExist = true; //Detecting if Building
            }
            else if (key == "highway")
            {
                roadExist = true&&true;       //Detecting if highway
            }
            else if (key == "leisure")
            {
                parkExist = true&&true;       //Detecting if parks
            }
            else if (key == "amenity")
            {
                amenityExist = true&&true;    //detecting if amenities.
            }
            else if (key == "waterway")
            {
                waterBodyExist = true && true; //detecting if water way
            }
        }
    }
}
