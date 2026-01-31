using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// TODO When reverting to patrol state, find closest point

// TODO Make sprite look at player when looking at player

// TODO Time to detect decrease over time

// TODO Fix enemy lighting from behind

public class Enemy : MonoBehaviour, ILookingCharacter
{
    private const float RaycastHitThreshold = 0.8f;

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
    private float noMaskDetectMultiplier = 3.0f;
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

    [SerializeField]
    private LayerMask sightBlockingLayers;

    private List<Transform> waypointList = new List<Transform>();
    private int waypointIndex;
    private State state = State.Patroling;
    private Transform player;
    private float detectionSecondsPassed;

    public bool Active { get; set;}

    public Vector2 LookDirection { get; private set; }

    public bool IsMoving { get; private set; }

    private void Awake()
    {
        Active = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        suspicionBarCanvas.SetActive(false);
        InitializeWaypoints();
        waypoints.SetParent(null, true);
        transform.position = waypointList[0].position;
        rigidbody.position = transform.position;
    }

    private void OnDrawGizmos()
    {
        if(waypoints == null)
        {
            return;
        }
        if(waypointList.Count != waypoints.childCount)
        {
            InitializeWaypoints();
        }
        Gizmos.color = Color.red;
        for(int i = 0; i < waypointList.Count; i++)
        {
            Gizmos.DrawLine(waypointList[i].position, waypointList[(i+1) % waypointList.Count].position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if( state == State.Chasing && collision.collider.tag == "Player")
        {
            GameManager.Instance.KillPlayer();
        }
    }

    private void Update()
    {
        if (!Active)
        {
            return;
        }
        if (GameManager.Instance.State != GameManager.GameState.Playing)
        {
            return;
        }
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
        IsMoving = true;

        Vector3 currentWaypointPosition = waypointList[waypointIndex].position;
        Vector3 offset = currentWaypointPosition - rigidbody.position;
        float distance = offset.magnitude;
        Move(offset, moveSpeed);

        if (distance < targetWaypointDistance )
        {
            waypointIndex = (waypointIndex + 1) % waypointList.Count;
        }
    }

    private void Move(Vector3 offset, float speed)
    {
        Vector3 direction = offset.normalized;
        LookDirection = new Vector2(direction.x, direction.z);

        rigidbody.MovePosition(rigidbody.position + direction * speed * Time.deltaTime);
    }

    private void LookForPlayer()
    {
        if (PlayerInView(startDetectionDistance))
        {
            state = State.Detecting;
            suspicionBarCanvas.SetActive(true);
            suspicionBarFill.fillAmount = 0;
            detectionSecondsPassed = 0;
        }
    }

    private void DoDetecting()
    {
        IsMoving = false;

        if (PlayerInView(endDetectionDistance))
        {
            Vector3 offset = player.position - rigidbody.position;
            Vector3 direction = offset.normalized;
            LookDirection = new Vector2(direction.x, direction.z);

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
        IsMoving = true;

        Vector3 offset = player.position - rigidbody.position;
        Move(offset, chaseSpeed);

        if (!PlayerInView(endDetectionDistance))
        {
            state = State.Patroling;
        }
    }

    private bool PlayerInView(float detectDistance)
    {
        if (!GameManager.Instance.PlayerMask.MaskMode)
        {
            detectDistance *= noMaskDetectMultiplier;
        }

        float playerDistance = Vector3.Distance(transform.position, player.position);
        if (playerDistance > detectDistance)
        {
            return false;
        }

        if(Physics.Linecast(transform.position + Vector3.up, player.position + Vector3.up, out RaycastHit hitInfo, sightBlockingLayers))
        {
            float hitDistance = Vector3.Distance(hitInfo.point, player.position);
            if (hitDistance > RaycastHitThreshold)
            {
                return false;
            }
        }

        return true;
    }

    private void InitializeWaypoints()
    {
        foreach (Transform child in waypoints)
        {
            waypointList.Add(child);
        }
    }
}
