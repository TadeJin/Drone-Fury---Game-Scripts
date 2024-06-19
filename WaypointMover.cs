using UnityEngine;

//Moves objects to waypoints, generates random spawn variable, rotates object models on init
public class WaypointMover : MonoBehaviour
{
    // Stores a reference to a waypoint system this object will use
    [SerializeField] private waypoints waypoints;

    public float moveSpeedMax = 20f;
    public float rotateSpeed = 4f;

    [SerializeField] private float distanceThershold = 0.1f;

    // The current waypoint the target that the object is moving towards
    private Transform currentWaypoint;


    //Rotation target
    private Quaternion roationGoal;
    //Direction to next waypoint
    private Vector3 directionToWaypoint;
    public float startrotationAnglex = 0f;
    public float startrotationAngley = 0f;
    public float startrotationAnglez = 0f;


    // Start is called before the first frame update
    void Start()
    {
        // Set initial position to first waypoint
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
        transform.position = currentWaypoint.position;

        //Set the next waypoint target
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
        transform.LookAt(currentWaypoint);
        }
    

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeedMax * Time.deltaTime);
        if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceThershold) {
            currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
        }
        RotateTowardsWaypoint();
    }

    //Slow rotate
     void RotateTowardsWaypoint() {

        directionToWaypoint = (currentWaypoint.position - transform.position).normalized;
        roationGoal = Quaternion.LookRotation(directionToWaypoint);
        transform.rotation = Quaternion.Slerp(transform.rotation, roationGoal * Quaternion.Euler(startrotationAnglex, startrotationAngley, startrotationAnglez), rotateSpeed * Time.deltaTime);
    }
}