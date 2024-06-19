using System;
using TMPro;
using UnityEngine;

public class PredatorUI : MonoBehaviour
{
    public float throttle;
    public bool laserOn = false;
    [SerializeField] private Rocket rocket;
    [SerializeField] private CameraZoomPredator camerazoom;
    [SerializeField] private PropellerMovement propellerMovement;
    [SerializeField] private TextMeshProUGUI laserText;
    [SerializeField] private TextMeshProUGUI zoom;
    [SerializeField] private TextMeshProUGUI throttlePercentage;
    [SerializeField] private TextMeshProUGUI distance;
    [SerializeField] private TextMeshProUGUI rocketCount;
    [SerializeField] private TextMeshProUGUI altitude;
    [SerializeField] private TextMeshProUGUI warning;
    [SerializeField] private TextMeshProUGUI speedUI;
    public float speed;
    private float counter;
    [SerializeField] private Playermovementspredator playermovementspredator;
    [SerializeField] private Su57Rocket su57Rocket;
    [SerializeField] private PauseMenu pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        throttle = 50;
        speed = 125;
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.paused) {
            throttlePercentage.text = "Throttle: " + Math.Floor(throttle) + "%";
            altitude.text = "Altitude[m]: " + Math.Floor(transform.position.y);
            zoom.text = "Cam zoom: " + Math.Floor((100 - (Math.Round(((camerazoom.currentFOV - 3) * 100)/57)))/10 + 1) + "x";
            speedUI.text = "Speed[km/h]: " + Math.Floor(speed);

            int numOfrockets = 0;
            foreach(bool i in rocket.rocketFired) {
                if (!i) {
                    numOfrockets++;
                }
            }
            rocketCount.text = "Rockets: " + numOfrockets + "/6";

            if (Input.GetKeyUp(KeyCode.F)) {
                if (laserOn) {
                    laserOn = false;
                } else {
                    laserOn = true;
                }
            }


            if (laserOn && !su57Rocket.hit) {
                laserText.enabled = true;
                distance.text = "Distance[m]: " + Math.Floor(rocket.hit.distance);
            } else {
                laserText.enabled = false;
                distance.text = "Distance[m]: N/A";
            }

            if (Input.GetKey(KeyCode.LeftShift) && throttle < 100) {
                throttle += 10f * Time.deltaTime; 
            }
            
            if (Input.GetKey(KeyCode.LeftControl) && throttle > 1) {
                throttle -= 10f * Time.deltaTime; 
            }

            if (speed < (2.5f * throttle) && !playermovementspredator.climbing) {
                speed += 0.001f * throttle * 0.5f * Time.deltaTime * 50;
            }

            if (speed > (2.5f * throttle) && !playermovementspredator.diving) {
                speed -= 0.001f * (100 - throttle) * 0.5f * Time.deltaTime * 50;
            }

            if (playermovementspredator.diving) {
                speed += 0.001f * transform.rotation.eulerAngles.x * 0.5f * Time.deltaTime * 50;
            }

            if (playermovementspredator.climbing) {
                if (0.001f * (transform.rotation.eulerAngles.x -360) * -1 * 2 - (throttle * 0.000002f) > 0) {
                    speed -= 0.001f * (transform.rotation.eulerAngles.x -360) * -1 * 4 - (throttle * 0.000002f) * Time.deltaTime * 10;
                }
            }
    
            if (speed < 80) {
                counter += Time.deltaTime;
                if(counter >= 0.4) {
                    warning.enabled = true;
                }
                if(counter >= 1) {
                    warning.enabled = false;
                    counter = 0;    
                }
            } else {
                warning.enabled = false;
            }
        }
    }
}
