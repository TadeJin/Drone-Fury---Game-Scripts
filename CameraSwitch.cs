using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

//Switches camera view
public class CameraSwitch : MonoBehaviour
{
    private Vector3 pointNearScout = new Vector3(522.6724f,24.5788f, 107.965f);
    private Vector3 pointFarScout = new Vector3(522.656f, 24.7133f, 107.7441f);
    private Vector3 pointNearFPV = new Vector3(521.79f, 24.5788f, 107.965f);
    private Vector3 pointFarFPV = new Vector3(521.774f, 24.7133f, 107.7441f);
    private bool start = false;

    public bool isNearScout = false;
    private bool isFarScout = false;
    private bool isNearFPV = false;
    private bool isFarFPV = false;
    private bool pressed = false;
    private bool step1 = false;
    private bool step2 = false;
    private bool toFPV = true;
   private float moveSpeed = 0.4f;

    private void Start() {
        RenderSettings.fog = false;
    }

    void Update() {
        if (!start) {
            transform.position = Vector3.MoveTowards(transform.position, pointNearScout, moveSpeed * 0.75f * Time.deltaTime);
        }

        if (transform.position == pointNearScout) {
            start = true;
        }

        if (transform.position == pointNearScout) {
            isNearScout = true;
            isFarScout= false;
            isNearFPV = false;
            isFarFPV = false;
        } else if (transform.position == pointFarScout) {
            isNearScout = false;
            isFarScout= true;
            isNearFPV = false;
            isFarFPV = false;
        } else if (transform.position == pointNearFPV) {
            isNearScout = false;
            isFarScout= false;
            isNearFPV = true;
            isFarFPV = false;
        } else if (transform.position == pointFarFPV) {
            isNearScout = false;
            isFarScout= false;
            isNearFPV = false;
            isFarFPV = true;
        }

        
        if ((Input.GetKey(KeyCode.Tab) && isNearFPV) || (Input.GetKey(KeyCode.Tab) && isNearScout)) {
            pressed = true;
        }

        if (pressed && toFPV) { //moves camera to FPV drone controller
            moveCamToFPV();
        }


        if (pressed && !toFPV) { //moves camera to scout drone controller
            moveCamToScout();
        }

        }

        public void moveCamToFPV() {
            RenderSettings.fog = true;
            if(!isFarScout && !step1 && !step2) {
                transform.position = Vector3.MoveTowards(transform.position, pointFarScout, moveSpeed * 3 * Time.deltaTime);
            } else if (isFarScout) {
                step1 = true;
            }

            if(step1 && !isFarFPV) {

                transform.position = Vector3.MoveTowards(transform.position, pointFarFPV, moveSpeed * 2 * Time.deltaTime);
            } else if (isFarFPV){
                step2 = true;
            }

            if(step1 && step2 && !isNearFPV) {
                transform.position = Vector3.MoveTowards(transform.position, pointNearFPV, moveSpeed * 3 * Time.deltaTime);
            } else if (isNearFPV) {
                step1 = false;
                step2 = false;
                pressed = false;
                toFPV = false;
            }
        }

        public bool moveCamToScout() {
            RenderSettings.fog = false;
            if(!isFarFPV && !step1 && !step2) {
                transform.position = Vector3.MoveTowards(transform.position, pointFarFPV, moveSpeed * 3 * Time.deltaTime);
            } else if (isFarFPV){
                step1 = true;
            }

            if (step1 && !isFarScout) {
                transform.position = Vector3.MoveTowards(transform.position, pointFarScout, moveSpeed * 2 * Time.deltaTime);
            } else if (isFarScout){
                step2 = true;
            }

            if(step1 && step2 && !isNearScout) {
                transform.position = Vector3.MoveTowards(transform.position, pointNearScout, moveSpeed * 3 * Time.deltaTime);
            } else if (isNearScout) {
                step1 = false;
                step2 = false;
                pressed = false;
                toFPV = true;
                return true;
            }
        return false;
        }

        public bool getToFPV() {
            return toFPV;
        }
    }
