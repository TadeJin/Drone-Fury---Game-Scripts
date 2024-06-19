using UnityEngine;

//Scout drone cube rotation, rotation angle limitation
public class ScoutCube : MonoBehaviour
{
    private Vector2 turn;
    //public GameObject cam2;
    [SerializeField] private DetectTarget isON;
    [SerializeField] private CameraSwitch mainCam;
    [SerializeField] private PauseMenu pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(180,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.paused) {
        if (/*cam2.activeSelf*/ mainCam.getToFPV() && isON.track == false) {
            turn.y -= Input.GetAxis("Mouse Y") *  PlayerPrefs.GetFloat("mouseSens");
                if (turn.y < 0f) {
                    turn.y = 0f;
                }

                if (turn.y > 175f) {
                    turn.y = 175f;
                }

                transform.localRotation = Quaternion.Euler(Mathf.Clamp(turn.y,0,175),0,0);
            }
        }
    }
}
