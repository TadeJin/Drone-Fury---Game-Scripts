using UnityEngine;

//Scout drone sphere rotation
public class ScoutCameraPredator : MonoBehaviour
{
    private Vector2 turn;
    //public GameObject cam2;
    [SerializeField] private Camera mainCam;
    [SerializeField] private PauseMenu pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.paused) {
            turn.x += Input.GetAxis("Mouse X") * PlayerPrefs.GetFloat("mouseSens");
            transform.localRotation = Quaternion.Euler(270,transform.rotation.y,turn.x);
        }
    }
}