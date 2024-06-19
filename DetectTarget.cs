using UnityEngine;
using UnityEngine.UI;

public class DetectTarget : MonoBehaviour
{
    public Collisions collisionA;
    public GameObject targetDestroyed;
    public GameObject target;
    public Camera cam;
    public Transform cube;
    public bool track = false;
    [SerializeField] private RawImage crosshair;
    [SerializeReference] private RawImage crosshairTrack;
    public Transform targetObject;
    //[SerializeField] private GameObject scoutcam;
    private bool pom = true;
    [SerializeField] private CameraSwitch mainCam;
    [SerializeField] CameraSwitch cameraSwitch;

    void Start()
    {
        crosshair.enabled = true;
        crosshairTrack.enabled = false;
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

    private void Update ()
    {
        if (collisionA.destruction && pom) {
            track = false;
            pom = false;
            crosshair.enabled = true;
            crosshairTrack.enabled = false;
        }

        if (Input.GetKeyUp(KeyCode.Space) && track && cameraSwitch.isNearScout) {
            Debug.Log("TARGET UNLOCKED");
            track = false;
            crosshair.enabled = true;
            crosshairTrack.enabled = false;
            cube.transform.rotation = Quaternion.Euler(cube.transform.rotation.x - 20,cube.transform.rotation.y + 10,cube.transform.rotation.z);  
        }

        if ((IsVisible(cam,target) && Input.GetKeyUp(KeyCode.Space) && !collisionA.hit && !track) || (IsVisible(cam,targetDestroyed) && Input.GetKeyUp(KeyCode.Space) && collisionA.hit && !track))  
        {
            Debug.Log("TARGET LOCKED");
            track = true;
            crosshair.enabled = false;
            crosshairTrack.enabled = true;
        }
        

        if (track && mainCam.getToFPV() && !collisionA.hit) {
            cube.LookAt(targetObject.position);
        } else if (track && mainCam.getToFPV() && collisionA.hit) {
            cube.LookAt(targetDestroyed.transform.position);
        }
    }
}
