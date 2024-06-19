using TMPro;
using UnityEngine;

public class SU57Movement : MonoBehaviour
{
    [SerializeField] private Transform su57;
    [SerializeField] private waypoints path;
    private Transform currentWaypoint;
    private float moveSpeedOfTarget = 5f;
    private float distanceThershold = 2f;
    private float rotateSpeed = 2.5f;
    private bool starting = true;
    [SerializeField] private GameObject wheels;
    [SerializeField] private GameObject rocketFired;
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject drone;
    private bool fired;
    public bool destroyed = false;
    [SerializeField] private BoxCollider flightCollider;
    [SerializeField] private GameObject sound;
    [SerializeField] private TextMeshProUGUI missilewarning;
    public bool go = false;
    private bool stop = false;
    [SerializeField] private Su57Rocket su57rocket;
    // Start is called before the first frame update
    void Start()
    {
        currentWaypoint = path.GetNextWaypoint(currentWaypoint);
        transform.LookAt(currentWaypoint);
    }

    // Update is called once per frame
    void Update()
    {   
        if(go && !stop)  {
            transform.GetComponent<AudioSource>().Play();
            stop = true;
        }
        if (go) {
            if (!destroyed) {
                if (starting) {
                    transform.Translate(Vector3.forward * Time.deltaTime * moveSpeedOfTarget*3);
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

                    if (Vector3.Distance(transform.position, new Vector3(currentWaypoint.position.x,transform.position.y,currentWaypoint.position.z)) < distanceThershold) {
                        if (path.GetNextWaypoint(currentWaypoint) != null) 
                            currentWaypoint = path.GetNextWaypoint(currentWaypoint);
                        else 
                            starting = false;
                    }
                    RotateTowardsWaypoint();
                } else {
                    transform.Translate(Vector3.forward * Time.deltaTime * moveSpeedOfTarget * 20);
                    if (transform.position.y < 100) {
                        transform.Translate(Vector3.up * Time.deltaTime * 4f);
                        su57.rotation = Quaternion.Euler(su57.rotation.x,180,su57.rotation.z);
                    } else {
                        if (!fired) {
                            Quaternion lookrotation = Quaternion.LookRotation(drone.transform.position - transform.position);
                            transform.rotation = Quaternion.Slerp(transform.rotation, lookrotation, Time.fixedDeltaTime * 0.2f);
                        }
                        if (Vector3.Distance(transform.position,drone.transform.position) < 1000) {
                            if (!fired) {
                                rocketFired.transform.position = rocket.transform.position;
                                rocket.SetActive(false);
                                sound.transform.GetComponent<AudioSource>().Play();
                                missilewarning.enabled = true;
                            }
                            fired = true;
                        } 
                    }

                    if(transform.position.y > 50) {
                        wheels.SetActive(false);
                    }

                    if(transform.position.y > 20) {
                        flightCollider.enabled = true;
                    }
                }
            } else {
                transform.GetComponent<AudioSource>().enabled = false;
            }
        }

        if(su57rocket.hit) {
            transform.GetComponent<AudioSource>().enabled = false;
        }
        if (fired) {
            rocketFired.transform.LookAt(drone.transform.position);
            rocketFired.transform.Translate(Vector3.forward * Time.deltaTime * 300);
            }
        }

        private void RotateTowardsWaypoint() {

        Quaternion lookrotation = Quaternion.LookRotation(new Vector3(currentWaypoint.position.x, currentWaypoint.position.y, currentWaypoint.position.z) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookrotation, Time.fixedDeltaTime * rotateSpeed);
        
    }
}

