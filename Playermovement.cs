using UnityEngine;

public class Playermovement : MonoBehaviour
{
    //natáčení po x, z o 7.5 stupně

    Rigidbody player;
    private float right_left_Input;
    private float forward_backward_Input;
    private bool fall_Input;
    private Vector2 tilt;
    private float maxTilt = 7.5f;
    private float tiltAcceleration = 10f;
    private float tiltDeceleration = 20f;
    private Vector2 turn; //.x = x // .y = z
    private float mouse_sens;
    private bool jump_Input;
    private float speed = 0f;
    private float maxSpeed = 20f;
    private float acceleration = 10f;
    private float deceleration = 20f;
    public float thrust = -10f;
    public cameraEffect signal;
    [SerializeField] private CameraSwitch mainCam;
    [SerializeField] private GameObject prop1;
    [SerializeField] private GameObject prop2;
    [SerializeField] private GameObject prop3;
    [SerializeField] private GameObject prop4;
    [SerializeField] private Baterry battery;
    public bool offGround = false;
    [SerializeField] private PauseMenu pauseMenu;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        mouse_sens = PlayerPrefs.GetFloat("mouseSens");
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.paused) {
            if (/*cam1.activeSelf*/ !mainCam.getToFPV() && !signal.signalLost && battery.batterylife > 0) {
                RenderSettings.fogDensity = 0.003f;
                right_left_Input = Input.GetAxis("Horizontal");
                forward_backward_Input = Input.GetAxis("Vertical");
                fall_Input = Input.GetKey(KeyCode.LeftShift);
                jump_Input = Input.GetKey(KeyCode.Space);

            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) && (speed< maxSpeed)) {
                    speed = speed + acceleration * Time.deltaTime;  
                } else {
                    if(speed > deceleration * Time.deltaTime) {
                        speed = speed - deceleration * Time.deltaTime;
                    } else if(speed < -deceleration * Time.deltaTime) {
                        speed = speed + deceleration * Time.deltaTime;
                    } else {
                        speed = 0;
                    }
                }

                if (offGround) {
                    turn.x += Input.GetAxis("Mouse X") * mouse_sens;
                    transform.localRotation = Quaternion.Euler(0,turn.x,0);
                    transform.Translate(Vector3.right * Time.deltaTime * speed * -right_left_Input);
                    transform.Translate(Vector3.forward * Time.deltaTime * speed * -forward_backward_Input);
                

                if(right_left_Input > 0 && (tilt.x < maxTilt))
                {
                    tilt.x += tiltAcceleration * Time.deltaTime;
                } else if(right_left_Input < 0 && (tilt.x > -maxTilt))
                {
                    tilt.x -= tiltAcceleration * Time.deltaTime;
                } else if (right_left_Input == 0){
                    if(tilt.x > tiltDeceleration * Time.deltaTime)
                        tilt.x -= tiltDeceleration * Time.deltaTime;
                    else if(tilt.x < -tiltDeceleration* Time.deltaTime)
                        tilt.x += tiltAcceleration * Time.deltaTime;
                    else
                        tilt.x = 0f;
                    }
                

                if(forward_backward_Input > 0 && (tilt.y > -maxTilt))
                {
                    tilt.y -= tiltAcceleration * Time.deltaTime;
                }
                else if(forward_backward_Input < 0 && (tilt.y < maxTilt))
                {
                    tilt.y += tiltAcceleration * Time.deltaTime;
                }
                else if (forward_backward_Input == 0)
                {
                    if(tilt.y > tiltDeceleration * Time.deltaTime)
                        tilt.y -= tiltDeceleration * Time.deltaTime;
                    else if(tilt.y < -tiltDeceleration* Time.deltaTime)
                        tilt.y += tiltAcceleration * Time.deltaTime;
                    else
                        tilt.y = 0f;
                }
                    
                transform.Rotate(new Vector3(tilt.y,0,tilt.x));
                }
                    
                if(fall_Input && thrust >= -10)
                    {
                        thrust -= 2f * Time.deltaTime;
                    }

                    
            
                    if(jump_Input && thrust <= 10)
                    {
                        thrust += 2f * Time.deltaTime;
                    }

                if (offGround) {
                    if(thrust < -1 || thrust > 1)
                    {
                        player.AddForce(Vector3.up * thrust);
                    }
                } else {
                    if (thrust > 0) {
                        transform.GetComponent<AudioSource>().enabled = true;
                        prop1.GetComponent<PropellerMovement> ().enabled = true;
                        prop2.GetComponent<PropellerMovement> ().enabled = true;
                        prop3.GetComponent<PropellerMovement> ().enabled = true;
                        prop4.GetComponent<PropellerMovement> ().enabled = true;
                        if (transform.position.y > 24) {
                            offGround = true;
                            transform.GetComponent<BoxCollider> ().enabled = true;
                        } else {
                            player.AddForce(Vector3.up * thrust);
                        }
                    }
                }
            }

            if(battery.batterylife < 0) {
                    thrust = -10f;
                    player.AddForce(Vector3.up * thrust/2.5f);
                    speed = 10f;
                    transform.Translate(Vector3.forward * Time.deltaTime * speed * -forward_backward_Input);
                    transform.GetComponent<Rigidbody>().useGravity = true;
                    prop1.GetComponent<PropellerMovement>().enabled = false;
                    prop2.GetComponent<PropellerMovement>().enabled = false;
                    prop3.GetComponent<PropellerMovement>().enabled = false;
                    prop4.GetComponent<PropellerMovement>().enabled = false;
                }
        }
    }
}