using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NpcMovement2 : MonoBehaviour
{
    [SerializeField] private GameObject target1;
    [SerializeField] private GameObject target2;
    [SerializeField] private GameObject target3;
    [SerializeField] private GameObject target4;
    private float moveSpeedOfTarget = 6f;

    public float rotateSpeed = 4f;

    protected float distanceThershold = 0.1f;
    private GameObject[] targets = new GameObject[4];
    private int[] pathNumbers = new int[6];
    //private int count = 5;
    private int[] vehiclePaths = new int[4];
    [SerializeField] private WaypointsRandom2 waypointsRandom2;
    protected RaycastHit hit;
    private Transform[] currentWaypoints = new Transform[4];
    private float[] waypointAdjusts = new float[4];
    private float[] adjusts = new float[4];
    public bool[] notDestroyed = new bool[4];
    [SerializeField] private GameObject su57;
    [SerializeField] private TextMeshProUGUI UIDestroyedtext;
    [SerializeField] private SU57Movement sU57Movement;



    // Start is called before the first frame update
    void Start()
    {
        notDestroyed[0] = true;
        notDestroyed[1] = true;
        notDestroyed[2] = true;
        notDestroyed[3] = true;

        targets[0] = target1;
        targets[1] = target2;
        targets[2] = target3;
        targets[3] = target4;

        pathNumbers[0] = 0;
        pathNumbers[1] = 1;
        pathNumbers[2] = 2;
        pathNumbers[3] = 3;
        pathNumbers[4] = 4;
        pathNumbers[5] = 5;

        for (int i = 0; i < 4; i++) {
            int random = UnityEngine.Random.Range(0,6);
            while (pathNumbers[random] == -1) {
                random = UnityEngine.Random.Range(0,6);
            }
            vehiclePaths[i] = pathNumbers[random];
            pathNumbers[random] = -1;
        }


        for(int i = 0; i < 4; i++) {
            currentWaypoints[i] = waypointsRandom2.GetNextWaypoint2(currentWaypoints[i],i);
            targets[i].transform.position = new Vector3(currentWaypoints[i].position.x,0,currentWaypoints[i].position.z - 1);
        }
    }

    // Update is called once per frame
    void Update()
    {   
        int count = 0;
        foreach(bool i in notDestroyed) {
            if (!i) {
                count++;
            }
        }
        UIDestroyedtext.text = "• Locate and destroy targets " + count + "/4 \n • Avoid getting destroyed";
        if (notDestroyed.Contains(false)) {
            sU57Movement.go = true;
        }
        for (int i = 0; i < 4; i++) {
            if (notDestroyed[i]) {
                Ray ray = new Ray(targets[i].transform.position, Vector3.down);

                if(Physics.Raycast(ray, out hit)) {
                    if (hit.distance > 0.005) {
                        adjusts[i] = (hit.distance - 0.005f) * -1;
                    } else {
                        adjusts[i] = (0.005f - hit.distance);
                    }
                }
            
                targets[i].transform.Translate(Vector3.forward * Time.deltaTime * moveSpeedOfTarget);
                try {
                    if (Vector3.Distance(new Vector3(targets[i].transform.position.x,targets[i].transform.position.y,targets[i].transform.position.z), new Vector3(currentWaypoints[i].position.x,0,currentWaypoints[i].position.z)) < distanceThershold) {

                        currentWaypoints[i] = waypointsRandom2.GetNextWaypoint2(currentWaypoints[i],i);
                    } 
                    RotateTowardsWaypoint(i);
                } catch (NullReferenceException) {
                    SceneManager.LoadScene("Target_lost");
            }
            } 
        }
        int iCount = 0;
        foreach (bool i in notDestroyed) {
            if (!i) {
                iCount++;
            }
        }

        if(iCount == 4) {
            Invoke("missionCompleted",5);
        }
    }
        //Slow rotation
    private void RotateTowardsWaypoint(int i) {
        Quaternion lookrotation = Quaternion.LookRotation(new Vector3(currentWaypoints[i].position.x, waypointAdjusts[i], currentWaypoints[i].position.z) - targets[i].transform.position);
        targets[i].transform.rotation = Quaternion.Slerp(targets[i].transform.rotation, lookrotation, Time.fixedDeltaTime * rotateSpeed);
    }

    public int getVehiclePath(int i) {
        return vehiclePaths[i];
    }

    private void missionCompleted() {
        SceneManager.LoadScene("Mission_completed");
    }
}
