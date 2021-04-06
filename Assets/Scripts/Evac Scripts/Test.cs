using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    Vector3 p1 = new Vector3(0, 0, 0);
    Vector3 p2 = new Vector3(3, 0, 1);
    Vector3 p3 = new Vector3(6, 0, 2);

    // Start is called before the first frame update
    void Start()
    {
        Debug.DrawLine(p1, p2, Color.white, 300f);
        Debug.DrawLine(p2, p3, Color.white, 300f);
        Debug.Log("lines drawn");

        //Vector3 inp3 = Intersection(p1, p2, p3, p4);


        Vector3 diff1 = (p2 - p1).normalized;
        Vector3 diff2 = (p3 - p2).normalized;

        // https://en.wikipedia.org/wiki/Lane
        // According to the article, it's 3.7m in Canada
        var cross1 = Vector3.Cross(diff1, Vector3.up) * 3.7f;
        var cross2 = Vector3.Cross(diff2, Vector3.up) * 3.7f;

        // Create points that represent the width of the road
        Vector3 v1 = p1 + cross1;
        Vector3 v2 = p1 - cross1;
        Vector3 v3 = p2 + cross1;
        Vector3 v4 = p2 - cross1;

        Vector3 v5 = p2 + cross2;
        Vector3 v6 = p2 - cross2;
        Vector3 v7 = p3 + cross2;
        Vector3 v8 = p3 - cross2;

        Debug.DrawLine(v1, v3, Color.green, 300f);
        Debug.DrawLine(v5, v7, Color.blue, 300f);

        Debug.DrawLine(v7, v7, Color.green, 300f);
        Debug.DrawLine(v6, v8, Color.green, 300f);

        Vector3 innew = Intersection(v1, v3, v5, v7);
        Debug.DrawLine(v1, innew, Color.red, 300f);
        Debug.DrawLine(v7, innew, Color.red, 300f);

        Vector3 innewr = Intersection(v2, v4, v6, v8);
        Debug.DrawLine(v2, innewr, Color.cyan, 300f);
        Debug.DrawLine(v8, innewr, Color.cyan, 300f);
    }

    // Update is called once per frame
    void Update()
    {


    }

    public Vector3 Intersection(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4)
    {
        Vector3 sect = new Vector3();

        float Grad1 = (p2.z - p1.z) / (p2.x - p1.x);
        float Grad2 = (p4.z - p3.z) / (p4.x - p3.x);

        float c1 = p1.z - Grad1 * p1.x;
        float c2 = p3.z - Grad2 * p3.x;

        sect.x = (c2 - c1) / (Grad1 - Grad2);
        sect.y = 0;
        sect.z = Grad1 * sect.x + c1;
        return sect;

    }
}
