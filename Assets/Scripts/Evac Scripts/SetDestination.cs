using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetDestination : MonoBehaviour
{
    //public float range = 100.0f;
    private Transform safety;
    private NavMeshPath path;
    public float range = 10.0f;
    bool found_mesh = false;

    [SerializeField] private NavMeshAgent agent; //set in inspector to reduce spawn time


    public NavMeshPath GetPath(GameObject target)
    {
    
        //move to Navmesh
        if (found_mesh == false)
        {
            Vector3 point;
            if (RandomPoint(transform.position, range, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                Debug.Log("did a thing");
                NavMeshAgent agent = GetComponent<NavMeshAgent>();
                agent.Warp(point);
            }
        }
        
    
        path = new NavMeshPath();
        safety = target.transform;
        NavMesh.CalculatePath(transform.position, safety.position, NavMesh.AllAreas, path);       
        return path;
        
    }

   bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                found_mesh = true;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }


}