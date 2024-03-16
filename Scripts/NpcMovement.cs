using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Moves objects to waypoints, generates random spawn variable, rotates object models on init
public class NpcMovement : MonoBehaviour
{
    // Stores a reference to a waypoint system this object will use
    public WaypointsRandom waypointsRandom;
    private float moveSpeedOfTarget = 6f;

    public float rotateSpeed = 4f;

    protected float distanceThershold = 0.1f;

    // The current waypoint the target that the object is moving towards
    public Transform currentWaypoint;
    public bool RandomSpawn = true;
    public float startrotationAnglex = 0f;
    public float startrotationAngley = 0f;
    public float startrotationAnglez = 0f;

    public int NumberOfPaths;
    public int spawnNumber;
    protected RaycastHit hit;
    protected float adjust;
    public float waypointAdjust;
    [SerializeField] private cameraEffect cameraEffect;


    // Start is called before the first frame update
    void Start()
    {
       /* if (transform.GetComponent<MeshRenderer>() != null) {

            transform.GetComponent<MeshRenderer>().enabled = true;
            for (int i = 0; i < transform.childCount; i++) {

                transform.GetChild(i).GetComponent<MeshRenderer>().enabled = true;
            }
        }*/
        
        spawnNumber = Random.Range(0,NumberOfPaths);
        //Set initial position to first waypoint
        currentWaypoint = waypointsRandom.GetNextWaypoint(currentWaypoint);
        transform.position = currentWaypoint.position;

        // Set initial position to first waypoint
        currentWaypoint = waypointsRandom.GetNextWaypoint(currentWaypoint);
        transform.LookAt(currentWaypoint);
        
         Ray ray1 = new Ray(currentWaypoint.position, Vector3.down);

        if(Physics.Raycast(ray1, out hit)) {
            waypointAdjust = currentWaypoint.position.y - hit.distance;
            }
    }

    // Update is called once per frame
    void Update()
    {
        if (!cameraEffect.signalLost) {
            Ray ray = new Ray(transform.position, Vector3.down);

            if(Physics.Raycast(ray, out hit)) {
                if (hit.distance > 0.005) {
                    adjust = (hit.distance - 0.005f) * -1;
                } else {
                    adjust = (0.005f - hit.distance);
                }
            }

            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeedOfTarget);
            transform.position = new Vector3(transform.position.x, transform.position.y + adjust, transform.position.z);
            //transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeedOfTarget * Time.deltaTime);
            if (Vector3.Distance(transform.position, new Vector3(currentWaypoint.position.x,transform.position.y,currentWaypoint.position.z)) < distanceThershold) {

                currentWaypoint = waypointsRandom.GetNextWaypoint(currentWaypoint);
            Ray ray1 = new Ray(currentWaypoint.position, Vector3.down);

            if(Physics.Raycast(ray1, out hit)) {
                waypointAdjust = currentWaypoint.position.y - hit.distance;
                }
            } 
            RotateTowardsWaypoint();
        }
    }

    //Slow rotate
    private void RotateTowardsWaypoint() {

        Quaternion lookrotation = Quaternion.LookRotation(new Vector3(currentWaypoint.position.x, waypointAdjust, currentWaypoint.position.z) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookrotation, Time.fixedDeltaTime * rotateSpeed);
        
    }
}


