using UnityEngine;
using Pathfinding;

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

    private int currentWaypoint - 0;

     void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if (target = null)
        {
            Debug.LogError("No Player Found");
            return;
        }
        seeker.StartPath(transform.position, target.position, OnPathComplete);

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
}
