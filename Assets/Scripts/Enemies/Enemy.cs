using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private enum State
    {
        Patroling,
        Detecting,
        Chasing,
    }

    [SerializeField]
    private float moveSpeed = 1;
    [SerializeField]
    private float chaseSpeed = 3;
    [SerializeField]
    private float targetWaypointDistance = 2.0f;
    [SerializeField]
    private float startDetectionDistance = 3.0f;
    [SerializeField]
    private float endDetectionDistance = 1.0f;
    [SerializeField]
    private float detectionSeconds = 2;
    [SerializeField]
    private Transform waypoints;
    [SerializeField]
    private Rigidbody rigidbody;
    [SerializeField]
    private GameObject suspicionBarCanvas;
    [SerializeField]
    private Image suspicionBarFill;

    private List<Transform> waypointList = new List<Transform>();
    private int waypointIndex;
    private State state = State.Patroling;
    private Transform player;
    private float detectionSecondsPassed;

    public bool Active { get; set;}

    private void Awake()
    {
        Active = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        suspicionBarCanvas.SetActive(false);
        InitializeComponents();
        waypoints.SetParent(null, true);
    }

    private void OnDrawGizmos()
    {
        if(!Active)
        {
            return;
        }
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
        switch(state)
        {
            case State.Patroling:
                DoPatrolling();
                LookForPlayer();
                break;
            case State.Detecting:
                DoDetecting();
                break;
            case State.Chasing:
                DoChasing();
                break;
        } 
    }

    private void DoPatrolling()
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

    private void LookForPlayer()
    {
        float playerDistance = Vector3.Distance(rigidbody.position, player.position);
        if(playerDistance < startDetectionDistance)
        {
            state = State.Detecting;
            suspicionBarCanvas.SetActive(true);
            suspicionBarFill.fillAmount = 0;
            detectionSecondsPassed = 0;
        }
    }

    private void DoDetecting()
    {
        float playerDistance = Vector3.Distance(rigidbody.position, player.position);
        if(playerDistance < endDetectionDistance)
        {
            detectionSecondsPassed += Time.deltaTime;
            suspicionBarFill.fillAmount = detectionSecondsPassed / detectionSeconds;
            if(detectionSecondsPassed > detectionSeconds)
            {
                state = State.Chasing;
                suspicionBarCanvas.SetActive(false);
            }
        }
        else
        {
            state = State.Patroling;
            suspicionBarCanvas.SetActive(false);
            detectionSecondsPassed = 0;
        }
    }

    private void DoChasing()
    {
        Vector3 offset = player.position - rigidbody.position;
        rigidbody.MovePosition(rigidbody.position + offset.normalized * chaseSpeed * Time.deltaTime);

        float playerDistance = offset.magnitude;
        if(playerDistance > endDetectionDistance)
        {
            state = State.Patroling;
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
