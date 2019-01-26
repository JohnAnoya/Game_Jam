using UnityEngine;
using Pathfinding;
using System.Collections;

[RequireComponent(typeof (Rigidbody2D))]
[RequireComponent (typeof (Seeker))]

public class EnemyAI : MonoBehaviour {

    public Transform target;
    public float updateRate = 2f;

    private Seeker seeker;
    private Rigidbody2D rb;


    public Path path;


    public float speed = 300f;
    public ForceMode2D fMode;

    [HideInInspector]
    public bool pathIsEnded = false;

    public float nextWaypointDistance = 3;

    private int currentWaypoint = 0;

    private bool searchingForPlayer = false;

     void Start() {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if (target = null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }

            return;
        }
        seeker.StartPath(transform.position, target.position, OnPathComplete);

        StartCoroutine (UpdatePath ());
    }
    
    IEnumerator SearchForPlayer() {
        GameObject sResult = GameObject.FindGameObjectWithTag("Player");
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if(sResult == null)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine (SearchForPlayer() );
        } else
        {
            target = sResult.transform;
            searchingForPlayer = false;
            StartCoroutine (UpdatePath() );
            return false;
        }
    }

    IEnumerator UpdatePath ()
    {
        if (target = null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }

            return false;
        }

        seeker.StartPath(transform.position, target.position, OnPathComplete);

        yield return new WaitForSeconds(1f/ updateRate);
        StartCoroutine (UpdatePath());


    }

    public void OnPathComplete (Path p)
    {
        Debug.Log("Got a path, any errors?" + p.error);
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;


        }
    }

    void FixedUpdate()
    {
        if (target = null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }

            return;
        }

        if (path == null)
        {
            return;
        }

        if(currentWaypoint >= path.vectorPath.Count)
        {
            if (pathIsEnded)
                return;

            Debug.Log("Path reached");
            pathIsEnded = true;
            return;
        }

        pathIsEnded = false;

        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;

        dir *= speed * Time.fixedDeltaTime;


        rb.AddForce(dir, fMode);

        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);

       if (dist < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }
    }
}
