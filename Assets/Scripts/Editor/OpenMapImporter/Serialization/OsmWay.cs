using System.Collections.Generic;
using System.Xml;

/*
    Copyright (c) 2017 Sloan Kelly

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
*/

/// <summary>
/// An OSM object that describes an arrangement of OsmNodes into a shape or road.
/// </summary>
class OsmWay : BaseOsm
{
    /// <summary>
    /// Way ID.
    /// </summary>
    public ulong ID { get; private set; }

    /// <summary>
    /// True if visible.
    /// </summary>
    public bool Visible { get; private set; }

    /// <summary>
    /// List of node IDs.
    /// </summary>
    public List<ulong> NodeIDs { get; private set; }

    /// <summary>
    /// True if the way is a boundary.
    /// </summary>
    public bool IsBoundary { get; private set; }

    /// <summary>
    /// True if the way is a building.
    /// </summary>
    public bool IsBuilding { get; private set; }

    /// <summary>
    /// True if the way is a road.
    /// </summary>
    public bool IsRoad { get; private set; }

    /// <summary>
    /// Height of the structure.
    /// </summary>
    public float Height { get; private set; }

    /// <summary>
    /// The name of the object.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// The number of lanes on the road. Default is 1 for contra-flow
    /// </summary>
    public int Lanes { get; private set; }

   /// <summary>
    /// True if the way is a grass.
    /// </summary>
    public bool IsGrass { get; private set; }

       /// <summary>
    /// True if the way is a grass.
    /// </summary>
    public bool IsWater { get; private set; }

           /// <summary>
    /// True if the way is a grass.
    /// </summary>
    public bool IsSand { get; private set; }


    public bool IsFlat { get; private set; }
    public bool IsAmenity { get; private set; }
    public bool Walkable { get; private set; }
    //public Material _material;

    //public Material _material {get;}
    /// Constructor.
    /// </summary>
    /// <param name="node"></param>
    public OsmWay(XmlNode node)
    {
        

        NodeIDs = new List<ulong>();
        Height = 3.0f; // Default height for structures is 1 story (approx. 3m)
        Lanes = 1;      // Number of lanes either side of the divide 
        Name = "";
        Walkable = true;

        // Get the data from the attributes
        ID = GetAttribute<ulong>("id", node.Attributes);
        //Visible = GetAttribute<bool>("visible", node.Attributes);

        // Get the nodes
        XmlNodeList nds = node.SelectNodes("nd");
        foreach(XmlNode n in nds)
        {
            ulong refNo = GetAttribute<ulong>("ref", n.Attributes);
            NodeIDs.Add(refNo);
        }

        //if it is a boundary
        if (NodeIDs.Count > 1)
        {
            IsBoundary = NodeIDs[0] == NodeIDs[NodeIDs.Count - 1];
        }

        // Read the tags
        XmlNodeList tags = node.SelectNodes("tag");
        foreach (XmlNode t in tags)
        {
            string key = GetAttribute<string>("k", t.Attributes);
            string value = GetAttribute<string>("v", t.Attributes);
            //string value = GetAttribute<string>("v", t.Attributes);
            if (key == "building:levels")
            {
                Height = 3.0f * GetAttribute<float>("v", t.Attributes);
            }
            else if (key == "height")
            {
                Height = 0.3048f * GetAttribute<float>("v", t.Attributes);
            }
            else if (key == "building")
            {
                IsBuilding = true; // GetAttribute<string>("v", t.Attributes) == "yes";
                Walkable = false;
            }
            else if (key == "amenity")
            {
                IsAmenity = true; // GetAttribute<string>("v", t.Attributes) == "yes";
                Walkable = false;
            }

            else if (key == "highway")
            {
                IsRoad = true;
            }
            else if (key=="lanes")
            {
                Lanes = GetAttribute<int>("v", t.Attributes);
            }
            else if (key =="name")
            {
                Name = GetAttribute<string>("v", t.Attributes);
                
            }
             else if ( key =="landuse" && value == "grass" | value == "protected_area" )
            {
                IsGrass = true;
                IsFlat = true;
                // without this line of code this script works fine
                // _material = Resources.Load("Grass", typeof(Material)) as Material;
            }
            else if ( value == "water" )
            {
                IsWater = true;
                 //_material = Resources.Load("Water", typeof(Material)) as Material;
            }
             else if ( value == "sand")
            {
                IsSand = true;
                IsFlat = true;
                //_material = Resources.Load("Sand", typeof(Material)) as Material;
            }
        }
    }
}

