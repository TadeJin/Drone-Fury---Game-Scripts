using UnityEngine;


public class Playermovementspredator : MonoBehaviour
{

    private float speed;
    private float roll;
    private float pitch;
    private float yaw;
    private Rigidbody rb;
    private float response = 0.5f;
    [SerializeField] private PredatorUI predatorUI;
    public bool diving;
    public bool climbing;
    [SerializeField] private PauseMenu pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    private void HandleInputs() {
        roll = Input.GetAxis("Roll");
        pitch = Input.GetAxis("Pitch");
        yaw = Input.GetAxis("Yaw");
    }
    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.paused) {
            HandleInputs();
        }
    }

    private void FixedUpdate() {
        speed = predatorUI.speed / (250 / 15);

        if (predatorUI.speed < 80) {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.0002f * (250 - predatorUI.speed) * 2,transform.position.z);
            if (transform.rotation.eulerAngles.x > 300) {
                    rb.AddTorque(transform.right * response *0.5f * 2.5f * (transform.rotation.eulerAngles.x - 360) * -1 * 0.25f);
            } else {
                if (transform.rotation.eulerAngles.x < 40) {
                    rb.AddTorque(transform.right * response * 0.5f * 2.5f * transform.rotation.eulerAngles.x * 0.5f);
                }
            }
        }

        if (transform.rotation.eulerAngles.x > 10 && transform.rotation.eulerAngles.x < 180) {
            diving = true;
        } else {
            diving = false;
        }

        if (transform.rotation.eulerAngles.x < 350 && transform.rotation.eulerAngles.x > 180) {
            climbing = true;
        } else {
            climbing = false;
        }

        if(((transform.rotation.eulerAngles.z - 360) * -1) < 35 || ((transform.rotation.eulerAngles.z - 360) * -1) > 325) {
            //NORMAL
            rb.AddForce(transform.forward * speed);
            rb.AddTorque(transform.up * yaw * response * 2.5f);
            rb.AddTorque(transform.right * pitch * response * 2.5f);
            rb.AddTorque(transform.forward * roll * response * 2.5f);
        } else if (((transform.rotation.eulerAngles.z - 360) * -1) > 35 && ((transform.rotation.eulerAngles.z - 360) * -1) < 180) {
            //TOO STRONG ANGLE
            rb.AddForce(transform.forward * speed);
            rb.AddTorque(transform.forward * roll * response * 2.5f);
            rb.AddTorque(transform.right * pitch * response * 2.5f);
            rb.AddTorque(transform.up * 0.5f * response * 5f);
        } else if (((transform.rotation.eulerAngles.z - 360) * -1) < 325) {
            //TOO STRONG ANGLE ON DIFFERENT SIDE
            rb.AddForce(transform.forward * speed);
            rb.AddTorque(transform.forward * roll * response * 2.5f);
            rb.AddTorque(transform.right * pitch * response * 2.5f);
            rb.AddTorque(transform.up * -0.5f * response * 5f);
        }
    }
}
