using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.UIElements;

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
        if (IsVisible(cam,target) && Input.GetKey(KeyCode.Space) && !collisionA.hit)  
        {
            //Debug.Log("TARGET LOCKED");
            track = true;
            crosshair.enabled = false;
            crosshairTrack.enabled = true;
        }
        
        if (IsVisible(cam,targetDestroyed) && Input.GetKey(KeyCode.Space) && collisionA.hit) {
            track = true;
            crosshair.enabled = false;
            crosshairTrack.enabled = true;
            Debug.Log("Destroyed LOCKED");
        }
        
    
        if (Input.GetKey(KeyCode.Escape) && track) {
            track = false;
            crosshair.enabled = true;
            crosshairTrack.enabled = false;
            cube.LookAt(target.transform.position);
        }

        if (track && /*scoutcam.activeSelf*/ mainCam.getToFPV() && !collisionA.hit) {
            cube.LookAt(targetObject.position);
        } else if (track && /*scoutcam.activeSelf*/ mainCam.getToFPV() && collisionA.hit) {
            cube.LookAt(targetDestroyed.transform.position);
        }
    }
}
