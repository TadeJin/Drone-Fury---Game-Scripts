using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class AirToAirMissile : MonoBehaviour
{
     private float flightSpeed = 150f;
     private float rotateSpeed = 100f;
    private bool locking = false;
    private bool locked = false;
    [SerializeField] private GameObject su57;
    [SerializeField] private Camera Targetcamera;
    [SerializeField] private GameObject cube;
    private bool fire1 = false;
    private bool fire2 = false;
    [SerializeField] private GameObject fakeRocket1;
    [SerializeField] private GameObject fakeRocket2;
    [SerializeField] private GameObject rocket1;
    [SerializeField] private GameObject rocket2;
    private int rocketCount = 2;
    [SerializeField] private TextMeshProUGUI src;
    [SerializeField] private TextMeshProUGUI lockText;
    [SerializeField] private TextMeshProUGUI rocketCountText;
    private float counter;

    private void Start() {
        
    }
    // Update is called once per frame
    void Update()
    {
        rocketCountText.text = "AAM: " + rocketCount + "/2";
        if(Input.GetMouseButtonDown(1) && !locking && !locked) {
            locking = true;
        } else if (Input.GetMouseButtonDown(1) && locking){
            locking = false;
        } else if (Input.GetMouseButtonDown(1) && locked) {
            locked = false;
            locking = false;
        }

        if (locking && !transform.GetComponent<Volume>().enabled) {
            if(IsVisible(Targetcamera,su57) && su57.transform.position.y > 20) {
                locked = true;
                transform.GetChild(2).GetComponent<AudioSource>().Play();
            }

            counter += Time.deltaTime;
            if(counter >= 0.4) {
                src.enabled = true;
            }
            if(counter >= 1) {
                src.enabled = false;
                counter = 0;    
            }
        } else {
            src.enabled = false;
        }

        if (locked && !transform.GetComponent<Volume>().enabled) {
            lockText.enabled = true;
            locking = false;
            cube.transform.LookAt(su57.transform);

            if (Input.GetMouseButtonUp(0) && rocketCount > 0) {
                transform.GetComponent<AudioSource>().Play();
                locked = false;
                locking = false;
                if (!fire1) {
                    fire1 = true;
                    fakeRocket1.SetActive(false);
                    rocket1.transform.position = new Vector3(fakeRocket1.transform.position.x+2,fakeRocket1.transform.position.y,fakeRocket1.transform.position.z);
                    Invoke("enableCollider",2);
                    rocketCount--;
                } else {
                    fire2 = true;
                    fakeRocket2.SetActive(false);
                    rocket2.transform.position = new Vector3(fakeRocket1.transform.position.x-2,fakeRocket1.transform.position.y,fakeRocket1.transform.position.z);
                    Invoke("enableCollider",2);
                    rocketCount--;
                }
            }
        } else {
            lockText.enabled = false;
        }

        if (fire1) {
            rocket1.transform.Translate(Vector3.forward * Time.deltaTime * flightSpeed);
            if (rocket1.GetComponent<BoxCollider>().enabled) {
                Quaternion lookrotation = Quaternion.LookRotation(su57.transform.position - rocket1.transform.position);
                rocket1.transform.rotation = Quaternion.RotateTowards(rocket1.transform.rotation,lookrotation, rotateSpeed * Time.deltaTime);
            }
        }

        if (fire2) {
            rocket2.transform.Translate(Vector3.forward * Time.deltaTime * flightSpeed);
            if (rocket2.GetComponent<BoxCollider>().enabled) {
                Quaternion lookrotation = Quaternion.LookRotation(su57.transform.position - rocket2.transform.position);
                rocket2.transform.rotation = Quaternion.RotateTowards(rocket2.transform.rotation,lookrotation, rotateSpeed * Time.deltaTime);
            }
        }
    }

    private bool IsVisible(Camera c, GameObject target)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = target.transform.position;

        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point)< 0)
            {
                return false;
            }
        }
        return true;
    }

    private void enableCollider() {
        if (fire1) {
            rocket1.GetComponent<BoxCollider>().enabled = true;
        }

        if (fire2) {
            rocket2.GetComponent<Collider>().enabled = true;
        }
    }
}
