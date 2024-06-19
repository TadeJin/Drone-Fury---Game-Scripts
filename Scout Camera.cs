using UnityEngine;

//Scout drone sphere rotation
public class ScoutCamera : MonoBehaviour
{
    private Vector2 turn;
    //public GameObject cam2;
    [SerializeField] private DetectTarget isON;
    [SerializeField] private CameraSwitch mainCam;
    [SerializeField] private PauseMenu pauseMenu;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.paused) {
            if (/*cam2.activeSelf*/ mainCam.getToFPV() && isON.track == false) {
                turn.x += Input.GetAxis("Mouse X") *  PlayerPrefs.GetFloat("mouseSens");
                transform.localRotation = Quaternion.Euler(0,turn.x,0);
            }
        }
    }
}