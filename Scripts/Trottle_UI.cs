using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Throttle_UI : MonoBehaviour
{
    public Text Throttle_ui;
    public Playermovement throttle;
    //public GameObject cam;
    [SerializeField] private TextMeshProUGUI losingSignal;
    [SerializeField] private TextMeshProUGUI signalLost;
    [SerializeField] private cameraEffect obj;
    [SerializeField] private RawImage crosshair;
    private float timer;


    // Start is called before the first frame update
    void Start()
    {
        losingSignal.enabled = false;
        signalLost.enabled = false;
        crosshair.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {   
        //*if (cam.activeSelf) {
            Throttle_ui.text = (Mathf.Round((throttle.thrust +10f) * (100f - 0f) / (10f +10f) + 0f)).ToString();

            if (obj.far) {
                timer = timer + Time.deltaTime;
                if(timer >= 0.5) {
                    losingSignal.enabled = true;
                }

                if(timer >= 1) {
                    losingSignal.enabled = false;
                    timer = 0;   
                } 
            } else {
                losingSignal.enabled = false;
            }
            if(obj.signalLost) {
                signalLost.enabled = true;
                losingSignal.enabled = false;
                Throttle_ui.enabled = false;
                crosshair.enabled = false;
            } else {
                signalLost.enabled = false;
                Throttle_ui.enabled = true;
                crosshair.enabled = true;
            }
        //}
    }
}