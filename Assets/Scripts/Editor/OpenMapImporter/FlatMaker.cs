using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
/// Make buildings.
/// </summary>
internal sealed class FlatMaker : BaseInfrastructureMaker
{
    
    private Material grass;
    public override int NodeCount
    
    {
        get
        {
            return map.ways.FindAll((w) => { return w.IsGrass && w.NodeIDs.Count > 1;}).Count;
        }
    }



    public FlatMaker(MapReader mapReader, Material material)
        : base(mapReader)
    {
        grass = material;
    }


    public override IEnumerable<int> Process()
    {
        int count = 0;

        
        // Iterate through all the buildings in the 'ways' list
        foreach (var Grass in map.ways.FindAll((w) => { return w.IsGrass && w.NodeIDs.Count > 1; }))
        {
            // Create the object
            CreateObject(Grass, grass, "Building");

            count++;
            yield return count;
        }  
    }

    /// <summary>
    /// Build the object using the data from the OsmWay instance.
    /// </summary>
    /// <param name="way">OsmWay instance</param>
    /// <param name="origin">The origin of the structure</param>
    /// <param name="vectors">The vectors (vertices) list</param>
    /// <param name="normals">The normals list</param>
    /// <param name="uvs">The UVs list</param>
    /// <param name="indices">The indices list</param>

    protected override void OnObjectCreated(OsmWay way, Vector3 origin, List<Vector3> vertices, List<Vector3> normals, List<Vector2> uvs, List<int> triangles)
    {
        for (int i = 1; i < way.NodeIDs.Count; i++)
        {
    
            OsmNode p1 = map.nodes[way.NodeIDs[i - 1]];
            OsmNode p2 = map.nodes[way.NodeIDs[i]];
 
            //drawing lines between points
            Vector3 v3 = new Vector3(0,0,0);
            Vector3 v1 = p1 - origin;
            Vector3 v2 = p2 - origin;
            
            vertices.Add(v3);
            vertices.Add(v1);
            vertices.Add(v2);
            

            uvs.Add(new Vector2(1, 1));
            uvs.Add(new Vector2(0, 1));
            uvs.Add(new Vector2(0, 0));

            normals.Add(-Vector3.forward);
            normals.Add(-Vector3.forward);
            normals.Add(-Vector3.forward);

            int idx1, idx2;
            idx1 = vertices.Count - 2;
            idx2 = vertices.Count - 1;

            // And now the roof triangles
            triangles.Add(idx1);
            triangles.Add(idx2);
            triangles.Add(0);
            
            // Don't forget the upside down one!
            triangles.Add(idx2);
            triangles.Add(idx1);
            triangles.Add(0);
        }
    }
}
