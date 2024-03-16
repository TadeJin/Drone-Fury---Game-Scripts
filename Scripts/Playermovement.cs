using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private float mouse_sens = 0.75f;
    private bool jump_Input;
    private float speed = 0f;
    private float maxSpeed = 20f;
    private float acceleration = 10f;
    private float deceleration = 20f;
    public float thrust = 0f;
    //public GameObject cam1;
    public cameraEffect signal;
    [SerializeField] private CameraSwitch mainCam;
    

    //public GameObject cam1;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        //m_Rigidbody.AddForce(transform.up * m_Thrust);
    }

    // Update is called once per frame
    void Update()
    {
        if (/*cam1.activeSelf*/ !mainCam.getToFPV() && !signal.signalLost) {
            RenderSettings.fogDensity = 0.003f;
            right_left_Input = Input.GetAxis("Horizontal");
            forward_backward_Input = Input.GetAxis("Vertical");
            fall_Input = Input.GetKey(KeyCode.LeftShift);
            jump_Input = Input.GetKey(KeyCode.Space);

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) ) && (speed< maxSpeed)) {
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

            if(fall_Input && thrust >= -10)
                {
                    thrust -= 2f * Time.deltaTime;
                }

                
                // transform.localRotation = Quaternion.Euler(0, turn.x,0);
        
                if(jump_Input && thrust <= 10)
                {
                    thrust += 2f * Time.deltaTime;
                }

                if(thrust < -1 || thrust > 1)
                {
                    player.AddForce(Vector3.up * thrust);
                }
        }
    }
}