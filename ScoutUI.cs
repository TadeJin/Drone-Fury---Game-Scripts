using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ScoutUI : MonoBehaviour
{
    protected double speedPercentage = 100;
    [SerializeField] protected TextMeshProUGUI throttlePercentage;
    [SerializeField] private WaypointMover Currentspeed;
    [SerializeField] private RawImage crosshairTrack;
    [SerializeField] protected TextMeshProUGUI warning;
    [SerializeField] private TextMeshProUGUI track;
    [SerializeField] private DetectTarget isON;
    //[SerializeField] private GameObject cam;
    [SerializeField] private TextMeshProUGUI zoom;
    [SerializeField] private CameraZoom currentZoom;
    [SerializeField] private TextMeshProUGUI hitConfirm;
    private float timer;
    [SerializeField] private CameraSwitch mainCam;

private void Start() {
    
    crosshairTrack.enabled = false;
    hitConfirm.enabled = false;
}

private void Update() {
        throttlePercentage.text = "THROTTLE: " + speedPercentage + "%";
        zoom.text = "ZOOM: " + (100 - (Math.Round(((currentZoom.currentFOV - 18) * 100)/42))) + "%";

        if (Input.GetKey(KeyCode.LeftShift) && mainCam.getToFPV()) {

            if (Currentspeed.moveSpeedMax < 10f) {

                Currentspeed.moveSpeedMax += 5f * Time.deltaTime;
            }
        }
        
        if (Input.GetKey(KeyCode.LeftControl) && mainCam.getToFPV()) {

            if(Currentspeed.moveSpeedMax > 4.5f) {

                Currentspeed.moveSpeedMax -= 5f * Time.deltaTime;
            }
        }

        speedPercentage = Math.Round((Currentspeed.moveSpeedMax * 100f) / 10f);

        if (speedPercentage == 45d) {
            timer = timer + Time.deltaTime;
          if(timer >= 0.4)
          {
                  warning.enabled = true;
          }
          if(timer >= 1)
          {
                  warning.enabled = false;
                  timer = 0;   
            } 
        } else {
            warning.enabled = false;
            }

        if (isON.track) {
            track.text = "ON";
            timer = timer + Time.deltaTime;
          if(timer >= 1) {
            track.enabled = true;
          }

          if(timer >= 2) {
            track.enabled = false;
            timer = 0;   
        } 
        
        } else {
            track.text = "";
            }
    }
}
