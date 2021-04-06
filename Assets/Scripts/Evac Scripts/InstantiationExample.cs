using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


public class InstantiationExample : MonoBehaviour 
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public SetDestination myPrefab;
    public int noOfAgents = 40;
    public GameObject Safehouse;

    public MeshFilter roadMeshParent;
    private Mesh roadMesh;

    List<NavMeshAgent> navAgent = new List<NavMeshAgent>();
    List<NavMeshPath> paths = new List<NavMeshPath>();


    // This script will simply instantiate the Prefab when the game starts.
    void Start()
    {

        transform.position = roadMeshParent.transform.position;
        transform.rotation = roadMeshParent.transform.rotation;
        roadMesh = roadMeshParent.mesh;

        
        var points = evenlyDistributedPointsOnMesh(noOfAgents); //this method returns a list of seperated verticies

        for (int i = 0; i < noOfAgents; i++)
        {
            // Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent);
            SetDestination agent = Instantiate(myPrefab, transform.position, Quaternion.identity, transform);
            //scaling it to the same as the parent
            //agent.name = "Person" + i.ToString();    
            agent.transform.localPosition =  Vector3.Scale(points[i], roadMeshParent.transform.lossyScale);
            Vector3 currentposition = agent.transform.position;
            

            //agent.GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(currentposition);
            //navAgent[i] = GetComponent<agent.AI.NavMeshAgent>();
            //Debug.Log("warpywarp");

            navAgent.Add(agent.GetComponent<NavMeshAgent>());
            paths.Add(agent.GetPath(Safehouse));
            
            Debug.Log("start");
        }
    }

private void Update()
    {
             //for ( int i = 0; i<noOfAgents; i++)
             {
                 //navAgent[i].SetPath(paths[i]);
             }
     }







    List<Vector3> evenlyDistributedPointsOnMesh(int pointCount) 
    {

        var result = new List<Vector3>();
        int spacing = roadMesh.vertexCount / pointCount;



        for (int i = 0; i< pointCount; i++) 
        {
            var point = roadMesh.vertices[i * spacing];

            //scale?
            //adding to the list
            result.Add(point);
        }
        return result;
    }
}
        
