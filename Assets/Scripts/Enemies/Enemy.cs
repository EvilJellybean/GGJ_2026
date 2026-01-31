using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1;
    [SerializeField]
    private float targetWaypointDistance = 0.5f;
    [SerializeField]
    private Transform waypoints;
    [SerializeField]
    private Rigidbody rigidbody;

    private List<Transform> waypointList = new List<Transform>();
    private int waypointIndex;

    private void Awake()
    {
        InitializeComponents();
        waypoints.SetParent(null, true);
    }

    private void OnDrawGizmos()
    {
        if(waypoints == null)
        {
            return;
        }
        if(waypointList.Count != waypoints.childCount)
        {
            InitializeComponents();
        }
        Gizmos.color = Color.red;
        for(int i = 0; i < waypointList.Count; i++)
        {
            Gizmos.DrawLine(waypointList[i].position, waypointList[(i+1) % waypointList.Count].position);
        }
    }

    private void Update()
    {
        Vector3 currentWaypointPosition = waypointList[waypointIndex].position;
        Vector3 offset = currentWaypointPosition - rigidbody.position;
        float distance = offset.magnitude;

        rigidbody.MovePosition(rigidbody.position + offset.normalized * moveSpeed * Time.deltaTime);

        if (distance < targetWaypointDistance )
        {
            waypointIndex = (waypointIndex + 1) % waypointList.Count;
        }
    }

    private void InitializeComponents()
    {
        foreach (Transform child in waypoints)
        {
            waypointList.Add(child);
        }
    }
}
