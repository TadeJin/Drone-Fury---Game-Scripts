using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private GameObject rocket;
    [SerializeField] private GameObject rocket1;
    [SerializeField] private GameObject rocket2;
    [SerializeField] private GameObject rocket3;
    [SerializeField] private GameObject rocket4;
    [SerializeField] private GameObject rocket5;
    [SerializeField] private GameObject rocket6;
    [SerializeField] private GameObject guidance;
    [SerializeField] private GameObject cube;
    private float flightSpeed = 100f;
    private float rotateSpeed = 75f;
    private bool fire = false;
    public RaycastHit hit;
    private List<GameObject> rockets = new List<GameObject> {};
    public List<bool> rocketFired = new List<bool> {};
    [SerializeField] private GameObject cam;
    [SerializeField] private PredatorUI predatorUI;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private GuidanceCollision guidanceCollision;
    [SerializeField] private GameObject BTR;
    [SerializeField] private GameObject URAL;
    [SerializeField] private GameObject KAMAZ   ;
    [SerializeField] private GameObject T72;
    [SerializeField] private GameObject SU57;
    


    // Start is called before the first frame update
    void Start()
    {
        rockets.Add(rocket1);
        rockets.Add(rocket2);
        rockets.Add(rocket3);
        rockets.Add(rocket4);
        rockets.Add(rocket5);
        rockets.Add(rocket6);

        foreach(GameObject rocket in rockets) {
            rocket.GetComponent<BoxCollider>().enabled = false;
        }

        rocketFired.Add(false);
        rocketFired.Add(false);
        rocketFired.Add(false);
        rocketFired.Add(false);
        rocketFired.Add(false);
        rocketFired.Add(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.paused) {
            transform.GetChild(0).GetComponent<AudioSource>().enabled = true;
            for (int i = 0; i < 6; i++) {
                if (rocketFired[i] == false) {
                    rockets[i].transform.SetPositionAndRotation(new Vector3(transform.position.x,transform.position.y - 0.05f,transform.position.z), Quaternion.Euler((360 - transform.rotation.eulerAngles.x) * -1, (360 - transform.rotation.eulerAngles.y) * -1, (360 -transform.rotation.eulerAngles.z) * -1));
                }
            }

                
                
            cam.transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(transform.rotation.eulerAngles.x - 360, transform.rotation.eulerAngles.y - 360, transform.rotation.eulerAngles.z - 360));
            int remainingRockets = 0;
            foreach (bool i in rocketFired) {
                if (!i)
                    remainingRockets++;
            }

            if (Input.GetKeyUp(KeyCode.Space) && remainingRockets != 0) {
                transform.GetComponent<AudioSource>().Play();
                if (rocketFired[0] == false && rocketFired[1] == false && rocketFired[2] == false && rocketFired[3] == false && rocketFired[4] == false && rocketFired[5] == false) {
                    fire = true;
                    rocketFired[0] = true;
                    for (int j = 0; j < 4;j++) {
                        rockets[0].transform.GetChild(j).gameObject.SetActive(true);
                    }
                    Invoke("enableCollider",0.25f);
                }
                else if (rocketFired[0] == true && rocketFired[1] == false && rocketFired[2] == false && rocketFired[3] == false && rocketFired[4] == false && rocketFired[5] == false) {
                    rocketFired[1] = true;
                    for (int j = 0; j < 4;j++) {
                        rockets[1].transform.GetChild(j).gameObject.SetActive(true);
                    }
                    Invoke("enableCollider",0.25f);
                }
                else if (rocketFired[0] == true && rocketFired[1] == true && rocketFired[2] == false && rocketFired[3] == false && rocketFired[4] == false && rocketFired[5] == false) {
                    rocketFired[2] = true;
                    for (int j = 0; j < 4;j++) {
                        rockets[2].transform.GetChild(j).gameObject.SetActive(true);
                    }
                    Invoke("enableCollider",0.25f);
                }
                else if (rocketFired[0] == true && rocketFired[1] == true && rocketFired[2] == true && rocketFired[3] == false && rocketFired[4] == false && rocketFired[5] == false) {
                    rocketFired[3] = true;
                    for (int j = 0; j < 4;j++) {
                        rockets[3].transform.GetChild(j).gameObject.SetActive(true);
                    }
                    Invoke("enableCollider",0.25f);
                } 
                else if (rocketFired[0] == true && rocketFired[1] == true && rocketFired[2] == true && rocketFired[3] == true && rocketFired[4] == false && rocketFired[5] == false) {
                    rocketFired[4] = true;
                    for (int j = 0; j < 4;j++) {
                        rockets[4].transform.GetChild(j).gameObject.SetActive(true);
                    }
                    Invoke("enableCollider",0.25f);
                } 
                else if (rocketFired[0] == true && rocketFired[1] == true && rocketFired[2] == true && rocketFired[3] == true && rocketFired[4] == true && rocketFired[5] == false) {
                    rocketFired[5] = true;
                    for (int j = 0; j < 4;j++) {
                        rockets[5].transform.GetChild(j).gameObject.SetActive(true);
                    }
                    Invoke("enableCollider",0.25f);
                }
            }

            Ray ray = new Ray(cube.transform.position, cube.transform.forward);

            if(Physics.Raycast(ray, out hit)) {
                guidance.transform.position = new Vector3(hit.point.x,0,hit.point.z);
                Debug.DrawLine(cube.transform.position,hit.point);
            }
            
            
            if (fire) {
                for (int i = 0; i < 6; i++) {
                    if (rocketFired[i] == true) {
                        rocket = rockets[i];
                        rocket.transform.Translate(Vector3.forward * Time.deltaTime * flightSpeed);
                        if (predatorUI.laserOn) {
                            if (rocket.GetComponent<BoxCollider>().enabled) {
                                if (guidanceCollision.BTR) {
                                    Quaternion lookrotation = Quaternion.LookRotation(BTR.transform.position - rocket.transform.position);
                                    rocket.transform.rotation = Quaternion.RotateTowards(rocket.transform.rotation,lookrotation, rotateSpeed * Time.deltaTime);
                                } else if (guidanceCollision.URAL) {
                                    Quaternion lookrotation = Quaternion.LookRotation(URAL.transform.position - rocket.transform.position);
                                    rocket.transform.rotation = Quaternion.RotateTowards(rocket.transform.rotation,lookrotation, rotateSpeed * Time.deltaTime);
                                } else if (guidanceCollision.KAMAZ) {
                                    Quaternion lookrotation = Quaternion.LookRotation(KAMAZ.transform.position - rocket.transform.position);
                                    rocket.transform.rotation = Quaternion.RotateTowards(rocket.transform.rotation,lookrotation, rotateSpeed * Time.deltaTime);
                                } else if (guidanceCollision.T72) {
                                    Quaternion lookrotation = Quaternion.LookRotation(T72.transform.position - rocket.transform.position);
                                    rocket.transform.rotation = Quaternion.RotateTowards(rocket.transform.rotation,lookrotation, rotateSpeed * Time.deltaTime);
                                } else if (guidanceCollision.SU57) {
                                    Quaternion lookrotation = Quaternion.LookRotation(SU57.transform.position - rocket.transform.position);
                                    rocket.transform.rotation = Quaternion.RotateTowards(rocket.transform.rotation,lookrotation, rotateSpeed * Time.deltaTime);
                                } else {
                                    Quaternion lookrotation = Quaternion.LookRotation(guidance.transform.position - rocket.transform.position);
                                    rocket.transform.rotation = Quaternion.RotateTowards(rocket.transform.rotation,lookrotation, rotateSpeed * Time.deltaTime);
                                }
                            }
                        }
                    }
                }
            }
        } else {
            transform.GetChild(0).GetComponent<AudioSource>().enabled = false;
        }
    }

    private void enableCollider() {
        for(int i = 0; i < 6; i++) {
            if (rocketFired[i] == true) {
                rockets[i].GetComponent<BoxCollider>().enabled = true;
            }
        }
    }
}

